using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class SetCamPos : MonoBehaviour {

	public GameObject waterBody;
	
	// Update is called once per frame
	void Update () {
	
		if (waterBody != null) {

			waterBody.GetComponent<Renderer>().sharedMaterial.SetVector ("_CamPos", gameObject.transform.position);
		}
	}
}
