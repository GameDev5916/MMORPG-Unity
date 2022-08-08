using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {

	bool go;

	public GameObject lighte;
	public Color EndColor;

	// Update is called once per frame
	void FixedUpdate () {
	
		if(go){
			lighte.GetComponent<Light>().color = Color.Lerp(lighte.GetComponent<Light>().color, EndColor, Time.deltaTime * .1f);
		}

	}

	void OnTriggerEnter (Collider col) {

		if(col.tag == "Player" && lighte.GetComponent<Light>().color != EndColor){
			go = true;
		}

	}

	void OnTriggerExit(Collider col){

		if(col.tag == "Player" && lighte.GetComponent<Light>().color != EndColor){
			go = false;
		}

	}

}
