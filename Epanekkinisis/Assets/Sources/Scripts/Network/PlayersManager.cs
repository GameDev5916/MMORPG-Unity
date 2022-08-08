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


public class PlayersManager : MonoBehaviour {
	
	public GameObject Dummy;
	public GameObject PrefabPlayer;
	public Transform SpawnPoint;
	GameObject localPlayer;
	public bool MovementDirty;
	
	public LogLevel logLevel = LogLevel.DEBUG;
	public SmartFox smartFox;
	Dictionary<SFSUser, GameObject> remotePlayers = new Dictionary<SFSUser, GameObject>();
	
	public bool MonstersSet;
	public List<GameObject> Monsters = new List<GameObject>();	
	
	Item SQLItem;

	
	void Start() {

		gameObject.AddComponent<Item>();
		SQLItem = GetComponent<Item>();

		SpawnPoint = GameObject.Find("SpawnPoint").transform;
		
		if (!SmartFoxConnection.IsInitialized) {
			Application.LoadLevel("Loading");
			return;
		}
		
		smartFox = SmartFoxConnection.Connection;
		
		// Register callback delegates
		smartFox.AddEventListener(SFSEvent.OBJECT_MESSAGE, OnObjectMessage);
		smartFox.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
		smartFox.AddEventListener(SFSEvent.USER_VARIABLES_UPDATE, OnUserVariableUpdate);
		smartFox.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoom);
		smartFox.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
		
		smartFox.AddLogListener(logLevel, OnDebugMessage);	//DISABLE FOR FPS BOOST
		
		// Start this clients avatar and get cracking!
		SpawnLocalPlayer();
		
