using UnityEngine;
using System.Collections;

public class OnCollision : MonoBehaviour {

	public GameObject Explosion;
	public int Time;

	void OnCollisionEnter(){

		Explosion.SetActive(true);

		if(GetComponent<Rigidbody>()){
			Destroy(GetComponent<Rigidbody>());
		}

		gameObject.SendMessage("Destroy");

	}

	IEnumerator Destroy () {

		yield return new WaitForSeconds(Time);
		Destroy(gameObject);

	}

}
