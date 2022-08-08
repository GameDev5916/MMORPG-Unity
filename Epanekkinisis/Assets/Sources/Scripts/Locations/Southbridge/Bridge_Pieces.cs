using UnityEngine;
using System.Collections;

public class Bridge_Pieces : MonoBehaviour {

	void OnTriggerEnter (Collider col) {

		if(col.tag == "Shot"){
			GetComponent<Rigidbody>().useGravity = true;
		}

	}

}
