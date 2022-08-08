using UnityEngine;
using System.Collections;

public class AreaFinder : MonoBehaviour {

	public Vector2 start;
	public Vector2 size;
	Vector2 end;


	void OnGUI () {

		if(Input.GetMouseButtonDown(0)){
			
			start = Event.current.mousePosition;
			
		}
		
		if(Input.GetMouseButtonUp(0)){
			
			end = Event.current.mousePosition;
			size.x = end.x - start.x;
			size.y = end.y - start.y;
			
		}

	}

}
