using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using Sfs2X.Logging;


public class Southbridge : MonoBehaviour {
	
	//----------------------------------------------------------
	// Setup variables
	//----------------------------------------------------------
	
	public LogLevel logLevel = LogLevel.DEBUG;

	Item SQLItem;

	public GameObject player;

	SmartFox smartFox;
	public string msg;

	//----------------------------------------------------------
	// Unity callbacks
	//----------------------------------------------------------
	void Start() {

		gameObject.AddComponent<Item>();
		SQLItem = GetComponent<Item>();

		if (!SmartFoxConnection.IsInitialized) {
			Application.LoadLevel("Loading");
			return;
		}
		
		smartFox = SmartFoxConnection.Connection;
		
		// Register callback delegates
		smartFox.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
		smartFox.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
		
		smartFox.AddLogListener(logLevel, OnDebugMessage);
		
		if(Info.haschar){
			
			Destroy(GameObject.Find("Start"));
			GameObject Player = Instantiate(player, transform.position, transform.rotation) as GameObject; 			
			Player.GetComponentInChildren<Player>().PlayerID = Info.PlayerID;
			
		}

	}	
	
	
	
	void FixedUpdate() {
		
		if (smartFox != null) {
			smartFox.ProcessEvents();
		}
		
	}
	
	
	
	//-------------------------------------------------------------------------------------------------------
	
	
	public void OnDebugMessage(BaseEvent evt) {
		string message = (string)evt.Params["message"];
		Debug.Log("[SFS DEBUG] " + message);
	}	
	
	public void OnConnectionLost(BaseEvent evt) {
		// Reset all internal states so we kick back to login screen
		smartFox.RemoveAllEventListeners();
		Application.LoadLevel("Loading");
	}	
	
	
	//-------------------------------------------------------------------------------------------------------
	
	
	
	
	//-------------MYSQL---------------------
	
	public void GetInfo(){
		
		ISFSObject Infox = new SFSObject();
		
		Infox.PutInt("PlayerID", Info.PlayerID);

		smartFox.Send(new ExtensionRequest("GetInfo", Infox));

	}
	

	public void AddCoins(int Coins){
		
		ISFSObject Coin = new SFSObject();
		
		Coin.PutInt("Coins", Coins);
		Coin.PutInt("PlayerID", Info.PlayerID);
		
		smartFox.Send(new ExtensionRequest("AddCoins", Coin)); 
		
	}
	
	
	public void AddXP(int XP){
		
		ISFSObject Coin = new SFSObject();
		
		Coin.PutInt("XP", XP);
		Coin.PutInt("PlayerID", Info.PlayerID);
		
		smartFox.Send(new ExtensionRequest("AddXP", Coin)); 
		
	}
	
	
	public void LevelUpen(int Level){
		
		ISFSObject LevelUpe = new SFSObject();
		
		LevelUpe.PutInt("Level", Level);
		LevelUpe.PutInt("PlayerID", Info.PlayerID);
		
		smartFox.Send(new ExtensionRequest("LevelUP", LevelUpe)); 
		
	}


	public void Character(string Name, string Gender, string Class){
		
		ISFSObject Character = new SFSObject();
		
		Character.PutUtfString("Name", Name);
		Character.PutUtfString("Gender", Gender);
		Character.PutUtfString("Class", Class);
		Character.PutInt("PlayerID", Info.PlayerID);
		
		smartFox.Send(new ExtensionRequest("NewCharacter", Character));
		
	}


	public void AddQuest(int QuestID){
		
		ISFSObject Quest = new SFSObject();
		
		Quest.PutInt("PlayerID", Info.PlayerID); 
		Quest.PutInt("QuestID", QuestID); 
		
		smartFox.Send(new ExtensionRequest("AddQuest", Quest)); 
		
	}


	public void DoQuest(int QuestID){
		
		ISFSObject IDs = new SFSObject();
		IDs.PutInt("PlayerID", Info.PlayerID);
		IDs.PutInt("QuestID", QuestID);
		smartFox.Send(new ExtensionRequest("DoQuest", IDs));
		
	}


	public void RemoveQuest(int QuestID){
		
		ISFSObject IDs = new SFSObject();
		IDs.PutInt("PlayerID", Info.PlayerID);
		IDs.PutInt("QuestID", QuestID);
		smartFox.Send(new ExtensionRequest("RemoveQuest", IDs));
		
	}


