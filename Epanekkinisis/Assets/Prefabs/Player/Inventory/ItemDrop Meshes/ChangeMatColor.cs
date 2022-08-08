using UnityEngine;
using System.Collections;

public class ChangeMatColor : MonoBehaviour {
	
	public Renderer[] Materials;
	
	// Use this for initialization
	void Start () {

		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		string grade = GetComponent<Item>().Grade;
		
		if(grade == "Simplex"){
			transform.FindChild("Point light").GetComponent<Light>().color = Color.white;
		}
		else if(grade == "Enhanced"){
			transform.FindChild("Point light").GetComponent<Light>().color = Color.green;
		}
		else if(grade == "Enchanted"){
			transform.FindChild("Point light").GetComponent<Light>().color = Color.blue;
		}
		else if(grade == "Malfeasant"){
			transform.FindChild("Point light").GetComponent<Light>().color = Color.red;
		}
		else if(grade == "Extinct"){
			transform.FindChild("Point light").GetComponent<Light>().color = new Color32(255, 128, 0, 255);
		}
		else if(grade == "Unique"){
			transform.FindChild("Point light").GetComponent<Light>().color = Color.yellow;
		}
				
		for(int i = 0; i <= Materials.Length-1; i++){
			Materials[i].material.SetColor("_Color", transform.FindChild("Point light").GetComponent<Light>().color);			
		}
		
	}


	void OnCollisionEnter(Collision col){

		if(col.gameObject.name.Contains("Terrain")){
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
		}

	}
	
	
}
