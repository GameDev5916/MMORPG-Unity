using UnityEngine;
using System.Collections;

public class LifeSource : MonoBehaviour {

	// Use this for initialization
	void Start () {

		gameObject.SendMessage("Wait");

	}
	
	void FixedUpdate () {

		if(gameObject.name == "LifeMeter"){

			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0,0,-(GameObject.Find("Character").GetComponent<Player>().MomentaryLife*180)/GameObject.Find("Character").GetComponent<Player>().MaxLife), 0.2f);
			
		}
		else{

			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0,0,(GameObject.Find("Character").GetComponent<Player>().MomentarySource*180)/GameObject.Find("Character").GetComponent<Player>().MaxSource), 0.2f);
			
		}
	
	}
	
	
	IEnumerator Wait() {
		
		yield return new WaitForSeconds(0.01f);
		
		transform.position =  GameObject.FindWithTag("Player").GetComponent<swap_cams>().ActiveCam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width/2, 0, transform.localPosition.z));

	}
	
}
