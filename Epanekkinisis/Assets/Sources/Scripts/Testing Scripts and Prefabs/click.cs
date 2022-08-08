using UnityEngine;
using System.Collections;

public class click : MonoBehaviour {
	
	public GameObject go;
	public GameObject go2;
	public GameObject go3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButtonUp(0)){

			//GameObject.Find("Item").GetComponent<Item>().Class = GameObject.Find("Item2").GetComponent<Item>().Class; 

			GameObject.Find("Main Camera").GetComponent<_CameraShake>().DoShake();

		}

		/*if(Input.GetMouseButtonDown(1) && (GetComponent<Pickup>().TempItem.tag == "Monster" || GetComponent<Pickup>().TempItem.tag == "Machine")){
		
			GameObject gm = Instantiate(go, transform.position, new Quaternion(transform.rotation.x, Random.Range(-60, 60), Random.Range(0, 360), 0)) as GameObject;
			
		}
			
		if(Input.GetMouseButtonDown(0) && (GetComponent<Pickup>().TempItem.tag == "Monster" || GetComponent<Pickup>().TempItem.tag == "Machine")){
		
			GameObject gm = Instantiate(go2, transform.position, transform.rotation) as GameObject;
			
		}

		if(Input.GetMouseButtonDown(2)){

			GameObject gm = Instantiate(go3, transform.position, transform.rotation) as GameObject;

			gm.rigidbody.AddForce(transform.forward * 1000);

		}*/

	}
	
	
}
