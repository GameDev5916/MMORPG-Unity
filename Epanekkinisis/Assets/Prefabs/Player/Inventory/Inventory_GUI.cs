using UnityEngine;
using System.Collections;

public class Inventory_GUI : MonoBehaviour {
	
	Rect InventoryWindow = new Rect(Screen.width/2 - 950/2, Screen.height/2 - 640/2, 950, 640);
	public GUISkin skin;
	public bool render;
	
	public GUIStyle grade;
	
	public Texture man;
	public Rect manPos;
	
	public Rect[] Buttons, Areas;
	public int[] ButtonsAngles;
	
	public int k, l;
	int o, y, areaindex, index1;
	
	public Rect[] Stats;
	public string[] StatsContent;
	
	public Rect txt, dlt;
	string dltxt = "Delete Item";
	
	public Item ItemStatsToShow;
	


	// Update is called once per frame
	void OnGUI () {
		
		if(render){
			GUI.skin = skin;	
			InventoryWindow = GUI.Window (1, InventoryWindow, InventoryWindowFunction, "I n v e n t o r y");		
		}
		
	}
	
	
	void InventoryWindowFunction (int windowID){
		
		GUI.DrawTexture(manPos, man);
		
		for(int i = 0; i <= 38; i++){	

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
			
			if(i >= 29){
				GUIUtility.RotateAroundPivot(ButtonsAngles[i-29], Buttons[i].position + new Vector2(25, 25));								
				GUI.Button(Buttons[i], GetComponent<Inventory_Functions>().Items[i].Icon);
				GUIUtility.RotateAroundPivot(-ButtonsAngles[i-29], Buttons[i].position + new Vector2(25, 25));
			}
			else{
				GUI.Button(Buttons[i], GetComponent<Inventory_Functions>().Items[i].Icon);
			}

			Areas[i] = new Rect(Buttons[i].x + InventoryWindow.x, Buttons[i].y + InventoryWindow.y, 50, 50);

			GUI.skin = skin;

		}
		
		
		
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		
		
		
		for(int i = 0; i <= Stats.Length-1; i++){
			
			if(ItemStatsToShow.ItemName == ""){
				
				StatsContent[0] = "No Item Selected";
				StatsContent[1] = "Level: ~";
				StatsContent[2] = "Type: ~";
				StatsContent[3] = "Coins: ~";
				StatsContent[4] = "Life: ~";
				StatsContent[5] = "Armor: ~";
				StatsContent[6] = "Damage: ~";
				StatsContent[7] = "Source: ~";
				StatsContent[8] = "Steadiness: ~";
				StatsContent[9] = "Acceleration: ~";
				StatsContent[10] = "Revitalization: ~";
				StatsContent[11] = "Death Resistance: ~";
				StatsContent[12] = "";

			}
			else{			
				
				StatsContent[0] = ItemStatsToShow.ItemName;
				StatsContent[1] = "Level: " + ItemStatsToShow.Level;
				StatsContent[2] = "Type: " + ItemStatsToShow.Type;
				StatsContent[3] = "Coins: " + ItemStatsToShow.Coins;
				StatsContent[4] = "Life: " + ItemStatsToShow.Life;
				StatsContent[5] = "Armor: " + ItemStatsToShow.Armor;
				StatsContent[6] = "Damage: " + ItemStatsToShow.Damage;
				StatsContent[7] = "Source: " + ItemStatsToShow.Source;
				StatsContent[8] = "Steadiness: " + ItemStatsToShow.Steadiness + " %";
				StatsContent[9] = "Acceleration: " + ItemStatsToShow.Acceleration + " %";
				StatsContent[10] = "Revitalization: " + ItemStatsToShow.Revitalization + " %";
				StatsContent[11] = "Death Resistance: " + ItemStatsToShow.DeathRes + " %";
				StatsContent[12] = ItemStatsToShow.Enchantment;

			}
			
			
			Stats[i].size = new Vector2(Stats[i].size.x, 35);
			GUI.Label(Stats[i], StatsContent[i], grade);
			
		}
		
		GUI.TextArea(txt, StatsContent[12]);
		
		if(GUI.Button(dlt, dltxt) && GetComponent<Inventory_Functions>().Items[GetComponent<Inventory_Functions>().ChoosenItem].Icon){
			
			if(dltxt == "R U SURE?"){

				GetComponent<Inventory_Functions>().RemoveItem(GetComponent<Inventory_Functions>().ChoosenItem);

				ItemStatsToShow.ItemID = 0;
				ItemStatsToShow.PlayerID = 0;						
				ItemStatsToShow.ItemName = "";		
				ItemStatsToShow.Class = "";
				ItemStatsToShow.Coins = 0;							
				ItemStatsToShow.Level = 0;							
				ItemStatsToShow.Icon = null;
				ItemStatsToShow.Icon = null;
				ItemStatsToShow.Type = "";								
				ItemStatsToShow.Enchantment = "";						
				ItemStatsToShow.Model = null;								
				ItemStatsToShow.Grade = "";
				ItemStatsToShow.Slot = 0;
				
				//Stats
				
				ItemStatsToShow.Life = 0;				 				
				ItemStatsToShow.Armor = 0;
				ItemStatsToShow.Damage = 0;
				ItemStatsToShow.Source = 0;								
				ItemStatsToShow.Steadiness = 0;   			
				ItemStatsToShow.Revitalization = 0;    	 				
				ItemStatsToShow.Acceleration = 0;         			
				ItemStatsToShow.DeathRes = 0; 

			}
			else{
				dltxt = "R U SURE?";
			}
			
		}
		
	}
	
}
