using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skills : MonoBehaviour {
	
	Rect SkillsWindow = new Rect(Screen.width/2 - 950/2, Screen.height/2 - 645/2, 950, 645);
	public GUISkin skin;
	public bool render;
	int index;

	public string XXXXXXXXXXXXXXXXX = "XXXXXXXXXXXXXXXXXXXXXXX";
	
	public List<Skill> PlayerSkills = new List<Skill>();
	
	public string XXXXXXXXXXXXXXXXXX = "XXXXXXXXXXXXXXXXXXXXXXX";
	
	public Texture line;
	
	public string XXXXXXXXXXXXXXXXXXX = "XXXXXXXXXXXXXXXXXXXXXXX";
	
	public Texture spike;
	
	
	// Use this for initialization
	void OnGUI () {
		
		GUI.skin = skin;
		
		if(render){
			SkillsWindow = GUI.Window (6, SkillsWindow, SkillsFunction, "B o o k  o f  A r t s");		
		}

	}
	
	void SkillsFunction (int windowID){
		
		GUI.DrawTexture(new Rect(70, 110, 25, 455), line);
		GUI.DrawTexture(new Rect(860, 110, 25, 455), line);
		
		GUIUtility.RotateAroundPivot(135, new Vector2(90, 120));
		GUI.DrawTexture(new Rect(90, 120, 36, 44), spike);
		GUIUtility.RotateAroundPivot(-135, new Vector2(90, 120));
		
		GUIUtility.RotateAroundPivot(-135, new Vector2(880, 145));
		GUI.DrawTexture(new Rect(880, 145, 36, 44), spike);
		GUIUtility.RotateAroundPivot(135, new Vector2(880, 145));
		
		GUIUtility.RotateAroundPivot(-45, new Vector2(855, 560));
		GUI.DrawTexture(new Rect(855, 560, 36, 44), spike);
		GUIUtility.RotateAroundPivot(45, new Vector2(855, 560));
		
		GUIUtility.RotateAroundPivot(45, new Vector2(70, 535));
		GUI.DrawTexture(new Rect(70, 535, 36, 44), spike);
		GUIUtility.RotateAroundPivot(-45, new Vector2(70, 535));
		
		
		//Skills

		GUI.DrawTexture(new Rect(106, 140, 730, 400), PlayerSkills[index].Page);		

		GUI.Label(new Rect(245, 555, 155, 40), "Source: " + PlayerSkills[index].Source.ToString());

		if(PlayerSkills[index].Unlocked){
			GUI.Label(new Rect(570, 555, 155, 40), "Status: Unlocked");
		}
		else{
			GUI.Label(new Rect(570, 555, 155, 40), "Status: Locked");
		}

		if(GUI.Button(new Rect(390, 550, 100, 40), "<")){
			index--;
		}
		
		if(GUI.Button(new Rect(480, 550, 100, 40), ">")){
			index++;
		}
		
		if(index < 0){
			index = 0;
		}
		else if(index > 5){
			index = 5;
		}

	}


	public void UnlockSkill(int skillid){
		
		PlayerSkills[skillid].Unlocked = true;
		
		if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){
			GameObject.Find("Southbridge").GetComponent<Southbridge>().AddSkill(skillid);
		}
		else{
			GameObject.Find("GameManager").GetComponent<PlayersManager>().AddSkill(skillid);	
		}
		
	}


	public void RunSkill(string skill){

		for(int i = 0; i <= PlayerSkills.Count-1; i++){

			if(PlayerSkills[i].Name == skill && PlayerSkills[i].Unlocked){

				for(int x = 29; i <= 38; i++){

					if(transform.parent.GetComponentInChildren<Inventory_Functions>().Items[x].Type == PlayerSkills[i].Type){

						//instantiate and send message to network

						break;

					}

				}

				break;

			}

		}

	}










	
}
