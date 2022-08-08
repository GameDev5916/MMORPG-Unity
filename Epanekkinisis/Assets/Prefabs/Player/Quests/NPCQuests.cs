using System;
using UnityEngine;
using System.Collections;

public class NPCQuests : MonoBehaviour {

	Rect NPCQuestsWindow = new Rect(0, -20, 550, 600);
	public GUISkin skin;
	public bool render;
	public GameObject CurrentNPC;
	public Texture LineTexture, NoQuests;
	
	float x, y;
	bool HasIt;
	public int ChoosenQuest;
	
	
	void OnGUI () {
		
		if(render){
			GUI.skin = skin;	
			NPCQuestsWindow = GUI.Window (2, NPCQuestsWindow, NPCQuestsWindowFunction, CurrentNPC.GetComponent<NPC>().Name);		
		}
		
	}
	
	
	void NPCQuestsWindowFunction (int windowID){

		GUI.DrawTexture(new Rect(260, 95, 40, 475), LineTexture);

		bool DoneAll = true;

		for(int i = 0; i <= CurrentNPC.GetComponent<NPC>().Quests.Count-1; i++){
			if(Info.Quests[CurrentNPC.GetComponent<NPC>().Quests[i]].Finished == 0){
				DoneAll = false;
			}
		}

		if(DoneAll){
			ChoosenQuest = 666;
		}
		else{
			ChoosenQuest = CurrentNPC.GetComponent<NPC>().Quests[0];
		}

		// ----------------------------------- QUEST BUTTONS
		
		x = 40;
		y = 155;
		
		for(int i = 0; i <= CurrentNPC.GetComponent<NPC>().Quests.Count-1; i++){
			
			if(Info.Quests[CurrentNPC.GetComponent<NPC>().Quests[i]].Finished == 0 && Info.Quests[Info.Quests[CurrentNPC.GetComponent<NPC>().Quests[i]].QuestToBeDoneID].Finished == 1 && Info.Quests[i].Level <= transform.parent.GetComponentInChildren<Player>().Level){	//Tsekarei level kai an tin exei kanei
				
				if(GUI.Button(new Rect(x, y, 230, 65), Info.Quests[CurrentNPC.GetComponent<NPC>().Quests[i]].Name)){
					ChoosenQuest = CurrentNPC.GetComponent<NPC>().Quests[i];
				}
				
				y += 47.5f;
				
			}
			
		}
		
		
		
		// ---------------------------------------- SHOP BUTTON IF HAS
		
		if(CurrentNPC.GetComponent<NPC>().ShopItems.Count > 0){
			
			if(GUI.Button(new Rect(x, y, 230, 65), "Shop")){				
				transform.parent.GetComponent<Mouse>().Shop();				
			}
			
		}
		
		
		
		
		
		// -------------------------------- QUEST GUI
		
		
		if(ChoosenQuest == 666){
			GUI.DrawTexture(new Rect(305, 150, 200, 318), NoQuests);						
		}
		else{
									
			GUI.Label(new Rect(300, 115, 200, 40), Info.Quests[ChoosenQuest].Name);
			
			if(Info.Quests[ChoosenQuest].Done == 0){
				GUI.TextArea(new Rect(300, 160, 200, 230), Info.Quests[ChoosenQuest].ADescription);
				GUI.Toggle(new Rect(375, 397, 60, 20), false, "Done");
			}
			else{
				GUI.TextArea(new Rect(300, 160, 200, 230), Info.Quests[ChoosenQuest].BDescription);
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
			
			
			HasIt = false;
			
			for(int i = 0; i <= GetComponent<Quests>().PlayerQuests.Count-1; i++){	
				
				if(ChoosenQuest == GetComponent<Quests>().PlayerQuests[i]){
					HasIt = true;
					break;
				}			
				
			}
			
			if(!HasIt){
				
				if(GUI.Button(new Rect(335, 490, 130, 50), "Accept") && GetComponent<Quests>().PlayerQuests.Count <= 5){

					GetComponent<Quests>().PlayerQuests.Add(ChoosenQuest);

					if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){	
						GameObject.Find("Southbridge").GetComponent<Southbridge>().AddQuest(Info.Quests[ChoosenQuest].QuestID);
					}
					else{
						GameObject.Find("GameManager").GetComponent<PlayersManager>().AddQuest(Info.Quests[ChoosenQuest].QuestID);
					}

				}
				
			}
			else if(HasIt && Info.Quests[ChoosenQuest].Done == 1){
				
				if(GUI.Button(new Rect(335, 490, 130, 50), "Finish")){

					transform.parent.GetComponentInChildren<Player>().AddXP(Info.Quests[ChoosenQuest].Xp);
					transform.parent.GetComponentInChildren<Player>().AddCoins(Info.Quests[ChoosenQuest].Coins);

					if(Info.Quests[ChoosenQuest].GivesItem){
						transform.parent.GetComponentInChildren<Inventory_Functions>().AddItem(Info.Quests[ChoosenQuest].Reward.GetComponent<Item>(), false, 666);
					}

					Info.Quests[ChoosenQuest].Finished = 1;
					GetComponent<Quests>().PlayerQuests.Remove(ChoosenQuest);

					if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){	
						GameObject.Find("Southbridge").GetComponent<Southbridge>().FinishQuest(Info.Quests[ChoosenQuest].QuestID);
					}
					else{
						GameObject.Find("GameManager").GetComponent<PlayersManager>().FinishQuest(Info.Quests[ChoosenQuest].QuestID);
					}

				}
				
			}
			
		}
		
	}
	
	
}
