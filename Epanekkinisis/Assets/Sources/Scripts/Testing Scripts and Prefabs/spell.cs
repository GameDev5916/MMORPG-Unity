using UnityEngine;
using System.Collections;

public class spell : MonoBehaviour {
	
	public Transform target;
	public float speed;
	public float turn;
	public AudioClip[] clips;
	
	// Use this for initialization
	void Start () {
	
		target = GameObject.FindWithTag("Player").GetComponentInChildren<Pickup>().TempItem.transform;
			
		GetComponent<AudioSource>().clip = clips[Random.Range(0, 5)];
		
		GetComponent<AudioSource>().Play();
		
		if(GameObject.FindWithTag("Player").GetComponent<swap_cams>().ActiveCam.name == "Main Camera"){
		
			GameObject.Find("Main Camera").GetComponent<Animation>().CrossFade("cam");
			
		}
		else{
				
			GameObject.Find("Camera").GetComponent<Animation>().CrossFade("cam2");
			
		}
		
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
			
			GetComponent<ParticleEmitter>().minEmission++;
			
			if(speed > 0){
				
				transform.position += transform.forward/speed;
		
				if(target.tag == "Monster" || target.tag == "Fire"){
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), turn);		
				}

			}
			else if(speed == 0){
			
				transform.position += transform.forward;
		
			if(target.tag == "Monster" || target.tag == "Fire"){
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), turn);	
			}

			}
			else if(speed < 0){
			
				transform.position += transform.forward*Mathf.Abs(speed);
		
			if(target.tag == "Monster" || target.tag == "Fire"){
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), turn);	
			}

			}
			
	}
	
	
	void OnCollisionEnter(Collision col){
	
		GetComponent<ParticleEmitter>().emit = false;
		
	}
	
	
}
