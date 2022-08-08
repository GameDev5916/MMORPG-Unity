using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recognizer : MonoBehaviour {

	//C:/Users/Upper/AppData/LocalLow/Razorwave/Epanekkinisis

	private List<Vector2> points = new List<Vector2>();
	
	public string gesture;
	
	private Vector3 virtualKeyPosition = Vector2.zero;
	private Rect drawArea;
	
	private GestureLibrary gl;
	
	public string libraryToLoad;


	void Start() {
		
		gl = new GestureLibrary(libraryToLoad);
		drawArea = new Rect(0, 0, Screen.width, Screen.height);
		
	}
	
	
	void Update() {

		if (Input.GetMouseButton(0)) {
			virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
		}
		
		if (drawArea.Contains(virtualKeyPosition)) {
			
			if (Input.GetMouseButtonDown(0)) {
				points.Clear();
			}
			
			if (Input.GetMouseButton(0)) {
				points.Add(new Vector2(virtualKeyPosition.x, -virtualKeyPosition.y));
			}
			
			if (Input.GetMouseButtonUp(0)) {

				Gesture g = new Gesture(points);
				Result result = g.Recognize(gl, true);
				
				gesture = result.Name + " @ " + result.Score;
				GetComponent<Skills>().RunSkill(result.Name);

			}
			
		}
		
	}
	
	
	private Vector3 WorldCoordinateForGesturePoint(Vector3 gesturePoint) {

		Vector3 worldCoordinate = new Vector3(gesturePoint.x, gesturePoint.y, 10);
		return Camera.main.ScreenToWorldPoint(worldCoordinate);

	}
}
