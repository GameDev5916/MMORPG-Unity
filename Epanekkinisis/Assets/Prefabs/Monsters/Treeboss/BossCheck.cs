using UnityEngine;
using System.Collections;

public class BossCheck : MonoBehaviour {
	
	public GameObject gate;
	public GameObject tree;
	
	void OnTriggerEnter(Collider col){
	
		if(col.tag == "Player"){
		
			GameObject.Find("Boss").GetComponent<Treeboss>().enabled = true;
			gate.GetComponent<BoxCollider>().enabled = true;
			tree.SetActive(true);
			
		}
		
	}
	
}
