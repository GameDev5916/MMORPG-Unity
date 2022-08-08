using UnityEngine;
using System.Collections;

public class SmoothLookAt : MonoBehaviour {
	
	public Transform End;
	public Transform Assistant;
	public float Speed = 1f;

	public int zoom = 20;
	public int normal = 60;
	public float smooth = 5;
	
	public bool isZoomed = false;

	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonUp(0)){

			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Skillball") {
				End = hit.transform;
			}

		}

		Assistant.LookAt(End);
		transform.rotation = Quaternion.Slerp(transform.rotation, Assistant.rotation, Time.deltaTime * Speed);

		if(Input.GetKeyDown("z")){
			isZoomed = !isZoomed; 
		}
		
		if(isZoomed){
			GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView,zoom,Time.deltaTime*smooth);
		}
		else{
			GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView,normal,Time.deltaTime*smooth);
		}

	}
}
