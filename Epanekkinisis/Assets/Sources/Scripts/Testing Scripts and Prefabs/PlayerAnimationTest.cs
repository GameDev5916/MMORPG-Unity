using UnityEngine;
using System.Collections;

public class PlayerAnimationTest : MonoBehaviour {
	
	Animation anim;
	public GameObject skeleton;
	public float smooth;
	string turn;
	
	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animation>();
		anim.wrapMode = WrapMode.Loop;
		anim.GetComponent<Animation>()["attack"].wrapMode = WrapMode.Once;
		anim.GetComponent<Animation>()["attack"].layer = 1;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		/*if(Input.GetButton("Vertical") && Input.GetAxis("Vertical") > 0){
			
			skeleton.transform.localRotation = Quaternion.Slerp(skeleton.transform.localRotation, new Quaternion(0, 0, 0, 0),  Time.deltaTime*smooth);
			
			animation.CrossFade("run");
			
		}
		
		if(Input.GetButton("Vertical") && Input.GetAxis("Vertical") < 0){

			skeleton.transform.localRotation = Quaternion.Slerp(skeleton.transform.localRotation, new Quaternion(0, 180, 0, 0),  Time.deltaTime*smooth);
			
			animation.CrossFade("run");
			
		}
		
		if(Input.GetButton("Horizontal") && Input.GetAxis("Horizontal") > 0){

			skeleton.transform.localRotation = Quaternion.Slerp(skeleton.transform.localRotation, new Quaternion(0, 575, 0, 0),  Time.deltaTime*smooth);								
			
			animation.CrossFade("run");
			
		}
		
		if(Input.GetButton("Horizontal") && Input.GetAxis("Horizontal") < 0){
			
			skeleton.transform.localRotation = Quaternion.Slerp(skeleton.transform.localRotation, new Quaternion(0, 270, 0, 0),  Time.deltaTime*smooth);
		
			animation.CrossFade("run");
			
		}
		
		if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0){
		
			animation.CrossFade("idle");
			
		}*/
		
		if (Input.GetButtonDown("Fire1")){
      	
			//animation.CrossFade("attack");
			
			skeleton.transform.localRotation = Quaternion.Slerp(skeleton.transform.localRotation, new Quaternion(0, -700, 0, 0),  Time.deltaTime*smooth);
			
		} 
		

	}
	
	
}
