using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Info : MonoBehaviour {

	static public bool haschar;
	static public bool IsReg;

	static public int PlayerID;

	static public string Name;
	static public int Level;
	static public string Classi;
	static public string Sex;

	static public int Coins;
	static public int Xp;

	static public Dictionary<int, int> Levels = new Dictionary<int, int>();
	static public List<Quest> Quests = new List<Quest>();

	//----------------------------------------


	static public int GetNextLevel(int level){
		return Levels[level+1];
	}


	static public bool CheckLevel(int xp, int level){
		
		if(xp >= Levels[level + 1]){
			return true;
		}
		else{
			return false;
		}
		
	}

	
	static public void SetLevels(){
		
		Levels.Add(2, 100);
		Levels.Add(3, 400);
		Levels.Add(4, 800);
		Levels.Add(5, 1300);
		Levels.Add(6, 1900);
		Levels.Add(7, 2600);
		Levels.Add(8, 3400);
		Levels.Add(9, 4400);
		Levels.Add(10, 5400);
		Levels.Add(11, 6500);
		Levels.Add(12, 7700);
		Levels.Add(13, 9000);
		Levels.Add(14, 10400);
		Levels.Add(15, 11900);
		Levels.Add(16, 13500);
		Levels.Add(17, 15200);
		Levels.Add(18, 17000);
		Levels.Add(19, 18900);
		Levels.Add(20, 20900);
		Levels.Add(21, 23000);
		Levels.Add(22, 25200);
		Levels.Add(23, 27500);
		Levels.Add(24, 29900);
		Levels.Add(25, 32400);
		Levels.Add(26, 35000);
		Levels.Add(27, 35027);
		Levels.Add(28, 37827);
		Levels.Add(29, 40727);
		Levels.Add(30, 43727);
		Levels.Add(31, 46827);
		Levels.Add(32, 50027);
		Levels.Add(33, 53327);
		Levels.Add(34, 56727);
		Levels.Add(35, 60227);
		Levels.Add(36, 60263);
		Levels.Add(37, 60300);
		Levels.Add(38, 60338);
		Levels.Add(39, 60377);
		Levels.Add(40, 64377);
		Levels.Add(41, 105377);
		Levels.Add(42, 109577);
		Levels.Add(43, 113877);
		Levels.Add(44, 118277);
		Levels.Add(45, 122777);
		Levels.Add(46, 127377);
		Levels.Add(47, 132077);
		Levels.Add(48, 136877);
		Levels.Add(49, 141777);
		Levels.Add(50, 146777);
		Levels.Add(51, 151877);
		Levels.Add(52, 157077);
		Levels.Add(53, 162377);
		Levels.Add(54, 167777);
		Levels.Add(55, 173277);
		Levels.Add(56, 178877);
		Levels.Add(57, 184577);
		Levels.Add(58, 190377);
		Levels.Add(59, 196277);
		Levels.Add(60, 202277);
		Levels.Add(61, 208377);
		Levels.Add(62, 214577);
		Levels.Add(63, 220877);
		Levels.Add(64, 227277);
		Levels.Add(65, 233777);
		Levels.Add(66, 240377);
		Levels.Add(67, 247077);
		Levels.Add(68, 253877);
		Levels.Add(69, 260777);
		Levels.Add(70, 267777);

	}


	//To done ginete apo to network kai tsekarei gia to questtobedone sto npcquests.cs An den exei proigoumeno quest to questtobedoneid menei 0


	static public void SetQuests(){
	
		Quests.Add(new Quest());

		Quests[0].Finished = 1;

		// TO 0 KENO GIA N MI GAMITHEI KAI TO DONE TRUE GIA TA QUESTS POU DEN 8ELOUN QUESTTOBEDONEID
		// TO DONE KAI TO FINISHED APO TO MANAGER
		// To PlayerID iparxei mono sto db gia na ksexorizei tous pektes

		Quests.Add(new Quest());
				
		Quests[1].QuestID = 0;
		Quests[1].QuestNPC = "Saus";

		Quests[1].Name = "The beginning";
		Quests[1].Level = 1;

		Quests[1].Done = 0;
		Quests[1].Finished = 0;

		Quests[1].QuestToBeDoneID = 0;

		Quests[1].ADescription = "Quest description before player completes it.";
		Quests[1].BDescription = "Quest description after player completes it.";

		Quests[1].Xp = 100;
		Quests[1].Coins = 10;
		Quests[1].GivesItem = false;
		Quests[1].Reward = null;	//Allios me Resources.Load

	}


	static public void Start(){

		for (int i = 1; i <= Quests.Count-1; i++) {

			if(GameObject.Find(Quests[i].QuestNPC)){
				GameObject.Find(Quests[i].QuestNPC).GetComponent<NPC>().Quests.Add(Quests[i].QuestID);
			}

		}

	}


	static public int FindQuestFromID (int ID){

		int slot = 666;

		for(int i = 0; i <= Quests.Count-1; i++){

			if(Quests[i].QuestID == ID){
				slot = i;
			}

		}

		return slot;

	}

	
}
