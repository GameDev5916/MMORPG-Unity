using UnityEngine;
using System.Collections;

public class LerpToTransparent : MonoBehaviour {
	
	public Material material1;
	public Material material2;
	
	public float speed;
	RaycastHit hit;
	
	public Transform old;
	
	void Update() {
		
		if(Physics.Raycast(transform.position, GameObject.Find("Graphics").transform.position - transform.position, out hit, Vector3.Distance(transform.position, GameObject.Find("Graphics").transform.position)-1) && hit.transform.GetComponent<Renderer>()){
			
			if(!old){
				
				old = hit.transform;
				
				material1 = hit.transform.GetComponent<Renderer>().material;
				hit.transform.GetComponent<Renderer>().material = material2;
				
			}
			
		}
		else{
			
			if(old){

				old.GetComponent<Renderer>().material = material1;
				old = null;
				material1 = null;

			}
			
		}
		
	}
	
}