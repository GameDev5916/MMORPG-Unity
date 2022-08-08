using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	RaycastHit hit;
	public GameObject TempItem;
	public GUISkin GuiSkin;
	
	public GUIStyle LabelColor;


	// Update is called once per frame
	void Update () {		

		if(Input.GetKeyUp(KeyCode.E)){

			if(TempItem.GetComponent<QuestCheck>()){
				TempItem.GetComponent<QuestCheck>().DoQuest();
			}
			
			if(TempItem != null && TempItem.tag == "Item"){
							
				transform.parent.GetComponentInChildren<Inventory_Functions>().AddItem(TempItem.GetComponent<Item>(), false, 666);				
				Destroy(TempItem);
				
			}
			else if(TempItem != null && TempItem.tag == "Machine"){

				TempItem.SendMessage("Activate");				
						
			}
			else if(TempItem != null && TempItem.tag == "Coins"){
				
				transform.parent.GetComponentInChildren<Player>().AddCoins(TempItem.GetComponent<Item>().Coins);				
				Destroy(TempItem);
				
			}
			
		}
		
	}
	
	
	
	void OnGUI() {
		
		GUI.skin = GuiSkin;
		
		if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)) {
			
			TempItem = hit.collider.gameObject;
			
			if(hit.collider.tag == "Item"){
				
				if(hit.collider.gameObject.GetComponent<Item>().Grade == "Simplex"){					
					LabelColor.normal.textColor = Color.white;					
				}
				else if(hit.collider.gameObject.GetComponent<Item>().Grade == "Enhanced"){					
					LabelColor.normal.textColor = Color.green;					
				}
				else if(hit.collider.gameObject.GetComponent<Item>().Grade == "Enchanted"){					
					LabelColor.normal.textColor = Color.cyan;					
				}
				else if(hit.collider.gameObject.GetComponent<Item>().Grade == "Malfeasant"){					
					LabelColor.normal.textColor = Color.red;					
				}
				else if(hit.collider.gameObject.GetComponent<Item>().Grade == "Extinct"){					
					LabelColor.normal.textColor = new Color32(255, 128, 0, 255);					
				}
				else if(hit.collider.gameObject.GetComponent<Item>().Grade == "Unique"){					
					LabelColor.normal.textColor = Color.yellow;					
				}
				
				GUI.Label (new Rect (Screen.width/2 - 250/2, 530, 250, 30), hit.collider.gameObject.GetComponent<Item>().ItemName, LabelColor);
				
			}
			else if(hit.collider.tag == "Machine"){
				
				LabelColor.normal.textColor = Color.white;				
				GUI.Label (new Rect (Screen.width/2 - 250/2, 530, 250, 30), "Activate", LabelColor);	
				
			}
			else if(hit.collider.tag == "Coins"){
				
				LabelColor.normal.textColor = new Color32(255, 128, 0, 255);				
				GUI.Label (new Rect (Screen.width/2 - 250/2, 530, 250, 30), hit.collider.gameObject.GetComponent<Item>().Coins + " Coins", LabelColor);	
				
			}
			else if(hit.collider.tag == "Dummy"){
				
				LabelColor.normal.textColor = Color.green;				
				GUI.Label (new Rect (Screen.width/2 - 250/2, 530, 250, 30), hit.collider.gameObject.GetComponent<Dummy>().Name, LabelColor);	
				
			}
			else if(hit.collider.tag == "Monster"){
				
				if(hit.collider.transform.FindChild("MonsterLife").GetComponent<MonsterLife>().Type == "Monster"){
					LabelColor.normal.textColor = Color.white;	
					GUI.Label (new Rect (Screen.width/2 - 200/2, 50, 200, 30), "Monster", LabelColor);
				}
				else if(hit.collider.transform.FindChild("MonsterLife").GetComponent<MonsterLife>().Type == "Special"){
					LabelColor.normal.textColor = Color.cyan;	
					GUI.Label (new Rect (Screen.width/2 - 250/2, 50, 250, 30), "Special Monster", LabelColor);
				}
				else if(hit.collider.gameObject.tag == "Mini Boss"){
					LabelColor.normal.textColor = new Color32(255, 128, 0, 255);	
					GUI.Label (new Rect (Screen.width/2 - 250/2, 50, 250, 30), "Mini Boss", LabelColor);
				}
				else if(hit.collider.gameObject.tag == "Boss"){
					LabelColor.normal.textColor = Color.yellow;		
					GUI.Label (new Rect (Screen.width/2 - 250/2, 50, 250, 30), "Boss", LabelColor);
				}
				
				GUI.Label (new Rect (Screen.width/2 - 250/2, 29, 250, 30), hit.collider.transform.FindChild("MonsterLife").GetComponent<MonsterLife>().Name + " - " + hit.collider.transform.FindChild("MonsterLife").GetComponent<MonsterLife>().PercentLife + "%", LabelColor);
				
			}
			else if(hit.collider.tag == "NPC"){
				
				LabelColor.normal.textColor = Color.white;	
				
				GUI.Label (new Rect (Screen.width/2 - 250/2, 30, 250, 30), hit.collider.gameObject.GetComponent<NPC>().Name, LabelColor);
				GUI.Label (new Rect (Screen.width/2 - 250/2, 50, 250, 30), hit.collider.gameObject.GetComponent<NPC>().Description, LabelColor);
				
			}
			
		}
		else{
			TempItem = null;
		}
		
	}	
	
}

