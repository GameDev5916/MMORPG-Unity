using UnityEngine;
using System.Collections;

public class CameraReset : MonoBehaviour {
	
	public bool touch;
	Vector3 pos;
	Vector3 velocity = Vector3.zero;
	
	float dist;
	
	// Use this for initialization
	void Start () {
		pos = transform.localPosition;
	}
	
	// Update is called once per frame
	void FixedUpdate () {		
		
		if(Input.GetAxis("Mouse ScrollWheel") > 0){
			transform.parent.Rotate(1, 0, 0);
		}
		else if(Input.GetAxis("Mouse ScrollWheel") < 0){
			transform.parent.Rotate(-1, 0, 0);
		}	

		if(!touch && Vector3.Distance(transform.position, GameObject.Find("Graphics").transform.position) != 1.910533f){
			transform.localPosition = Vector3.SmoothDamp(transform.localPosition, pos, ref velocity, 0.3f);
		}
		
	}
	
	void OnCollisionExit (Collision collision){
		touch = false;
	}
	
	void OnCollisionEnter(Collision collision){
		touch = true;
	}
	
}
