using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour {
	
	public string Name;
	public string Description;
	public List<int> Quests = new List<int>();
	public List<Item> ShopItems = new List<Item>();

	void Start () {

		Name = gameObject.name;
			
		for(int i = 0; i <= GetComponents<Item>().Length-1; i++){
			ShopItems.Add(GetComponents<Item>()[i]);
		}

	}

}
