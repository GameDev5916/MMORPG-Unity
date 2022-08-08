using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gesture {
	
    /**
     * CLASS PROPERTIES
     */

    public string Name { get; set; }
    public List<Vector2> Points { get; set; }
    public List<float> Vector { get; set; }
    public float IndicativeAngle { get; set; }

    /**
     * CLASS CONSTANTS
     */
    public int NUMBER_OF_POINTS { get { return 64; } }
    public float SQUARE_SIZE { get { return 250f; } }
    public Vector2 ORIGIN { get { return Vector2.zero; } }
    public float DIAGONAL { get { return Mathf.Sqrt(Mathf.Pow(SQUARE_SIZE, 2) * 2); } }
    public float HALF_DIAGONAL { get { return DIAGONAL * 0.5f; } }
    public float ANGLE_RANGE { get { return 45f * Mathf.Deg2Rad; } }
    public float ANGLE_PRECISION { get { return 2f * Mathf.Deg2Rad; } }
    public float PHI { get { return 0.5f * (-1f + Mathf.Sqrt(5f)); } }


    public Gesture(List<Vector2> points, string name = "") {
        this.Name = name;
        this.Points = points;
        this.IndicativeAngle = Gesture.GetIndicativeAngle(points);
        this.Points = this.Resample(NUMBER_OF_POINTS);
        this.Points = this.RotateBy(-this.IndicativeAngle);
        this.Points = this.ScaleTo(this.SQUARE_SIZE);
        this.Points = this.TranslateTo(this.ORIGIN);
        this.Vector = this.Vectorize();
    }


    public Result Recognize(GestureLibrary gestureLibrary, bool fast = false) {

        if (this.Points.Count <= 2) {
            return new Result("Not enough points captured", 0f);
        } else {
            List<Gesture> library = gestureLibrary.Library;

			float b = .25f;	//Identification Factor
            int u = -1;

            for (int i = 0; i < library.Count; i++) {

                float d = 0;
            
                if (fast) {
                    d = GetOptimalCosineDistance(library[i].Vector, this.Vector);
                } else {
                    d = GetDistanceAtBestAngle(library[i], -this.ANGLE_RANGE, +this.ANGLE_RANGE, this.ANGLE_PRECISION);
                }

                if (d < b) {
                    b = d;
                    u = i;
                }
            }

            if (u == -1) {
                return new Result("No match", 0f);
            } else {
                return new Result(library[u].Name, fast ? 1f / b : 1f - b / this.HALF_DIAGONAL);
            }
        }
    }


    public List<Vector2> Resample(int numberOfPoints) {

        float I = Gesture.GetPathLength(this.Points) / (numberOfPoints - 1);
        float D = 0.0f;

        List<Vector2> resampledPoints = new List<Vector2>();
        resampledPoints.Add(this.Points[0]);

        for (int i = 1; i < this.Points.Count; i++) {
            float d = Vector2.Distance(this.Points[i - 1], this.Points[i]);

            if (D + d >= I) {

                float x = this.Points[i - 1].x + ((I - D) / d) * (this.Points[i].x - this.Points[i - 1].x);
                float y = this.Points[i - 1].y + ((I - D) / d) * (this.Points[i].y - this.Points[i - 1].y);
                Vector2 q = new Vector2(x, y);
                resampledPoints.Add(q);
                this.Points.Insert(i, q);
                D = 0.0f;

            } else {
                D += d;
            }
        }

        if (resampledPoints.Count == numberOfPoints - 1) {
            resampledPoints.Add(this.Points[this.Points.Count - 1]);
        }

        return resampledPoints;
    }


    public List<Vector2> RotateBy(float angle) {

        Vector2 center = Gesture.GetCenter(this.Points);
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);
        List<Vector2> rotatedPoints = new List<Vector2>();

        for (int i = 0; i < this.Points.Count; i++) {
            float x = (this.Points[i].x - center.x) * cos - (this.Points[i].y - center.y) * sin + center.x;
            float y = (this.Points[i].x - center.x) * sin + (this.Points[i].y - center.y) * cos + center.y;
            rotatedPoints.Add(new Vector2(x, y));
        }

        return rotatedPoints;
    }


    public List<Vector2> ScaleTo(float size) {

        Rect boundingBox = Gesture.GetBoundingBox(this.Points);
        List<Vector2> scaledPoints = new List<Vector2>();

        for (int i = 0; i < this.Points.Count; i++) {
            float x = this.Points[i].x * (size / boundingBox.width);
            float y = this.Points[i].y * (size / boundingBox.height);
            scaledPoints.Add(new Vector2(x, y));
        }

        return scaledPoints;
    }


    public List<Vector2> TranslateTo(Vector2 point) {

        Vector2 center = Gesture.GetCenter(this.Points);
        List<Vector2> translatedPoints = new List<Vector2>();

        for (int i = 0; i < this.Points.Count; i++) {
            float x = this.Points[i].x + point.x - center.x;
            float y = this.Points[i].y + point.y - center.y;
            translatedPoints.Add(new Vector2(x, y));
        }

        return translatedPoints;
    }


    public List<float> Vectorize() {

        float sum = 0f;
        List<float> vector = new List<float>();

        for (int i = 0; i < this.Points.Count; i++) {
            vector.Add(this.Points[i].x);
            vector.Add(this.Points[i].y);
            sum += Mathf.Pow(this.Points[i].x, 2) + Mathf.Pow(this.Points[i].y, 2);
        }

        float magnitude = Mathf.Sqrt(sum);

        for (int i = 0; i < vector.Count; i++) {
            vector[i] /= magnitude;
        }

        return vector;
    }


    public static Vector2 GetCenter(List<Vector2> points) {
        Vector2 center = Vector2.zero;

        for (int i = 0; i < points.Count; i++) {
            center += points[i];
        }

        return center / points.Count;
    }


    public static Rect GetBoundingBox(List<Vector2> points) {

        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;

        for (int i = 0; i < points.Count; i++) {
            minX = Mathf.Min(minX, points[i].x);
            minY = Mathf.Min(minY, points[i].y);
            maxX = Mathf.Max(maxX, points[i].x);
            maxY = Mathf.Max(maxY, points[i].y);
        }

        return new Rect(minX, minY, maxX - minX, maxY - minY);
    }


    public static float GetPathLength(List<Vector2> points) {

        float length = 0;

        for (int i = 1; i < points.Count; i++) {
            length += Vector2.Distance(points[i - 1], points[i]);
        }

        return length;
    }


    public static float GetDistanceBetweenPaths(List<Vector2> points1, List<Vector2> points2) {

        float distance = 0;

        for (int i = 0; i < points1.Count; i++) {
            distance += Vector2.Distance(points1[i], points2[i]);
        }

        return distance / points1.Count;
    }


    public static float GetIndicativeAngle(List<Vector2> points) {
        Vector2 centroid = Gesture.GetCenter(points);
        return Mathf.Atan2(centroid.y - points[0].y, centroid.x - points[0].x);
    }


    public float GetDistanceAtAngle(Gesture gesture, float angle) {
        List<Vector2> newPoints = this.RotateBy(angle);
        return Gesture.GetDistanceBetweenPaths(newPoints, gesture.Points);
    }


    public float GetDistanceAtBestAngle(Gesture gesture, float a, float b, float threshold) {

        float x1 = this.PHI * a + (1f - this.PHI) * b;
        float f1 = this.GetDistanceAtAngle(gesture, x1);
        float x2 = (1f - this.PHI) * a + this.PHI * b;
        float f2 = this.GetDistanceAtAngle(gesture, x2);

        while (Mathf.Abs(b - a) > threshold) {
            if (f1 < f2) {
                b = x2;
                x2 = x1;
                f2 = f1;
                x1 = this.PHI * a + (1f - this.PHI) * b;
                f1 = this.GetDistanceAtAngle(gesture, x1);
            } else {
                a = x1;
                x1 = x2;
                f1 = f2;
                x2 = (1f - this.PHI) * a + this.PHI * b;
                f2 = this.GetDistanceAtAngle(gesture, x2);
            }
        }

        return Mathf.Min(f1, f2);
    }


    public float GetOptimalCosineDistance(List<float> v1, List<float> v2)
    {
        float a = 0f;
        float b = 0f;
        
        for (int i = 0; i < v1.Count; i += 2)
        {
            a += v1[i] * v2[i] + v1[i + 1] * v2[i + 1];
            b += v1[i] * v2[i + 1] - v1[i + 1] * v2[i];
        }

        float angle = Mathf.Atan(b / a);
        return Mathf.Acos(a * Mathf.Cos(angle) + b * Mathf.Sin(angle));
    }


    public override string ToString() {

        string message = this.Name + "; ";

        foreach (Vector2 v in this.Points) {
            message += v.ToString() + " ";
        }

        return message;
    }

} // end of Gesture
