using UnityEngine;
using System.Collections;

public class ExplodeChance : MonoBehaviour {
	
	int chance;
	public GameObject explosion;

	// Update is called once per frame
	void OnCollisionEnter (Collision col) {
	
		if(col.collider.gameObject.tag == "Player"){
			
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(gameObject);
	
		}
			
	}
	
}
