using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {
	
	public float stopAt;
	bool go;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(go){
		
		if(transform.rotation.y <= stopAt){
		
		transform.Rotate(0f, 2f, 0f);
		
		}
		else{
			
		go = false;	
			
		}
			
		}
		
	}
	

	void Activate(){
		
		go = true;
		
	}

	
}