		if(SmartFoxConnection.Connection.UserManager.UserCount == 1){
			MonstersSet = true;
		}
		else if(SmartFoxConnection.Connection.UserManager.UserCount > 1){
			gameObject.SendMessage("SendStats");
		}
		
	}	
	
	
	void Update () {
		
		if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0){
			MovementDirty = true;
		}
		
	}
	
	
	void FixedUpdate() {
		
		if (smartFox != null) {
			smartFox.ProcessEvents();
			
			// If we spawned a local player, send position if movement is dirty
			if (MovementDirty) {
				
				List<UserVariable> userVariables = new List<UserVariable>();
				
				userVariables.Add(new SFSUserVariable("x", (double)localPlayer.transform.position.x));
				userVariables.Add(new SFSUserVariable("y", (double)localPlayer.transform.position.y));
				userVariables.Add(new SFSUserVariable("z", (double)localPlayer.transform.position.z));
				userVariables.Add(new SFSUserVariable("rot", (double)localPlayer.transform.rotation.eulerAngles.y));
				
				smartFox.Send(new SetUserVariablesRequest(userVariables));
				MovementDirty = false;
				
			}
			
		}	
		
	}
	
	
	
	//--------------------------------------------------------------------------------------------------------------
	
	
	
	
	
	public void OnUserVariableUpdate(BaseEvent evt) {
		
		// When user variable is updated on any client, then this callback is being received
		// This is where most of the Enchanted happens
		
		ArrayList changedVars = (ArrayList)evt.Params["changedVars"];
		SFSUser user = (SFSUser)evt.Params["user"];
		List<UserVariable> userVariables = new List<UserVariable>();
		
		
		
		if (user == smartFox.MySelf) return;
		
		
		//Get new players pos-rot
		
		if (!remotePlayers.ContainsKey(user)) {									// NEW PLAYER
						
			Vector3 pos = SpawnPoint.position;
			float rotAngle = 0;
			
			if (user.ContainsVariable("x")) {									// POSITION
				pos.x = (float)user.GetVariable("x").GetDoubleValue();
				pos.y = (float)user.GetVariable("y").GetDoubleValue();
				pos.z = (float)user.GetVariable("z").GetDoubleValue();
			}
			
			if (user.ContainsVariable("rot")) {									// ROTATION
				rotAngle = (float)user.GetVariable("rot").GetDoubleValue();
			}
			
			SpawnRemotePlayer(user, pos, Quaternion.Euler(0, rotAngle, 0));
			
			
			// MONSTERS FOR OTHER
			
			if(MonstersSet){													
				
				for(int i = 0; i <= Monsters.Count-1; i++){
					userVariables.Add(new SFSUserVariable("monsterx" + i.ToString(), (double)Monsters[i].transform.position.x));
					userVariables.Add(new SFSUserVariable("monstery" + i.ToString(), (double)Monsters[i].transform.position.y));
					userVariables.Add(new SFSUserVariable("monsterz" + i.ToString(), (double)Monsters[i].transform.position.z));
					userVariables.Add(new SFSUserVariable("monsterot" + i.ToString(), (double)Monsters[i].transform.rotation.eulerAngles.y));
				}
				
			}
			
		}
		else{
			
			
			// MONSTERS FOR ME
			
			if(!MonstersSet && changedVars.Contains("monsterx0")){	
				
				for(int i = 0; i <= Monsters.Count-1; i++){
					
					Monsters[i].transform.position = new Vector3((float)user.GetVariable("monsterx" + i.ToString()).GetDoubleValue(), (float)user.GetVariable("monstery" + i.ToString()).GetDoubleValue(), (float)user.GetVariable("monsterz" + i.ToString()).GetDoubleValue());
					Monsters[i].transform.Rotate(0, (float)user.GetVariable("monsterot" + i.ToString()).GetDoubleValue(), 0);
					
				}
				
				MonstersSet = true;
				
			}
			
			
			
			// OTHER PLAYERS POSITION - ROTATION
			
			if (changedVars.Contains("x")) {						
				// Move the character to a new position...
				remotePlayers[user].GetComponent<SimpleRemoteInterpolation>().SetTransform(new Vector3((float)user.GetVariable("x").GetDoubleValue(), (float)user.GetVariable("y").GetDoubleValue(), (float)user.GetVariable("z").GetDoubleValue()), Quaternion.Euler(0, (float)user.GetVariable("rot").GetDoubleValue(), 0),true);
			}
			
			
			
			// OTHER PLAYER'S STATS
			
			if (changedVars.Contains("name")){						
				remotePlayers[user].GetComponent<Dummy>().Damage = user.GetVariable("damage").GetIntValue();
				remotePlayers[user].GetComponent<Dummy>().Name = user.GetVariable("name").GetStringValue();
			}
			
			
			
			// UPDATE CHAT
			
			if (changedVars.Contains("talk")){				
				gameObject.GetComponent<Chat>().Speak(user.GetVariable("talk").GetStringValue());				
			}
			


			// REMOVE PLAYER THAT CLOSED THE GAME

			if (changedVars.Contains("close")){				
				Debug.Log("Removing player unit " + remotePlayers[user].GetInstanceID());
				RemoveRemotePlayer(user);				
			}



			// RUN SKILL
			
			
			
		}
		
		
		// SEND MY STATS
		
		userVariables.Add(new SFSUserVariable("x", (double)localPlayer.transform.position.x));
		userVariables.Add(new SFSUserVariable("y", (double)localPlayer.transform.position.y));
		userVariables.Add(new SFSUserVariable("z", (double)localPlayer.transform.position.z));
		userVariables.Add(new SFSUserVariable("rot", (double)localPlayer.transform.rotation.eulerAngles.y));
		
		smartFox.Send(new SetUserVariablesRequest(userVariables));
		
	}
	
	
	
	// -------------------------------------- FUNCTIONS ------------------------------------------------------


	
	// TELL THEM YOU ENTERED
	
	public void SpawnMyPlayer(){
		
		List<UserVariable> userVariables = new List<UserVariable>();
		
		userVariables.Add(new SFSUserVariable("x", (double)localPlayer.transform.position.x));
		userVariables.Add(new SFSUserVariable("y", (double)localPlayer.transform.position.y));
		userVariables.Add(new SFSUserVariable("z", (double)localPlayer.transform.position.z));
		userVariables.Add(new SFSUserVariable("rot", (double)localPlayer.transform.rotation.eulerAngles.y));
		
		smartFox.Send(new SetUserVariablesRequest(userVariables));	
		
	}
	
	
	
	// SPAWN LOCAL PLAYER	
	
	void SpawnLocalPlayer() {
		
		Vector3 pos;
		Quaternion rot;
		
		// See if there already exists a model - if so, take its pos+rot before destroying it
		if (localPlayer != null) {
			pos = localPlayer.transform.position;
			rot = localPlayer.transform.rotation;
			Destroy(localPlayer);
		} else {
			pos = SpawnPoint.position;
			rot = Quaternion.Euler(0, 0, 0);
		}
		
		// Lets spawn our local player model
		localPlayer = GameObject.Instantiate(PrefabPlayer, pos, rot) as GameObject;
		
	}

	
	
	//REMOTE	
	
	void SpawnRemotePlayer(SFSUser user, Vector3 pos, Quaternion rot) {
		
		// See if there already exists a model so we can destroy it first
		if (remotePlayers.ContainsKey(user) && remotePlayers[user] != null) {
			Destroy(remotePlayers[user]);
			remotePlayers.Remove(user);
		}
		
		// Lets spawn our remote player model
		GameObject remotePlayer = GameObject.Instantiate(Dummy, pos, rot) as GameObject;
		remotePlayer.AddComponent<SimpleRemoteInterpolation>();
		remotePlayer.GetComponent<SimpleRemoteInterpolation>().SetTransform(pos, rot, false);
		
		
		// Lets track the dude
		remotePlayers.Add(user, remotePlayer);
		
	}


	
	// SEND STATS PER SECOND
	
	IEnumerator SendStats(){
		
		yield return new WaitForSeconds(0.5f);
		
		List<UserVariable> userStats = new List<UserVariable>();
		
		userStats.Add(new SFSUserVariable("damage", localPlayer.GetComponentInChildren<Player>().MomentaryDamage));
		userStats.Add(new SFSUserVariable("name", localPlayer.GetComponentInChildren<Player>().Name));
		
		smartFox.Send(new SetUserVariablesRequest(userStats));
		
		gameObject.SendMessage("SendStats");
		
	}



	// TALK TO CHAT
		
	public void Talk(string say){
		
		List<UserVariable> chat = new List<UserVariable>();
		
		chat.Add(new SFSUserVariable("talk", say));
		
		smartFox.Send(new SetUserVariablesRequest(chat));	
		
	}
	
	
	//-------------------------------------------------------------------------------------------------------
	
	
	//LISTENERS

	public void OnDebugMessage(BaseEvent evt) {
		string message = (string)evt.Params["message"];
		Debug.Log("[SFS DEBUG] " + message);
	}

	public void OnObjectMessage(BaseEvent evt) {
		// The only messages being send around are
		// - a remove message from someone that is dropping out
		ISFSObject dataObj = (SFSObject)evt.Params["message"];
		SFSUser sender = (SFSUser)evt.Params["sender"];
		
		if ((string)evt.Params["cmd"] == "ME TO KALO AMA XRISIMOPOIH8EI") {

		}

	}

	public void OnUserExitRoom(BaseEvent evt) {			// KAPOIOS EFIGE
		SFSUser user = (SFSUser)evt.Params["user"];	
		RemoveRemotePlayer(user);		
	}

	public void OnConnectionLost(BaseEvent evt) {		// STO LOGIN
		smartFox.RemoveAllEventListeners();
		Application.LoadLevel("Loading");
	}	
	
	void OnApplicationQuit() {							// KLEINEI TO GAME
		List<UserVariable> userVariables = new List<UserVariable>();
		userVariables.Add(new SFSUserVariable("close", "close"));
		smartFox.Send(new SetUserVariablesRequest(userVariables));	
	}

	void RemoveRemotePlayer(SFSUser user) {

		if (user == smartFox.MySelf) return;
		
		if (remotePlayers.ContainsKey(user)) {			
			Destroy(remotePlayers[user]);
			remotePlayers.Remove(user);			
		}

	}		
	
	
	
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
		
		Itemx.PutFloat("Steadiness", Iteme.Steadiness);
		Itemx.PutFloat("Acceleration", Iteme.Acceleration); 
		Itemx.PutFloat("DeathRes", Iteme.DeathRes); 
		Itemx.PutFloat("Revitalization", Iteme.Revitalization); 
		
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
		
		Skillx.PutInt("Player", skillid);
		Skillx.PutInt("SkillID", Info.PlayerID); 
		
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
				
				SQLItem.Steadiness = (float)Item.GetDouble("steadiness");
				SQLItem.Acceleration = (float)Item.GetDouble("acceleration");
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