	public void FinishQuest(int QuestID){
		
		ISFSObject IDs = new SFSObject();
		IDs.PutInt("PlayerID", Info.PlayerID);
		IDs.PutInt("QuestID", QuestID);
		smartFox.Send(new ExtensionRequest("FinishQuest", IDs));
		
	}

	
	public void AddItem(Item Iteme){
		
		ISFSObject Itemx = new SFSObject();
		
		Itemx.PutInt("PlayerID", Iteme.PlayerID); 
		
		Itemx.PutUtfString("Name", Iteme.ItemName);
		Itemx.PutUtfString("Class", Iteme.Class);
		Itemx.PutUtfString("Grade", Iteme.Grade); 
		Itemx.PutUtfString("Type", Iteme.Type); 

		Itemx.PutInt("Coins", Iteme.Coins); 
		Itemx.PutInt("Level", Iteme.Level);  
		
		Itemx.PutUtfString("Enchantment", Iteme.Enchantment); 
		Itemx.PutUtfString("Model", Iteme.Model.name); 
		
		Itemx.PutInt("Slot", Iteme.Slot);
		
		Itemx.PutInt("Life", Iteme.Life); 
		Itemx.PutInt("Source", Iteme.Source);
		Itemx.PutInt("Armor", Iteme.Armor);
		Itemx.PutInt("Damage", Iteme.Damage);

		Itemx.PutFloat("steadiness", Iteme.Steadiness);
		Itemx.PutFloat("acceleration", Iteme.Acceleration); 
		Itemx.PutFloat("revitalization", Iteme.Revitalization); 
		Itemx.PutFloat("deathres", Iteme.DeathRes); 

		smartFox.Send(new ExtensionRequest("AddItem", Itemx)); 
		
	}


	public void ItemStatus(Item item){
		
		ISFSObject Item1 = new SFSObject();
		
		Item1.PutInt("Slot", item.Slot);
		Item1.PutInt("ItemID", item.ItemID);
		
		smartFox.Send(new ExtensionRequest("ItemStatus", Item1)); 
		
	}
	
	
	public void DeleteItem(Item item){
		
		ISFSObject Item1 = new SFSObject();
		
		Item1.PutInt("ItemID", item.ItemID);
		
		smartFox.Send(new ExtensionRequest("DeleteItem", Item1)); 
		
	}


	public void AddSkill(int skillid){
		
		ISFSObject Skillx = new SFSObject();
		
		Skillx.PutInt("SkillID", skillid);
		Skillx.PutInt("PlayerID", Info.PlayerID); 
		
		smartFox.Send(new ExtensionRequest("AddSkill", Skillx)); 
		
	}
	

	public void SkillsAndQuestsAndItems(){
		
		ISFSObject PlayerID = new SFSObject();
		PlayerID.PutInt("PlayerID", Info.PlayerID);
		smartFox.Send(new ExtensionRequest("Items", PlayerID));
		
		ISFSObject PlayerID2 = new SFSObject();
		PlayerID2.PutInt("PlayerID", Info.PlayerID);
		smartFox.Send(new ExtensionRequest("Quests", PlayerID2));
		
		ISFSObject PlayerID3 = new SFSObject();
		PlayerID3.PutInt("PlayerID", Info.PlayerID);
		smartFox.Send(new ExtensionRequest("Skills", PlayerID3));
		
	}
	

	public void ChangeLocation(string Location){
		
		ISFSObject UserID = new SFSObject();
		UserID.PutInt("PlayerID", Info.PlayerID);
		UserID.PutUtfString("Location", Location);
		smartFox.Send(new ExtensionRequest("RemoveQuest", UserID));

		Application.LoadLevel(Location);

	}
	
