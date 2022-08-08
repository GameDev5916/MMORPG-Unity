using UnityEngine;
using System.Collections;

public class Riddles : MonoBehaviour {

	Rect RiddlesWindow = new Rect(Screen.width/2 - 475/2, Screen.height/2 - 340/2, 475, 340);

	public GUISkin customSkin;
	public bool render;	

	public string Name;
	public string Question;
	public string Answer;

	string answ = "";

	int points;


	void  OnGUI (){
		
		GUI.skin = customSkin;

		if(render){
			RiddlesWindow = GUI.Window (4, RiddlesWindow, RiddlesFunction, "R i d d l e");
		}
				
	}

	void RiddlesFunction (int windowID) {	

		GUI.TextArea(new Rect(90, 120, 300, 100), Question);

		answ = GUI.TextField(new Rect(115, 230, 250, 20), answ);
		
		if(GUI.Button(new Rect(170, 260, 150, 30), "Answer")){

			if(answ.Trim().ToUpper() == Answer){

				points++;

			}

		}

	}	
	
}
