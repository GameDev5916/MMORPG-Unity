using UnityEngine;
using System.Collections;

public class Slowmo : MonoBehaviour {

	public float smooth;
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Fire1")) {

			if (Time.timeScale == 1.0F) {

				Time.timeScale = 0.7F;
				Time.fixedDeltaTime = 0.02F * Time.timeScale;

			}
			else{

				Time.timeScale = 1.0F;
				Time.fixedDeltaTime = 0.02F * Time.timeScale;

			}

		}

	}

}
