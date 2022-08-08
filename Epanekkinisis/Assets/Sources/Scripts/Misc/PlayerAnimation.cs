using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	
	Animation anim;
	public GameObject skeleton;
	public float speed;
	
	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animation>();
		anim.wrapMode = WrapMode.Loop;
		anim.GetComponent<Animation>()["attack"].wrapMode = WrapMode.Once;
		anim.GetComponent<Animation>()["attack"].layer = 1;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetAxis("Vertical") > 0){

			//skeleton.transform.localRotation = new Quaternion(0, 0, 0, 0);
			
			skeleton.transform.localRotation = Quaternion.Lerp(skeleton.transform.localRotation, Quaternion.Euler(0,0,0), speed);  // 90 degrees forward/down
			
			anim.CrossFade("run");
			
		}
		
		if(Input.GetAxis("Vertical") < 0){
		
			//skeleton.transform.localRotation = new Quaternion(0, 180, 0, 0);
			
			skeleton.transform.localRotation = Quaternion.Lerp(skeleton.transform.localRotation, Quaternion.Euler(0,180,0), speed);  // 90 degrees backwards/up
			
			anim.CrossFade("run");
			
		}
		
		if(Input.GetAxis("Horizontal") > 0){
			
			//skeleton.transform.localRotation = new Quaternion(0, 90, 0, 0);
			
			skeleton.transform.localRotation = Quaternion.Lerp(skeleton.transform.localRotation, Quaternion.Euler(0,90,0), speed);  // 90 degrees right
			
			anim.CrossFade("run");
			
		}
		
		if(Input.GetAxis("Horizontal") < 0){
			
			//skeleton.transform.localRotation = new Quaternion(0, 270, 0, 0);
			
			skeleton.transform.localRotation = Quaternion.Lerp(skeleton.transform.localRotation, Quaternion.Euler(0,-90,0), speed);  // 90 degrees left
			
			anim.CrossFade("run");
			
		}
		
		
		if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0){
		
			anim.CrossFade("idle");
			
		}
		
		
		if (Input.GetButtonDown("Fire1")){
			
			anim.CrossFade("attack");
		
		} 

		
	}
	
	
}
