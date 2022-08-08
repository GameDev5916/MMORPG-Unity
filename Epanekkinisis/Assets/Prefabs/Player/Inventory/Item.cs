using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	
	//Info
	
	public int ItemID;
	public int PlayerID;	

	public string ItemName;		

	public string Class;
	public string Grade;	
	public string Type;

	public int Coins;				
	public int Level;		
	
	public Texture Icon;		

	public string Enchantment;	
	public GameObject Model;	
	public int Slot;

	//Stats
	
	public int Life;
	public int Armor;
	public int Damage;
	public int Source;	
		
	public float Steadiness;	//(%)
	public float Revitalization;    	 //(%)
	public float DeathRes;     	 //(%)
	public float Acceleration;	//(%)
	
}
