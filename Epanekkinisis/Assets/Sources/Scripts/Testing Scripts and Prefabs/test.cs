using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetButton("Fire1")){
			GetComponent<Rigidbody>().AddForce(transform.right * speed);

		}

	}
}
