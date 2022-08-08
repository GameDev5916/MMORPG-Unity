using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {
	
	Rect NPCShopWindow = new Rect(0, -20, 550, 600);
	public GUISkin skin;
	public bool render;
	public GameObject CurrentNPC;
	public GUIStyle Grade;
	
	public Rect Icon, Name, Price;

	public Rect[] Types;
	public Texture Line;

	string type = "Hat";
	int ChoosenItem;

	
	void OnGUI () {
		
		if(render){
			GUI.skin = skin;	
			NPCShopWindow = GUI.Window (5, NPCShopWindow, NPCShopWindowFunction, "S h o p");		
		}
		
	}
	
	void NPCShopWindowFunction (int windowID){
	
		//---------------------------------------
		
		if(GUI.Button(Types[0], "Hat")){
			type = "Hat";
		}
		if(GUI.Button(Types[1], "Pauldrons")){
			type = "Pauldrons";
		}
		if(GUI.Button(Types[2], "Left Handed")){
			type = "Left Handed";
		}
		if(GUI.Button(Types[3], "Right Handed")){
			type = "Right Handed";
		}
		if(GUI.Button(Types[4], "Panoply")){
			type = "Panoply";
		}
		if(GUI.Button(Types[5], "Belt")){
			type = "Belt";
		}
		if(GUI.Button(Types[6], "Boots")){
			type = "Boots";
		}
		if(GUI.Button(Types[7], "Spirit")){
			type = "Spirit";
		}
		if(GUI.Button(Types[8], "Two Handed")){
			type = "Two Handed";
		}
		if(GUI.Button(Types[9], "Gloves")){
			type = "Gloves";
		}
		
		//---------------------------------------
		
		GUI.DrawTexture(new Rect(25, 175, 500, 40), Line);
		
		//---------------------------------------
		
		Icon.position = new Vector2(70, 210);
		Name.position = new Vector2(121, 210);
		Price.position = new Vector2(120, 235);

		for(int i = 0; i <= CurrentNPC.GetComponent<NPC>().ShopItems.Count-1; i++){

			if(CurrentNPC.GetComponent<NPC>().ShopItems[i].Level <= transform.parent.GetComponentInChildren<Player>().Level && CurrentNPC.GetComponent<NPC>().ShopItems[i].Type == type && CurrentNPC.GetComponent<NPC>().ShopItems[i].Class == transform.parent.GetComponentInChildren<Player>().Classi){
				
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
				
				if(GUI.Button(Icon, CurrentNPC.GetComponent<NPC>().ShopItems[i].Icon)){	
					ChoosenItem = i;
					transform.parent.GetComponentInChildren<Inventory_Functions>().PreviewItem(CurrentNPC.GetComponent<NPC>().ShopItems[i]);
				}
				
				GUI.skin = skin;
				
				if(CurrentNPC.GetComponent<NPC>().ShopItems[i].Grade == "Simplex" || CurrentNPC.GetComponent<NPC>().ShopItems[i].Grade == ""){					
					Grade.normal.textColor = Color.white;					
				}
				else if(CurrentNPC.GetComponent<NPC>().ShopItems[i].Grade == "Enhanced"){					
					Grade.normal.textColor = Color.green;					
				}
				else if(CurrentNPC.GetComponent<NPC>().ShopItems[i].Grade == "Enchanted"){					
					Grade.normal.textColor = Color.cyan;					
				}
				else if(CurrentNPC.GetComponent<NPC>().ShopItems[i].Grade == "Malfeasant"){					
					Grade.normal.textColor = Color.red;					
				}
				else if(CurrentNPC.GetComponent<NPC>().ShopItems[i].Grade == "Extinct"){					
					Grade.normal.textColor = new Color32(255, 128, 0, 255);	
				}
				else if(CurrentNPC.GetComponent<NPC>().ShopItems[i].Grade == "Unique"){					
					Grade.normal.textColor = Color.yellow;					
				}
				
				GUI.Label(Name, CurrentNPC.GetComponent<NPC>().ShopItems[i].ItemName, Grade);
				
				Grade.normal.textColor = Color.white;
				GUI.Label(Price, CurrentNPC.GetComponent<NPC>().ShopItems[i].Coins.ToString() + " Coins", Grade);


				if(i % 2 == 0){

					Icon.x -= 210;
					Name.x -= 210;
					Price.x -= 210;
					
					Icon.y += 60;
					Name.y += 60;
					Price.y += 60;

				}
				else{

					Icon.x += 210;
					Name.x += 210;
					Price.x += 210;

				}
				
			}
			
		}


		if(GUI.Button(new Rect(265, 510, 100, 40), "Buy") && transform.parent.GetComponentInChildren<Player>().Coins >= CurrentNPC.GetComponent<NPC>().ShopItems[ChoosenItem].Coins){
			transform.parent.GetComponentInChildren<Inventory_Functions>().AddItem(CurrentNPC.GetComponent<NPC>().ShopItems[ChoosenItem], false, 666);				
		}
		if(GUI.Button(new Rect(175, 510, 100, 40), "Sell")){
			transform.parent.GetComponentInChildren<Player>().AddCoins(transform.parent.GetComponentInChildren<Inventory_Functions>().Items[transform.parent.GetComponentInChildren<Inventory_Functions>().ChoosenItem].Coins);
			transform.parent.GetComponentInChildren<Inventory_Functions>().RemoveItem(transform.parent.GetComponentInChildren<Inventory_Functions>().ChoosenItem);
		}

		
	}
	
}
