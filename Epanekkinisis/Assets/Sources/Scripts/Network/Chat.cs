using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chat : MonoBehaviour {

	public string talk = "";
	public string chat = "";
	public bool render;

	public GUISkin skin;


	void OnGUI () {
	
		GUI.skin = skin;

		GUI.TextArea (new Rect (10, Screen.height - 140, 300, 100), chat);

		if(render){

		talk = GUI.TextField (new Rect (10, Screen.height - 30, 200, 100), talk);

		if (GUI.Button (new Rect (210, Screen.height - 35, 100, 40), "Talk") || Input.GetKeyDown(KeyCode.Return)) {
			
			gameObject.GetComponent<PlayersManager>().Talk(GameObject.Find("Character").GetComponent<Player>().Name + ": " + talk);
			gameObject.SendMessage("Speak", GameObject.Find("Character").GetComponent<Player>().Name + ": " + talk);
		
		}

		}

	}
	
	public void Speak(string wut){
	
		chat = chat + "\n" + wut;
		
	}
	
}
