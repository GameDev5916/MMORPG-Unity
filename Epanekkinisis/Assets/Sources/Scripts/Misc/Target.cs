using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	RaycastHit hit;
	public Vector3 target;

	// Update is called once per frame
	void Update () {	

		Ray ray = GameObject.FindWithTag("Player").GetComponent<swap_cams>().ActiveCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(GetComponent<GUITexture>().GetScreenRect().position.x/Screen.width, GetComponent<GUITexture>().GetScreenRect().position.y/Screen.height, 0));

		if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity)){
			target = hit.point;
		}

	}

	public IEnumerator Think (string Text, int Secs){
	
		GetComponent<GUIText>().text = Text;		
		yield return new WaitForSeconds(Secs);		
		GetComponent<GUIText>().text = "";

	}
	
}
