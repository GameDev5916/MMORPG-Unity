using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
	
	public int PatrolSpeed;
	public bool patrol;
	
	CharacterController controller;
	public string direction;	
	
	public float sideDuration;
	public float forwardDuration;
	
	RaycastHit front;

	public Transform old;
	public bool rotate;
	Quaternion to;


	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(patrol){
			
			if(!GetComponent<AnimationManager>().animationComp.isPlaying || GetComponent<AnimationManager>().animationComp.IsPlaying("walk")){
				GetComponent<AnimationManager>().SendMessage("CustomAnimation", "run");
			}

			if(direction == "" && Physics.Raycast(transform.localPosition, transform.forward, out front, forwardDuration) && front.transform != old && (front.collider.tag != "Player" && front.collider.tag != "Dummy")){

				old = front.transform;
				gameObject.SendMessage("Direct");
				
			}
			else{
				controller.SimpleMove(transform.forward*PatrolSpeed);
			}

			if(rotate){
				Rotate(direction);
			}

		}
		
	}
	
	
	void Direct () {
		
		if(Physics.Raycast(transform.localPosition, transform.right, sideDuration) && !Physics.Raycast(transform.localPosition, transform.right*-1, sideDuration)){
			direction = "left";
			to = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z));
		}
		else if(Physics.Raycast(transform.localPosition, transform.right*-1, sideDuration) && !Physics.Raycast(transform.localPosition, transform.right, sideDuration)){
			direction = "right";
			to = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z));
		}
		else if(!Physics.Raycast(transform.localPosition, transform.right*-1, sideDuration) && !Physics.Raycast(transform.localPosition, transform.right, sideDuration)){
			
			int roll = Random.Range(1, 100);
			
			if(roll <= 50){
				direction = "right";
				to = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z));
			}
			else{
				direction = "left";
				to = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z));
			}
			
		}
		else if(Physics.Raycast(transform.localPosition, transform.right*-1, sideDuration) && Physics.Raycast(transform.localPosition, transform.right, sideDuration)){
			direction = "back";
			to = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z));
		}

		rotate = true;
		gameObject.SendMessage("Check");

		print(direction);

	}
	
	void Rotate(string direction){

		if(direction == "left"){
			transform.rotation = Quaternion.Slerp(transform.rotation, to, Time.deltaTime*4);
		}
		else if(direction == "right"){
			transform.rotation = Quaternion.Slerp(transform.rotation, to, Time.deltaTime*4);	
		}
		else if(direction == "back"){
			transform.rotation = Quaternion.Slerp(transform.rotation, to, Time.deltaTime*4);
		}
		
	}
	
	IEnumerator Check () {
		
		yield return new WaitForSeconds(1);
		direction = "";
		rotate = false;
		
	}
	
	
}
