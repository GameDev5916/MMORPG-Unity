using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	Rect CharacterWindow = new Rect (0, 0, 397, 620);
	public GUISkin customSkin;
	public bool render = false;
	public Texture basicstats;
	public Texture resistances;
	public Texture info;

	
	void OnGUI () {
		
		GUI.skin = customSkin;
		
		if(render){
			CharacterWindow = GUI.Window (0, CharacterWindow, WindowFunction, "C h a r a c t e r");
		}
		
	}

	
	void WindowFunction (int windowID) {

		//Char Stats
		GUI.DrawTexture(new Rect(20,133,205,25), info,  ScaleMode.ScaleAndCrop);
		GUI.Label (new Rect (200, 160, 150, 30), "Life: " + GetComponent<Player>().MomentaryLife + "/" + GetComponent<Player>().MaxLife);
		GUI.Label (new Rect (200, 190, 150, 30), "Source: " + GetComponent<Player>().MomentarySource + "/" + GetComponent<Player>().MaxSource);
		GUI.Label (new Rect (200, 220, 150, 30), "Armor: " + GetComponent<Player>().MomentaryArmor);
		GUI.Label (new Rect (200, 250, 150, 30), "Damage: " + GetComponent<Player>().MomentaryDamage);

		GUI.DrawTexture(new Rect(100,355,205,25), resistances,  ScaleMode.ScaleAndCrop);
		GUI.Label (new Rect (100, 380, 200, 30), "Steadiness: " + GetComponent<Player>().Steadiness + "%");
		GUI.Label (new Rect (100, 410, 200, 30), "Acceleration: " + GetComponent<Player>().Acceleration + "%");
		GUI.Label (new Rect (100, 440, 200, 30), "Revitalization: " + GetComponent<Player>().Revitalization + "%");
		GUI.Label (new Rect (100, 470, 200, 30), "Death Resistance: " + GetComponent<Player>().DeathRes + "%");

		//Char Stats 
		
		//Char Info
		GUI.DrawTexture(new Rect(175,135,205,25), basicstats,  ScaleMode.ScaleAndCrop);
		GUI.Label (new Rect (45, 160, 150, 30), GetComponent<Player>().Name);
		GUI.Label (new Rect (45, 190, 150, 30), GetComponent<Player>().Classi);
		GUI.Label (new Rect (45, 220, 150, 30), "Level: " + GetComponent<Player>().Level);
		GUI.Label (new Rect (45, 250, 150, 30), "Gender: " + GetComponent<Player>().Sex);
		GUI.Label (new Rect (45, 280, 150, 30), "Xp: " + GetComponent<Player>().Xp + " (" + (transform.parent.GetComponentInChildren<Player>().Xp*100)/Info.GetNextLevel(GetComponent<Player>().Level) + "%)");
		GUI.Label (new Rect (45, 310, 150, 30), "Coins: " + GetComponent<Player>().Coins + " Coins");
		//Char Info
		
	}
	
	
	
}
