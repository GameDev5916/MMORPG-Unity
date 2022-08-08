using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CapturePoints : MonoBehaviour {

    private List<Vector2> points = new List<Vector2>();

    public GameObject gestureOnScreen;
    private LineRenderer gestureLineRenderer;
    private int vertexCount = 0;

    private string message;
    private RuntimePlatform platform;

    private Vector3 virtualKeyPosition = Vector2.zero;
    private Rect drawArea;

    private string newGestureName = "";
    private GestureLibrary gl;

    public string libraryToLoad;


    void Start() {
        gl = new GestureLibrary(libraryToLoad);

        platform = Application.platform;

        gestureLineRenderer = gestureOnScreen.GetComponent<LineRenderer>();

        drawArea = new Rect(0, 0, Screen.width - 370, Screen.height);
    }


    void Update() {

        if (Input.GetKeyUp(KeyCode.Escape)) {
            Application.Quit();
        }

        if (platform == RuntimePlatform.Android) {
            if (Input.touchCount > 0) {
                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            }
        } else {
            if (Input.GetMouseButton(0)) {
                virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            }
        }

        if (drawArea.Contains(virtualKeyPosition)) {

            if (Input.GetMouseButtonDown(0)) {
                points.Clear();
                gestureLineRenderer.SetVertexCount(0);
                vertexCount = 0;
            }

            if (Input.GetMouseButton(0)) {
                points.Add(new Vector2(virtualKeyPosition.x, -virtualKeyPosition.y));

                gestureLineRenderer.SetVertexCount(++vertexCount);
                gestureLineRenderer.SetPosition(vertexCount - 1, WorldCoordinateForGesturePoint(virtualKeyPosition));
            }

            if (Input.GetMouseButtonUp(0)) {
                Gesture g = new Gesture(points);
                Result result = g.Recognize(gl, true);

                message = result.Name + "; " + result.Score;
            }

        }

    }


    void OnGUI() {
        GUI.Box(drawArea, "Draw Area");

        GUI.skin.label.fontSize = 20;
        GUI.Label(new Rect(10, Screen.height - 40, 500, 50), message);

        GUI.Label(new Rect(Screen.width - 340, 10, 70, 30), "Add as: ");
        newGestureName = GUI.TextField(new Rect(Screen.width - 270, 10, 200, 30), newGestureName);

        if (GUI.Button(new Rect(Screen.width - 60, 10, 50, 30), "Add")) {
            Gesture newGesture = new Gesture(points, newGestureName);
            gl.AddGesture(newGesture);
        }
    }


    private Vector3 WorldCoordinateForGesturePoint(Vector3 gesturePoint) {
        Vector3 worldCoordinate = new Vector3(gesturePoint.x, gesturePoint.y, 10);
        return Camera.main.ScreenToWorldPoint(worldCoordinate);
    }
}
