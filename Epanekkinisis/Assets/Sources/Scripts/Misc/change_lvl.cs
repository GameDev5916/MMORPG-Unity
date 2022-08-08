using UnityEngine;
using System.Collections;

public class change_lvl : MonoBehaviour {
	
	public string Location;	
	
	void OnTriggerEnter(Collider col){
		
		if(col.tag == "Player"){

			if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){	
				GameObject.Find("Southbridge").GetComponent<Southbridge>().ChangeLocation(Location);
			}
			else{
				GameObject.Find("GameManager").GetComponent<PlayersManager>().ChangeLocation(Location);	
			}

		}

	}
	
	
}
