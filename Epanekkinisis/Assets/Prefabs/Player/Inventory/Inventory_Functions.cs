using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory_Functions : MonoBehaviour {
	
	public List<Item> Items = new List<Item>();
	
	public int Hover;
	public int ChoosenItem;
	public int StartDrag;
	
	Texture DragIcon;
	public string[] Types;
	
	
	void Start () {

		for(int i = 0; i <= 39; i++){
			gameObject.AddComponent<Item>();	
		}
		
		for(int i = 0; i <= 38; i++){
			Items[i] = GetComponents<Item>()[i];	
		}
		
		GetComponent<Inventory_GUI>().ItemStatsToShow = GetComponents<Item>()[39];
		
	}
	
	
	// Update is called once per frame
	void OnGUI () {
		
		if(GetComponent<Inventory_GUI>().render){
			
			GUI.depth = -1;
			
			Hover = 666;
			
			for(int i = 0; i <= GetComponent<Inventory_GUI>().Areas.Length-1; i++){
				
				if(GetComponent<Inventory_GUI>().Areas[i].Contains(Event.current.mousePosition)){
					Hover = i;
				}
				
			}			
			
			if(DragIcon){	
				
				GUI.skin = null;
				
				Texture2D temp = GUI.skin.button.normal.background;
				GUI.skin.button.normal.background = null;
				GUI.Button(new Rect(Event.current.mousePosition.x-27.5f, Event.current.mousePosition.y-27.5f, 55, 55), DragIcon);	
				GUI.skin.button.normal.background = temp;
				
			}
			
		}
		
	}
	
	
	void Update () {
		
		if(GetComponent<Inventory_GUI>().render && Hover != 666){					

			if(Input.GetMouseButtonDown(0) && Items[Hover].Icon){

				DragIcon = Items[Hover].Icon;
				Items[Hover].Icon = null;
				StartDrag = Hover;

			}

			if(Input.GetMouseButtonUp(0) && DragIcon && !Items[Hover].Icon){
								
				Items[StartDrag].Icon = DragIcon;
				DragIcon = null;

				if(StartDrag == Hover){	//Preview

					PreviewItem(Items[StartDrag]);
					ChoosenItem = StartDrag;

				}

				if(StartDrag != Hover && Hover < 29){	//Change place in inventory - Unequip
					
					Swap(Items[StartDrag], Items[Hover]);
					ChoosenItem = Hover;
					
				}

				if(StartDrag != Hover && Hover >= 29 && Items[StartDrag].Type == Types[Hover-29] && Items[StartDrag].Level <= transform.parent.GetComponentInChildren<Player>().Level){	//Equip
					
					Swap(Items[StartDrag], Items[Hover]);
					ChoosenItem = Hover;
					
				}

			}

			if(Input.GetMouseButtonUp(1) && Items[Hover].Type == "Misc"){	//Instantiate Misc

				Destroy(GameObject.Find(Items[Hover].Model.name + "(Clone)"));
				Instantiate(Items[Hover].Model);
			}
			
		}
		
	}
	



	public void Swap (Item from, Item to){

		//Info
		
		to.ItemID = from.ItemID;
		to.PlayerID = from.PlayerID;
		to.ItemName = from.ItemName;
		to.Class = from.Class;
		to.Grade = from.Grade;
		to.Type = from.Type;	
		to.Coins = from.Coins;				
		to.Level = from.Level;						
		to.Icon = from.Icon;
		to.Enchantment = from.Enchantment;						
		to.Model = from.Model;
		to.Slot = Items.IndexOf(to);

		if(from.Slot < 29 && to.Slot >= 29){
			Instantiate(to.Model);
		}
		else if(from.Slot >= 29 && to.Slot < 29){
			Destroy(GameObject.Find(to.Model.name + "(Clone)"));
		}

		//Stats
		
		to.Life = from.Life;			 				
		to.Armor = from.Armor;
		to.Damage = from.Damage;
		to.Source = from.Source;						
		to.Steadiness = from.Steadiness;			
		to.Revitalization = from.Revitalization;				
		to.Acceleration = from.Acceleration;	
		to.DeathRes = from.DeathRes;


		if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){	
			GameObject.Find("Southbridge").GetComponent<Southbridge>().ItemStatus(to);
		}
		else{
			GameObject.Find("GameManager").GetComponent<PlayersManager>().DeleteItem(to);	
		}

		
		//--------------------------------------------
		
		
		from.ItemID = 0;
		from.PlayerID = 0;						
		from.ItemName = "";
		from.Class = "";
		from.Coins = 0;
		from.Level = 0;
		from.Icon = null;
		from.Type = "";						
		from.Enchantment = "";
		from.Model = null;
		from.Grade = "";
		from.Slot = 0;
		
		//Stats
		
		from.Life = 0;	 				
		from.Armor = 0;
		from.Damage = 0;
		from.Source = 0;
		from.Steadiness = 0;
		from.Revitalization = 0;		
		from.Acceleration = 0;
		from.DeathRes = 0;
		
	}



	
	public void AddItem (Item item, bool hasIt, int Slot){
		
		if(hasIt){
			
			Items[Slot].ItemID = item.ItemID;
			Items[Slot].PlayerID = Info.PlayerID;						
			Items[Slot].ItemName = item.ItemName;
			Items[Slot].Class = item.Class;
			Items[Slot].Coins = item.Coins;				
			Items[Slot].Level = item.Level;						
			Items[Slot].Icon = item.Icon;
			Items[Slot].Type = item.Type;						
			Items[Slot].Enchantment = item.Enchantment;						
			Items[Slot].Model = item.Model;			
			Items[Slot].Grade = item.Grade;
			Items[Slot].Slot = Slot;
			
			
			//Stats
			
			Items[Slot].Life = item.Life;			 				
			Items[Slot].Armor = item.Armor;
			Items[Slot].Damage = item.Damage;
			Items[Slot].Source = item.Source;						
			Items[Slot].Steadiness = item.Steadiness;			
			Items[Slot].Revitalization = item.Revitalization;				
			Items[Slot].Acceleration = item.Acceleration;	
			Items[Slot].DeathRes = item.DeathRes;
			
			if(Slot >= 29){
				Instantiate(Items[Slot].Model);
			}
			
		}
		else{
			
			for(int i = 0; i <= 28; i++){
				
				if(Items[i].ItemName == ""){
					
					//Info
					
					Items[i].ItemID = item.ItemID;
					Items[i].PlayerID = Info.PlayerID;						
					Items[i].ItemName = item.ItemName;
					Items[i].Class = item.Class;
					Items[i].Coins = item.Coins;				
					Items[i].Level = item.Level;						
					Items[i].Icon = item.Icon;
					Items[i].Type = item.Type;						
					Items[i].Enchantment = item.Enchantment;						
					Items[i].Model = item.Model;			
					Items[i].Grade = item.Grade;
					Items[i].Slot = Items.IndexOf(Items[i]);
					
					
					//Stats
					
					Items[i].Life = item.Life;			 				
					Items[i].Armor = item.Armor;
					Items[i].Damage = item.Damage;
					Items[i].Source = item.Source;						
					Items[i].Steadiness = item.Steadiness;			
					Items[i].Revitalization = item.Revitalization;				
					Items[i].Acceleration = item.Acceleration;	
					Items[i].DeathRes = item.DeathRes;
					
					
					if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){	
						GameObject.Find("Southbridge").GetComponent<Southbridge>().AddItem(Items[i]);
					}
					else{
						GameObject.Find("GameManager").GetComponent<PlayersManager>().AddItem(Items[i]);	
					}
					
					return;
					
				}
				
			}
			
		}
		
	}



	
	public void RemoveItem (int Slot){
			
			if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){	
				GameObject.Find("Southbridge").GetComponent<Southbridge>().DeleteItem(Items[Slot]);
			}
			else{
				GameObject.Find("GameManager").GetComponent<PlayersManager>().DeleteItem(Items[Slot]);	
			}
			
			//Info
			
			Items[Slot].ItemID = 0;
			Items[Slot].PlayerID = 0;						
			Items[Slot].ItemName = "";		
			Items[Slot].Class = "";
			Items[Slot].Coins = 0;							
			Items[Slot].Level = 0;							
			Items[Slot].Icon = null;
			Items[Slot].Icon = null;
			Items[Slot].Type = "";								
			Items[Slot].Enchantment = "";						
			Items[Slot].Model = null;								
			Items[Slot].Grade = "";
			Items[Slot].Slot = 0;
			
			//Stats
			
			Items[Slot].Life = 0;				 				
			Items[Slot].Armor = 0;
			Items[Slot].Damage = 0;
			Items[Slot].Source = 0;								
			Items[Slot].Steadiness = 0;   			
			Items[Slot].Revitalization = 0;    	 				
			Items[Slot].Acceleration = 0;         			
			Items[Slot].DeathRes = 0;   

	}


	
	public void PreviewItem(Item item){
		
		if(item.Grade == "Simplex" || item.Grade == ""){					
			GetComponent<Inventory_GUI>().grade.normal.textColor = Color.white;					
		}
		else if(item.Grade == "Enhanced"){					
			GetComponent<Inventory_GUI>().grade.normal.textColor = Color.green;					
		}
		else if(item.Grade == "Enchanted"){					
			GetComponent<Inventory_GUI>().grade.normal.textColor = Color.cyan;					
		}
		else if(item.Grade == "Malfeasant"){					
			GetComponent<Inventory_GUI>().grade.normal.textColor = Color.red;					
		}
		else if(item.Grade == "Extinct"){					
			GetComponent<Inventory_GUI>().grade.normal.textColor = new Color32(255, 128, 0, 255);	
		}
		else if(item.Grade == "Unique"){					
			GetComponent<Inventory_GUI>().grade.normal.textColor = Color.yellow;					
		}
		
		GetComponent<Inventory_GUI>().ItemStatsToShow.ItemName = item.ItemName;
		GetComponent<Inventory_GUI>().ItemStatsToShow.Type = item.Type;
		GetComponent<Inventory_GUI>().ItemStatsToShow.Coins = item.Coins;
		GetComponent<Inventory_GUI>().ItemStatsToShow.Life = item.Life;
		GetComponent<Inventory_GUI>().ItemStatsToShow.Armor = item.Armor;
		GetComponent<Inventory_GUI>().ItemStatsToShow.Damage = item.Damage;
		GetComponent<Inventory_GUI>().ItemStatsToShow.Source = item.Source;
		GetComponent<Inventory_GUI>().ItemStatsToShow.Enchantment = item.Enchantment;
		GetComponent<Inventory_GUI>().ItemStatsToShow.Steadiness = item.Steadiness;
		GetComponent<Inventory_GUI>().ItemStatsToShow.Acceleration = item.Acceleration;
		GetComponent<Inventory_GUI>().ItemStatsToShow.Revitalization = item.Revitalization;
		GetComponent<Inventory_GUI>().ItemStatsToShow.DeathRes = item.DeathRes;
		
	}
	
	
}
