using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Melee : MonoBehaviour {
	
	public GameObject ClosestPlayer;
	public float ClosestPlayerDistance;
	
	CharacterController controller;
	RaycastHit hit;
	int spot;
	
	public float AttackDistance;
	public float TurnSpeed;
	public float Speed;
	
	public bool str8;
	
	public List<GameObject> Targets = new List<GameObject>();
	public List<float> Distances = new List<float>();
	public List<Vector3> Spots = new List<Vector3>();
	
	
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	
	void Update () {
		
		if(!GetComponent<Patrol>().patrol){
			
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
			
			if((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)){
				gameObject.SendMessage("AddSpot");
			}
			
			
			//-----------------------------------------------------------------------------------------------------
			
			
			if(ClosestPlayerDistance < AttackDistance && !GetComponent<AnimationManager>().animationComp.isPlaying){
				gameObject.SendMessage("CustomAnimation", "attack");
			}
			
			if(ClosestPlayerDistance > AttackDistance){
				
				if(!GetComponent<AnimationManager>().animationComp.isPlaying){
					gameObject.SendMessage("CustomAnimation", "run");
				}
				
				if(Physics.Raycast(transform.localPosition, ClosestPlayer.transform.localPosition - transform.localPosition, out hit, Vector3.Distance(transform.localPosition, ClosestPlayer.transform.localPosition)-1) && hit.transform.name != "Player"){
					
					str8 = false;
					
					for(int i = 0; i <= Spots.Count-1; i++){
						
						if(!Physics.Raycast(transform.localPosition, Spots[i] - transform.localPosition, out hit, Vector3.Distance(transform.localPosition, Spots[i]))){
							spot = i;
						}
						
					}
					
					if((Vector3.Distance(transform.localPosition, Spots[spot]) <= 1) && (spot < Spots.Count-1)){
						spot++;
					}
					
					// find the target direction:
					Vector3 dir = Spots[spot] - transform.localPosition;
					
					dir.y = 0; // make direction strictly horizontal
					dir.Normalize(); // normalize it
					
					// turn gradually to the target each frame:
					transform.forward = Vector3.Slerp(transform.forward, dir, TurnSpeed * Time.deltaTime);
					
					Vector3 forward = transform.TransformDirection(Vector3.forward);
					controller.SimpleMove(forward * Speed);
					
				}
				else{
					
					str8 = true;
					
					// find the target direction:
					Vector3 dir = ClosestPlayer.transform.localPosition - transform.localPosition;
					
					dir.y = 0; // make direction strictly horizontal
					dir.Normalize(); // normalize it
					
					// turn gradually to the target each frame:
					transform.forward = Vector3.Slerp(transform.forward, dir, TurnSpeed * Time.deltaTime);
					
					Vector3 forward = transform.TransformDirection(Vector3.forward);
					controller.SimpleMove(forward * Speed);
					
				}
				
			}
			
		}
		
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
			
			if(!Physics.Raycast(transform.localPosition, col.transform.localPosition - transform.localPosition, out hit, Vector3.Distance(transform.localPosition, col.transform.localPosition)-1)){
				
				GetComponent<Patrol>().patrol = false;
				
				Targets.Add(col.gameObject);
				Spots.Add(col.transform.localPosition);
				
				gameObject.SendMessage("CustomAnimation", "run"); //Episis starxidia mas
				
			}
			
		}
		
	}


	void OnTriggerExit(Collider col){
		
		if(col.tag == "Player" || col.tag == "Dummy"){
			
			if(Targets.Count == 1){
				Targets.Remove(col.gameObject);
				Spots.Clear();
				GetComponent<Patrol>().patrol = true;
			}
			else{
				Targets.Remove(col.gameObject);
			}
			
		}
		
	}
	
	
	
	
	public void AddSpot(){
		
		if(ClosestPlayer != null && Spots[Spots.Count-1] != ClosestPlayer.transform.localPosition){
			Spots.Add(ClosestPlayer.transform.localPosition);
		}
		
	}	

	
}
