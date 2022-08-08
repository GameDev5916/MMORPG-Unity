using UnityEngine;
using System.Collections;

public class QuestCheck : MonoBehaviour {

	public int QuestID;

	public void DoQuest(){

		Info.Quests[Info.FindQuestFromID(QuestID)].Done = 1;

		if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){	
			GameObject.Find("Southbridge").GetComponent<Southbridge>().DoQuest(QuestID);
		}
		else{
			GameObject.Find("GameManager").GetComponent<PlayersManager>().DoQuest(QuestID);
		}

		Destroy(this);

	}

}
