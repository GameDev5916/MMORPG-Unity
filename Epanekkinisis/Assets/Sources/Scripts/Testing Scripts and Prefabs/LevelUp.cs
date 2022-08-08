using UnityEngine;
using System.Collections;

public class LevelUp : MonoBehaviour {
	
	bool LightOff;
	float inte;
	
	// Use this for initialization
	void Start () {
		
		gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up*2000);
		gameObject.SendMessage("Up");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(LightOff){
			
			if(gameObject.GetComponentInChildren<Light>().intensity > 0){
				
				inte = GetComponentInChildren<Light>().intensity;
				inte -= 0.1f;
				gameObject.GetComponentInChildren<Light>().intensity = inte;
				
			}
			
		}
		
	}
	
	
	public IEnumerator Up(){
		
		yield return new WaitForSeconds(3);
		
		gameObject.GetComponent<ParticleEmitter>().emit = false;
		
		LightOff = true;
		
	}
	
	
}
