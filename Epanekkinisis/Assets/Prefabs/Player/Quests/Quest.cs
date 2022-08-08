using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour {
	
	public int QuestID;
	public string QuestNPC;

	public string Name;
	public int Level;

	public int Done;
	public int Finished;
	public bool HasIt;

	public string BDescription;
	public string ADescription;

	public int QuestToBeDoneID;

	public int Xp;
	public int Coins;
	public bool GivesItem;
	public GameObject Reward;

}