	//--------------------MYSQL---------------------
	
	
	
	
	
	
	//------------------------ MYSQL BACK ----------------------------
	
	
	void OnExtensionResponse(BaseEvent evt){
		
		
		//Get Items
		
		if ((string)evt.Params["cmd"] == "Items") {
			
			ISFSObject parameters = (SFSObject)evt.Params["params"];
			
			ISFSArray Items = parameters.GetSFSArray("Items");
			
			int Size = Items.Size();
		
			for(int i = 0; i <= Size-1; i++){
			
				ISFSObject Item = Items.GetSFSObject(i);

				SQLItem.ItemID = Item.GetInt("itemid");
				SQLItem.PlayerID = Item.GetInt("playerid");

				SQLItem.ItemName = Item.GetUtfString("name");	
				SQLItem.Class = Item.GetUtfString("class");
				SQLItem.Grade = Item.GetUtfString("grade");
				SQLItem.Type = Item.GetUtfString("type");

				SQLItem.Coins = Item.GetInt("coins");
				SQLItem.Level = Item.GetInt("level");

				SQLItem.Icon = Resources.Load("Icons/" + Item.GetUtfString("name")) as Texture;

				SQLItem.Enchantment = Item.GetUtfString("enchantment");
				SQLItem.Model = Resources.Load("Models/" + Item.GetUtfString("model")) as GameObject;

				SQLItem.Slot = Item.GetInt("slot");

				SQLItem.Life = Item.GetInt("life");
				SQLItem.Armor = Item.GetInt("armor");
				SQLItem.Damage = Item.GetInt("damage");
				SQLItem.Source = Item.GetInt("source");

				SQLItem.Acceleration = (float)Item.GetDouble("acceleration");
				SQLItem.Steadiness = (float)Item.GetDouble("steadiness");
				SQLItem.Revitalization = (float)Item.GetDouble("revitalization");
				SQLItem.DeathRes = (float)Item.GetDouble("deathres");

				GameObject.FindWithTag("Player").GetComponentInChildren<Inventory_Functions>().AddItem(SQLItem, true, SQLItem.Slot);			
				
			}
			
		}
		
		
		
		//Get Quests
		
		if ((string)evt.Params["cmd"] == "Quests") {
			
			ISFSObject parameters = (SFSObject)evt.Params["params"];
			
			ISFSArray Quests = parameters.GetSFSArray("Quests");
			
			int Size = Quests.Size();
			
			for(int i = 0; i <= Size-1; i++){
				
				ISFSObject Quest = Quests.GetSFSObject(i);

				Info.Quests[Info.FindQuestFromID(Quest.GetInt("QuestID"))].Done = Quest.GetInt("done");
				Info.Quests[Info.FindQuestFromID(Quest.GetInt("QuestID"))].Finished = Quest.GetInt("finished");
				Info.Quests[Info.FindQuestFromID(Quest.GetInt("QuestID"))].HasIt = true;

				if(Quest.GetInt("finished") == 0){
					GameObject.FindWithTag("Player").GetComponentInChildren<Quests>().PlayerQuests.Add(Info.FindQuestFromID(Quest.GetInt("QuestID")));
				}

			}
			
		}
		
			
		
		
		//Get Skills
		
		if ((string)evt.Params["cmd"] == "Skills") {
			
			ISFSObject parameters = (SFSObject)evt.Params["params"];
			
			ISFSArray Skills = parameters.GetSFSArray("Skills");
			
			int Size = Skills.Size();
			
			for(int i = 0; i <= Size-1; i++){
				
				ISFSObject Skill = Skills.GetSFSObject(i);
				
				GameObject.Find("Skills").GetComponent<Skills>().UnlockSkill(Skill.GetInt("skillid"));
				
			}
			
		}
		


		//Info
		
		if ((string)evt.Params["cmd"] == "GetInfo") {
			
			ISFSObject parameters = (SFSObject)evt.Params["params"];
			
			ISFSArray Infos = parameters.GetSFSArray("Info");
			
			ISFSObject Info = Infos.GetSFSObject(0);
			
			GameObject.Find("Character").GetComponent<Player>().Infose(Info.GetUtfString("name"), Info.GetInt("level"), Info.GetUtfString("class"), Info.GetInt("Coins"), Info.GetUtfString("gender"), Info.GetInt("xp"));
			
			gameObject.SendMessage("SkillsAndQuestsAndItems");

		}
		
		
		
		//Character Name Check
		
		if ((string)evt.Params["cmd"] == "Taken") {
			
			msg = "Name taken";
			
		}
		
		
		//Character Created
		
		if ((string)evt.Params["cmd"] == "Created") {
			
			GameObject.Find("Start").GetComponent<CharacterCreator>().created = true;
			
		}
		


		//Set ID taken from MySQL

		if ((string)evt.Params["cmd"] == "ItemID") {
			
			ISFSObject parameters = (SFSObject)evt.Params["params"];
			
			for(int i = 0; i <= 19; i++){
				
				if(GameObject.FindWithTag("Player").GetComponentInChildren<Inventory_Functions>().Items[i].Slot == parameters.GetInt("Slot")){					
					GameObject.FindWithTag("Player").GetComponentInChildren<Inventory_Functions>().Items[i].ItemID = parameters.GetInt("ItemID");					
				}
				
			}
			
		}



		//Error
		
		if ((string)evt.Params["cmd"] == "Error") {
			
			ISFSObject parameters = (SFSObject)evt.Params["params"];
			
			print (parameters.GetUtfString("ErrorMsg"));
			
		}
		
		
		
		
		//Test
		
		if ((string)evt.Params["cmd"] == "Test") {
			
			ISFSObject parameters = (SFSObject)evt.Params["params"];
			
			print (parameters.GetInt("Test1"));
			print (parameters.GetInt("Test2"));
			print (parameters.GetInt("Test3"));
			
		}
		
		
	}
	
	//MYSQL BACK ---------------------------------------------------------------------
	
	
	
	
}