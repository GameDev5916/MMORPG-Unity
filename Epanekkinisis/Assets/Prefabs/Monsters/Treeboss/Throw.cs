using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour {
	
	public GameObject rock;	
	public int speed;
	
	public void Throwe(int damage, int aim){

		GameObject rock1 = Instantiate(rock, transform.position, transform.localRotation) as GameObject;
		rock1.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
		
		rock1.GetComponent<Shot>().Damage = damage;
		
	}
	
}
