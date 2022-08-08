using UnityEngine;
using System.Collections;

public class Skala : MonoBehaviour {
	
	bool left;
	bool right;
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(left){
		transform.Rotate(0, -0.1f, 0);
		}
		else if(right){
		transform.Rotate(0, 0.1f, 0);
		}
		
	}
	
	IEnumerator Rotate(){
		
		left = true;
		
		yield return new WaitForSeconds(2);
		
		left = false;
		
		yield return new WaitForSeconds(5);
		
		gameObject.SendMessage("Rotate2");
		
	}
	
	IEnumerator Rotate2(){
		
		right = true;
		
		yield return new WaitForSeconds(2);
		
		right = false;
		
		yield return new WaitForSeconds(5);
		
		gameObject.SendMessage("Rotate");
		
	}
	
	void Starte(){
	
		gameObject.SendMessage("Rotate");
		
	}
	
}
