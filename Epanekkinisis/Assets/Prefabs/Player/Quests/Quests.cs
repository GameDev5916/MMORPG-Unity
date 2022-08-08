using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quests : MonoBehaviour {

	Rect QuestsWindow = new Rect(Screen.width/2 - 550/2, Screen.height/2 - 300, 550, 600);
	public GUISkin skin;
	public bool render;
	public Texture LineTexture, NoQuests;
	
	float x, y;
	public int ChoosenQuest;
	
	public List<int> PlayerQuests = new List<int>();
	
	
	void OnGUI () {
		
		if(render){
			GUI.skin = skin;	
			QuestsWindow = GUI.Window (3, QuestsWindow, QuestsWindowFunction, "Q u e s t s");		
		}
		
	}
	
	
	void QuestsWindowFunction (int windowID){

		if(PlayerQuests.Count == 0){
			ChoosenQuest = 666;
		}
		else{
			ChoosenQuest = PlayerQuests[0];
		}

		if(ChoosenQuest == 666){ 			//An den exei quests
			GUI.DrawTexture(new Rect(110, 215, 350, 200), NoQuests);
		}
		else{

			GUI.DrawTexture(new Rect(260, 95, 40, 475), LineTexture);
			
			// ----------------------------------- QUEST BUTTONS
			
			x = 40;
			y = 155;
			
			for(int i = 0; i <= PlayerQuests.Count-1; i++){
				
				if(GUI.Button(new Rect(x, y, 230, 65), Info.Quests[PlayerQuests[i]].Name)){
					ChoosenQuest = PlayerQuests[i];
				}
				
				y += 47.5f;
				
			}
			
			
			
			// -------------------------------- QUEST GUI
			
			
			GUI.Label(new Rect(300, 115, 200, 40), Info.Quests[ChoosenQuest].Name);

			if(Info.Quests[ChoosenQuest].Done == 0){
				GUI.TextArea(new Rect(300, 160, 200, 230), Info.Quests[ChoosenQuest].BDescription);
				GUI.Toggle(new Rect(375, 397, 60, 20), false, "Done");
			}
			else{
				GUI.TextArea(new Rect(300, 160, 200, 230), Info.Quests[ChoosenQuest].ADescription);
				GUI.Toggle(new Rect(375, 397, 60, 20), true, "Done");
			}			
			
			if(Info.Quests[ChoosenQuest].GivesItem){
				
				GUI.Label(new Rect(295, 425, 150, 40), "Coins: " + Info.Quests[ChoosenQuest].Coins);
				GUI.Label(new Rect(295, 450, 150, 40), "XP: " + Info.Quests[ChoosenQuest].Xp);
				
				GUI.skin = null;
				
				GUI.skin.button.border.top = 6;
				GUI.skin.button.border.right = 6;
				GUI.skin.button.border.left = 6;
				GUI.skin.button.border.bottom = 6;
				GUI.skin.button.margin.top = 0;
				GUI.skin.button.margin.right = 0;
				GUI.skin.button.margin.left = 0;
				GUI.skin.button.margin.bottom = 0;
				GUI.skin.button.padding.top = 1;
				GUI.skin.button.padding.right = 1;
				GUI.skin.button.padding.left = 1;
				GUI.skin.button.padding.bottom = 1;
				
				if(GUI.Button(new Rect(450, 425, 50, 50), Info.Quests[ChoosenQuest].Reward.GetComponent<Item>().Icon)){				
					transform.parent.GetComponentInChildren<Inventory_Functions>().PreviewItem(Info.Quests[ChoosenQuest].Reward.GetComponent<Item>());
				}
				
				GUI.skin = skin;
				
			}
			else{
				
				GUI.Label(new Rect(325, 425, 150, 40), "Coins: " + Info.Quests[ChoosenQuest].Coins);
				GUI.Label(new Rect(325, 450, 150, 40), "XP: " + Info.Quests[ChoosenQuest].Xp);
				
			}


			if(GUI.Button(new Rect(300, 510, 200, 35), "I'm duffer to complete this quest")){

				GetComponent<Quests>().PlayerQuests.Remove(ChoosenQuest);

				if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){	
					GameObject.Find("Southbridge").GetComponent<Southbridge>().RemoveQuest(Info.Quests[ChoosenQuest].QuestID);
				}
				else{
					GameObject.Find("GameManager").GetComponent<PlayersManager>().RemoveQuest(Info.Quests[ChoosenQuest].QuestID);
				}

			}

		}
		
	}
	
	
}
