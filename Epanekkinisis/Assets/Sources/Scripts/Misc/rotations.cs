using UnityEngine;
using System.Collections;

public class rotations : MonoBehaviour {

	//Publics
	
	public bool rotx;
	public bool roty;
	public bool rotz;
	public float rotspeed;
	
	public string XXXXXXXXXXXXXXX = "XXXXXXXXXXXXXXXXXX";
	
	public bool updown;
	public float udspeed;
	public float distance;
	
	public string XXXXXXXXXXXXXXXX = "XXXXXXXXXXXXXXXXXX";
	
	public bool forward;
	public bool right;
	public bool left;
	public bool back;
	public float mspeed;
	
	
	//Privates	
	
	float posy;
	bool done;
	
	
	// Use this for initialization
	void Start () {
		posy = transform.localPosition.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(forward){			
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + mspeed);			
		}		
		if(right){			
			transform.localPosition = new Vector3(transform.localPosition.x + mspeed, transform.localPosition.y, transform.localPosition.z);			
		}		
		if(left){			
			transform.localPosition = new Vector3(transform.localPosition.x - mspeed, transform.localPosition.y, transform.localPosition.z);			
		}		
		if(back){			
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - mspeed);			
		}
		
		if(rotx){
			transform.Rotate(rotspeed, 0, 0);
		}		
		if(roty){
			transform.Rotate(0, rotspeed, 0);
		}		
		if(rotz){
			transform.Rotate(0, 0, rotspeed);		
		}


		if(updown){
			
			if(transform.localPosition.y >= posy + distance){
				done = true;
			}
			else if(transform.localPosition.y <= posy - distance){
				done = false;			
			}			
			
			if(done){
				transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - udspeed, transform.localPosition.z);		
			}
			else{
				transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + udspeed, transform.localPosition.z);		
			}			
			
		}
		
		
	}
	
	void stoprot(){
		rotx = false;
		roty = false;
		rotz = false;
	}
	
	void startrot(){
		rotx = true;
		roty = true;
		rotz = true;
	}
	
	void startrotx(){
		rotx = true;
	}
	
	void startroty(){
		roty = true;
	}
	
	void startrotz(){
		rotz = true;
	}
	
	
}
