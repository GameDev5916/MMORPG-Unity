using UnityEngine;
using System.Collections;

public class Location : MonoBehaviour {
	
	public string LastTown;
	GameObject MapName;
	bool done;
	int timer;
	public float fadespeed;

	// Use this for initialization
	void Start () {

		MapName = new GameObject();
		MapName.name = "MapName";

		MapName.transform.position = new Vector3(0.5f, 0.5f, 0);
		MapName.transform.localScale = new Vector3(0, 0, 1);
		
		MapName.AddComponent<GUITexture>();
		MapName.GetComponent<GUITexture>().texture = Resources.Load("Locations/" + GameObject.Find ("Terrain").GetComponent<MapInfo> ().Name) as Texture;

		MapName.GetComponent<GUITexture>().pixelInset = new Rect(-150, 200, 300, 60);

	}
	
	void OnGUI(){
		
		MapName.GetComponent<GUITexture>().color = new Color(MapName.GetComponent<GUITexture>().color.r, MapName.GetComponent<GUITexture>().color.g, MapName.GetComponent<GUITexture>().color.b, MapName.GetComponent<GUITexture>().color.a - fadespeed);
		
		if(MapName.GetComponent<GUITexture>().color.a == 0){
			Destroy(MapName);
		}
		
	}
	
}
