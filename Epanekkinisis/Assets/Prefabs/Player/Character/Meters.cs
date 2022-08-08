using UnityEngine;
using System.Collections;

public class Meters : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
	
		transform.parent.GetComponent<swap_cams>().maincam.GetComponent<MotionBlur>().blurAmount = 0.8f - (GetComponent<Player>().MomentaryLife * (0.8f / (GetComponent<Player>().MaxLife/2)));
		transform.parent.GetComponent<swap_cams>().maincam.GetComponent<ContrastStretchEffect>().limitMinimum = 0.15f - (GetComponent<Player>().MomentaryLife * (0.15f / (GetComponent<Player>().MaxLife/2)));

		transform.parent.GetComponent<swap_cams>().maincam.GetComponent<FrostEffect>().FrostAmount = 0.45f - (GetComponent<Player>().MomentarySource * (0.45f / (GetComponent<Player>().MaxSource/2)));

		//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		transform.parent.GetComponent<swap_cams>().secondcam.GetComponent<MotionBlur>().blurAmount = 0.8f - (GetComponent<Player>().MomentaryLife * (0.8f / (GetComponent<Player>().MaxLife/2)));
		transform.parent.GetComponent<swap_cams>().secondcam.GetComponent<ContrastStretchEffect>().limitMinimum = 0.15f - (GetComponent<Player>().MomentaryLife * (0.15f / (GetComponent<Player>().MaxLife/2)));
		
		transform.parent.GetComponent<swap_cams>().secondcam.GetComponent<FrostEffect>().FrostAmount = 0.45f - (GetComponent<Player>().MomentarySource * (0.45f / (GetComponent<Player>().MaxSource/2)));

	}

}
