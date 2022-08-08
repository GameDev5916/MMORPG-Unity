using UnityEngine;
using System.Collections;

public class spell2 : MonoBehaviour {
	
	public int speed;
	
	// Use this for initialization
	void Start () {
	
		GetComponent<Rigidbody>().AddForce(GameObject.FindWithTag("Player").GetComponent<swap_cams>().ActiveCam.transform.forward * speed);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
}
