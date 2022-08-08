using UnityEngine;
using System.Collections;

public class MapInfo : MonoBehaviour {

	public string Name;
	
	public enum MapType { Town = 0, Public = 1, Dungeon = 2, Boss = 3 }
	public MapType type;
	
	public AudioClip Track;

	void Start () {

		if(type == MapType.Town){

			GameObject.FindWithTag("Player").GetComponent<Location>().LastTown = Name;

			if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){
				GameObject.Find("Southbridge").GetComponent<Southbridge>().ChangeLocation(name);					
			}
			else{
				GameObject.Find("GameManager").GetComponent<PlayersManager>().ChangeLocation(Name);	
			}

		}

	}

}
