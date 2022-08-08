using UnityEngine;
using System.Collections;

public class WorldMap : MonoBehaviour {

	public GUISkin skin;
	Rect windowRect = new Rect(Screen.width/2 - 1000/2, -20, 1000, 650);

	public string XXXXXXXXXXXXX = "XXXXXXXXXXXXXXXXXX";
	
	Vector2 pivotPoint = new Vector2(424, 355);
	float Ring1Angle, Ring2Angle, Ring3Angle, Ring4Angle, TA1, TA2, TA3, TA4;

	public Texture border, ring1, ring2, ring3, ring4;

	Rect size = new Rect(50, 125, 748, 460);

	public bool render;

	public float var = 170;


	void OnGUI() {

		GUI.skin = skin;

		if(render){
			windowRect = GUI.Window(0, windowRect, DoMyWindow, "W o r l d  M a p");
		}

	}

	void DoMyWindow(int windowID) {

		GUI.Label(new Rect(810, var, 140, 40), "First Ring");

		if(GUI.RepeatButton(new Rect(810, var+30, 80, 40), "<")){
			Ring1Angle--;
		}
		if(GUI.RepeatButton(new Rect(870, var+30, 80, 40), ">")){
			Ring1Angle++;
		}


		GUI.Label(new Rect(810, var+75, 140, 40), "Second Ring");
		
		if(GUI.RepeatButton(new Rect(810, var+105, 80, 40), "<")){
			Ring2Angle--;
		}
		if(GUI.RepeatButton(new Rect(870, var+105, 80, 40), ">")){
			Ring2Angle++;
		}


		GUI.Label(new Rect(810, var+150, 140, 40), "Third Ring");
		
		if(GUI.RepeatButton(new Rect(810, var+180, 80, 40), "<")){
			Ring3Angle--;
		}
		if(GUI.RepeatButton(new Rect(870, var+180, 80, 40), ">")){
			Ring3Angle++;
		}


		GUI.Label(new Rect(810, var+225, 140, 40), "Fourth Ring");
		
		if(GUI.RepeatButton(new Rect(810, var+255, 80, 40), "<")){
			Ring4Angle--;
		}
		if(GUI.RepeatButton(new Rect(870, var+255, 80, 40), ">")){
			Ring4Angle++;
		}

		if(GUI.Button(new Rect(810, var+310, 140, 40), "Default")){

			TA1 = Ring1Angle;
			TA2 = Ring2Angle;
			TA3 = Ring3Angle;
			TA4 = Ring4Angle;

			Ring1Angle = 0;
			Ring2Angle = 0;
			Ring3Angle = 0;
			Ring4Angle = 0;

		}

		if(GUI.Button(new Rect(810, var+340, 140, 40), "Last Combination")){
			
			Ring1Angle = TA1;
			Ring2Angle = TA2;
			Ring3Angle = TA3;
			Ring4Angle = TA4;
			
		}

		GUI.DrawTexture(size, border);

		GUIUtility.RotateAroundPivot(Ring1Angle, pivotPoint);
		GUI.DrawTexture(size, ring1);
		GUIUtility.RotateAroundPivot(-Ring1Angle, pivotPoint);

		GUIUtility.RotateAroundPivot(Ring2Angle, pivotPoint);
		GUI.DrawTexture(size, ring2);
		GUIUtility.RotateAroundPivot(-Ring2Angle, pivotPoint);

		GUIUtility.RotateAroundPivot(Ring3Angle, pivotPoint);
		GUI.DrawTexture(size, ring3);
		GUIUtility.RotateAroundPivot(-Ring3Angle, pivotPoint);

		GUIUtility.RotateAroundPivot(Ring4Angle, pivotPoint);
		GUI.DrawTexture(size, ring4);
		GUIUtility.RotateAroundPivot(-Ring4Angle, pivotPoint);

	}



}