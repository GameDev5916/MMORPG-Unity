using UnityEngine;
using System.Collections;

public class ObjectMove : MonoBehaviour {

	public float movedownY;
	public float movedownX;
	public float movedownZ;

	public string XXXXXXXXXXXXXXXX = "XXXXXXXXXXXXXXXX";

	public float sensitivityY = 1;
	public float sensitivityX = 1;
	public float sensitivityZ = 2;


	void  Update (){
		
		movedownY += Input.GetAxis("Mouse Y") * sensitivityY;
		movedownX += Input.GetAxis("Mouse X") * sensitivityX;
		movedownZ += Input.GetAxis("Mouse ScrollWheel") * sensitivityZ;
		
		if (Input.GetMouseButton(0)){
			
			transform.Translate(Vector3.forward * movedownY);
			transform.Translate(Vector3.right * movedownX);
			
			transform.Translate(Vector2.up * movedownZ);
			
		}
		
		movedownY = 0;
		movedownX = 0;
		movedownZ = 0;
		
	}

}
