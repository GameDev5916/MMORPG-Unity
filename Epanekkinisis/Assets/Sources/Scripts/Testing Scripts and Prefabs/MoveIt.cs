using UnityEngine;
using System.Collections;

public class MoveIt : MonoBehaviour {
	
	public float speed;

	void Start () {
		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update () {	
		GetComponent<CharacterController>().Move(GetComponent<swap_cams>().ActiveCam.transform.forward * speed * Input.GetAxis("Vertical"));
	}

}
