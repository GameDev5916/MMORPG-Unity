using UnityEngine;
using System.Collections;

public class swap_cams : MonoBehaviour {
	
	public GameObject secondcam;
	public GameObject maincam;
	bool cam = false;
	public GameObject ActiveCam;


	// Use this for initialization
	void Start () {		
		ActiveCam = maincam;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyUp(KeyCode.X)){
			
			if(cam == false){

				maincam.SetActive(false);
				secondcam.SetActive(true);
				cam = true;
				ActiveCam = secondcam;
				
			}
			else if(cam == true){

				maincam.SetActive(true);
				secondcam.SetActive(false);
				cam = false;
				ActiveCam = maincam;
				
			}

			GetComponent<Mouse>().Check();

		}
		
	}
	
	
}
