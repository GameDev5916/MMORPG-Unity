using UnityEngine;
using System.Collections;

public class DrunkAim : MonoBehaviour {
	
	public Texture texture;
	public int lerpspeed;
	public float size, speed, rotspeed;
	public Vector2 toPos = new Vector2(-21, -20);

	float i, lerp;
	int to;
	bool activo;
	
	// Update is called once per frame
	void Update () {

		if((GetComponent<GUITexture>().pixelInset.x <= 500 && GetComponent<GUITexture>().pixelInset.x >= -500) && (GetComponent<GUITexture>().pixelInset.y <= 200 && GetComponent<GUITexture>().pixelInset.y >= -300)){
			Vector2 pos = new Vector2(GetComponent<GUITexture>().pixelInset.x - Input.GetAxis("Mouse X")*10, GetComponent<GUITexture>().pixelInset.y - Input.GetAxis("Mouse Y")*10);
			GetComponent<GUITexture>().pixelInset = new Rect(pos.x, pos.y, GetComponent<GUITexture>().pixelInset.width, GetComponent<GUITexture>().pixelInset.height);
		}

		Vector2 pose = Vector2.Lerp(GetComponent<GUITexture>().pixelInset.position, toPos, Time.deltaTime*GameObject.Find("Character").GetComponent<Player>().Steadiness);
		GetComponent<GUITexture>().pixelInset = new Rect(pose.x, pose.y, GetComponent<GUITexture>().pixelInset.width, GetComponent<GUITexture>().pixelInset.height);

		
		if(GameObject.FindWithTag("Player").GetComponent<swap_cams>().ActiveCam.GetComponent<Pickup>().TempItem && (GameObject.FindWithTag("Player").GetComponent<swap_cams>().ActiveCam.GetComponent<Pickup>().TempItem.tag == "Monster" || GameObject.FindWithTag("Player").GetComponent<swap_cams>().ActiveCam.GetComponent<Pickup>().TempItem.tag == "Coins" || GameObject.FindWithTag("Player").GetComponent<swap_cams>().ActiveCam.GetComponent<Pickup>().TempItem.tag == "Item" || GameObject.FindWithTag("Player").GetComponent<swap_cams>().ActiveCam.GetComponent<Pickup>().TempItem.tag == "Skill" || GameObject.FindWithTag("Player").GetComponent<swap_cams>().ActiveCam.GetComponent<Pickup>().TempItem.tag == "Machine")){
			activo = true;
		}
		else{
			activo = false;
		}

		
		if(activo){
			
			if(size <= 76){
				to = 0;
			}
			else if(size >= 99){
				to = 1;
			}
			
			if(to == 0){
				size = Mathf.Lerp(size, 100, Time.deltaTime*speed);
			}
			else{
				size = Mathf.Lerp(size, 75, Time.deltaTime*speed);
			}
			
			i += rotspeed;
			
			if(i > 360){
				i = 0;
			}
			
		}
		else{
			
			size = 100;
			lerp = Mathf.Lerp(i, 0, Time.deltaTime*lerpspeed);
			i = lerp;
		}
		
	}
	
	void OnGUI () {
		
		if(activo){	
			
			GUIUtility.RotateAroundPivot(i, new Vector2(Screen.width/2, Screen.height/2));
			GUI.DrawTexture(new Rect((Screen.width/2)-size/2, (Screen.height/2)-size/2, size, size), texture);
			GUIUtility.RotateAroundPivot(-i, new Vector2(Screen.width/2, Screen.height/2));
			
		}
		else{
			
			GUIUtility.RotateAroundPivot(lerp, new Vector2(Screen.width/2, Screen.height/2));
			GUI.DrawTexture(new Rect((Screen.width/2)-size/2, (Screen.height/2)-size/2, size, size), texture);

		}
		
	}
	
	
}
