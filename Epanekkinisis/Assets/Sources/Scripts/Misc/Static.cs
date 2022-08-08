using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Static : MonoBehaviour {
	
	public GameObject ClosestPlayer;
	public float ClosestPlayerDistance;

	public float AttackSpeed;
	public float TurnSpeed;
	
	public List<GameObject> Targets = new List<GameObject>();
	public List<float> Distances = new List<float>();
	
	
	void Start () {
		
		gameObject.SendMessage("CustomAnimation", "idle");
		
	}
	
	
	void Update () {
			
			//-----------------------------------------------------------------------------------------------------

			if(Targets.Count == 0){
				ClosestPlayer = null;
			}
			else if(Targets.Count == 1){
				ClosestPlayer = Targets[0];
				ClosestPlayerDistance = Vector3.Distance(transform.localPosition, ClosestPlayer.transform.localPosition);
			}
			else{
				
				Distances.Clear();
				
				for(int i = 0; i <= Targets.Count-1; i++){
					Distances.Add(Vector3.Distance(transform.localPosition, Targets[i].transform.localPosition));
				}
				
				ClosestPlayer = Targets[Array.IndexOf(Distances.ToArray(), Mathf.Max(Distances.ToArray()))];
				ClosestPlayerDistance = Vector3.Distance(transform.localPosition, ClosestPlayer.transform.localPosition);
				
			}
			
			
			//-----------------------------------------------------------------------------------------------------
			
			if(ClosestPlayer != null && !Physics.Raycast(transform.localPosition, ClosestPlayer.transform.localPosition - transform.localPosition, Vector3.Distance(ClosestPlayer.transform.localPosition, transform.localPosition)-1)){
				
				// find the target direction:
				Vector3 dir = ClosestPlayer.transform.localPosition - transform.localPosition;
				
				dir.y = 0; // make direction strictly horizontal
				dir.Normalize(); // normalize it
				
				// turn gradually to the target each frame:
				transform.forward = Vector3.Slerp(transform.forward, dir, TurnSpeed * Time.deltaTime);
				
			}
		
	}
	
	
	
	IEnumerator Attack () {
		
		yield return new WaitForSeconds((float)(GetComponent<AnimationManager>().animationComp.GetClip("attack").length + AttackSpeed));
		
		gameObject.SendMessage("CustomAnimation", "attack");
		
		gameObject.SendMessage("Attack");
		
	}


	void OnCollisionEnter(Collision col){
		
		GetComponentInChildren<MonsterLife>().DamageMe(col.transform.GetComponent<Shot>().Damage);
		
	}

	
	void OnTriggerStay(Collider col){		//Bori na einai mesa alla na mi fenete

		if(col.tag == "Player" || col.tag == "Dummy"){

			for(int i = 0; i <= Targets.Count-1; i++){
				
				if(Targets[i] == col.gameObject){
					return;
				}
				
			}

			if(!Physics.Raycast(transform.localPosition, col.transform.localPosition - transform.localPosition, Vector3.Distance(transform.localPosition, col.transform.localPosition)-1)){

				Targets.Add(col.gameObject);
				gameObject.SendMessage("Attack");
				
			}
			
		}
		
	}


	
	void OnTriggerExit(Collider col){
		
		if(col.tag == "Player" || col.tag == "Dummy"){
			
			Targets.Remove(col.gameObject);

			gameObject.SendMessage("CustomAnimation", "idle");
						
		}
		
	}
	
	
}
