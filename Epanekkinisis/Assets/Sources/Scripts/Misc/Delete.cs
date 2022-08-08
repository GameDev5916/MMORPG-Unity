using UnityEngine;
using System.Collections;

public class Delete : MonoBehaviour {
	
	public float time;
	public bool IsParticle;
	
	// Use this for initialization
	void Start () {
		
		if(IsParticle){
		gameObject.GetComponentInChildren<ParticleAnimator>().autodestruct = true;	
		}
		
		gameObject.SendMessage("boom", time);
		
	}
	
	
	public IEnumerator boom(float timer){

		yield return new WaitForSeconds(timer);
		
		if(IsParticle){
		gameObject.GetComponentInChildren<ParticleEmitter>().emit = false;
		}
		else{
		Destroy(gameObject);		
		}
		
	}
	
	
}
