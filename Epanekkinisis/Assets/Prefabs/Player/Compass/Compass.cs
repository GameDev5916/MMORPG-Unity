using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour {

	public bool render;

	public GUISkin skin;
	
	public Texture compass;
	
	public Rect comPos;
	public Rect lbl1;
	public Rect lbl2;
	public Rect lbl3;

	Vector3 angles;

	int momsource, maxsource, maxlife, momlife;

	// Update is called once per frame
	void Update () {

		transform.GetChild(0).LookAt(new Vector3(0, 0, GameObject.Find("North").transform.position.z));
		angles = transform.GetChild(0).transform.rotation.eulerAngles - transform.parent.rotation.eulerAngles;

		momsource = GameObject.FindWithTag("Player").GetComponentInChildren<Player>().MomentarySource;
		momlife = GameObject.FindWithTag("Player").GetComponentInChildren<Player>().MomentaryLife;
		maxlife = GameObject.FindWithTag("Player").GetComponentInChildren<Player>().MaxLife;
		maxsource = GameObject.FindWithTag("Player").GetComponentInChildren<Player>().MaxSource;
	}

	
	// Use this for initialization
	void OnGUI () {
				
		if(render){

			GUI.skin = skin;

			GUIUtility.RotateAroundPivot(angles.y, new Vector2(44, 45));
			GUI.DrawTexture(comPos, compass);
			GUIUtility.RotateAroundPivot(-angles.y, new Vector2(44, 45));
			
			GUI.Label(lbl1, "Life: " + (momlife*100)/maxlife + "%");
			GUI.Label(lbl2, "Source: " + (momsource*100)/maxsource + "%");
			
			if(momlife <= (momlife*5)/100){
				GUI.Label(lbl1, "Status: Fucked Up");
			}
			
			if(momlife > (momlife*5)/100 && momlife <= (momlife*80)/100){
				GUI.Label(lbl2, "Status: Yeh...");
			}
			
			if(momlife > (momlife*80)/100){
				GUI.Label(lbl3, "Status: Fine");
			}
			
		}
		
	}

}
