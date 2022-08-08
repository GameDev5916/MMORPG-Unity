using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Translate(Input.GetAxis("Horizontal")/10, 0, Input.GetAxis("Vertical")/10);

		if(Input.GetKey(KeyCode.R)){
			transform.Translate(0, .1f, 0);
		}

		if(Input.GetKey(KeyCode.F)){
			transform.Translate(0, -.1f, 0);
		}

	}
}
