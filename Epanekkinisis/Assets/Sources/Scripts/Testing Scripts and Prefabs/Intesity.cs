using UnityEngine;
using System.Collections;

public class Intesity : MonoBehaviour {
	
	float inte;
	public float speed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(gameObject.GetComponent<Light>().intensity > 0){
			
		inte = GetComponent<Light>().intensity;
		inte -= speed;
		gameObject.GetComponent<Light>().intensity = inte;
			
		}
		else{
			
		Destroy(gameObject);	
			
		}
		
	}
	
}
