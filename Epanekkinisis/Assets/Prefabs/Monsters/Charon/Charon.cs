using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Charon : MonoBehaviour {
	
	public GameObject Player;
	public float PlayerDistance;
	
	CharacterController controller;
	RaycastHit hit;
	int spot;
	
	public float AttackDistance;
	public float TurnSpeed;
	public float Speed;
	
	public bool str8;
	
	public List<Vector3> Spots = new List<Vector3>();
	
	
	void Start () {
		Player = GameObject.FindWithTag("Player");
		controller = GetComponent<CharacterController>();
	}
	
	
	void Update () {
		
		PlayerDistance = Vector3.Distance(transform.localPosition, Player.transform.localPosition);
		
		//-----------------------------------------------------------------------------------------------------------
		
		if(PlayerDistance < AttackDistance && !GetComponent<AnimationManager>().animationComp.isPlaying){
			gameObject.SendMessage("CustomAnimation", "attack");
		}
		
		if(PlayerDistance > AttackDistance){
			
			if(!GetComponent<AnimationManager>().animationComp.isPlaying){
				gameObject.SendMessage("CustomAnimation", "run");
			}
			
			if(Physics.Raycast(transform.localPosition, Player.transform.localPosition - transform.localPosition, out hit, Vector3.Distance(transform.localPosition, Player.transform.localPosition)-1) && hit.transform.name != "Player"){
				
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
				Vector3 dir = Player.transform.localPosition - transform.localPosition;
				
				dir.y = 0; // make direction strictly horizontal
				dir.Normalize(); // normalize it
				
				// turn gradually to the target each frame:
				transform.forward = Vector3.Slerp(transform.forward, dir, TurnSpeed * Time.deltaTime);
				
				Vector3 forward = transform.TransformDirection(Vector3.forward);
				controller.SimpleMove(forward * Speed);
				
			}
			
		}
		
	}
	
	
	
	public void AddSpot(){
		
		if(Player != null && Spots[Spots.Count-1] != Player.transform.localPosition){
			Spots.Add(Player.transform.localPosition);
		}
		
	}	
	
	
}
