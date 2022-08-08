using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Item))]
[CanEditMultipleObjects]

public class ItemEditor : Editor {

	Item myTarget;

	public override void OnInspectorGUI(){

		myTarget = (Item)target;

		myTarget.ItemName = EditorGUILayout.TextField(new GUIContent("Name:"), myTarget.ItemName);
		myTarget.Class = EditorGUILayout.TextField(new GUIContent("Class:"), myTarget.Class);
		myTarget.Type = EditorGUILayout.TextField(new GUIContent("Type:"), myTarget.Type);
		myTarget.Grade = EditorGUILayout.TextField(new GUIContent("Grade:"), myTarget.Grade);				
		
		myTarget.Coins = EditorGUILayout.IntField(new GUIContent("Price:"), myTarget.Coins);
		myTarget.Slot = EditorGUILayout.IntField(new GUIContent("Slot:"), myTarget.Slot);

		myTarget.Level = EditorGUILayout.IntSlider ("Level", myTarget.Level, 1, 50);
		ProgressBar (myTarget.Level / 50.0f, "Level: " + myTarget.Level);
		
		EditorGUILayout.LabelField("Enchantment:");
		myTarget.Enchantment = EditorGUILayout.TextArea(myTarget.Enchantment, GUILayout.Height(100), GUILayout.MaxWidth(235));
		
		myTarget.Icon = (Texture)EditorGUILayout.ObjectField(new GUIContent("Icon:"), myTarget.Icon, typeof(Texture), true);
		myTarget.Model = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Model:"), myTarget.Model, typeof(GameObject), true);
		
		
		//------------------Stats---------------------------------------
		
		EditorGUILayout.LabelField("");
		EditorGUILayout.LabelField("--------------------Stats-----------------------");	  	
		EditorGUILayout.LabelField("");
		
		
		EditorGUILayout.LabelField("Item Type: " + myTarget.Type);   	
		EditorGUILayout.LabelField("");
		
		if(myTarget.Type == "Hat" || myTarget.Type == "Gloves" || myTarget.Type == "Panoply" || myTarget.Type == "Boots" || myTarget.Type == "Belt" || myTarget.Type == "Pauldrons"){
			
			myTarget.Damage = 0;
			myTarget.Source = 0;								 		 		
			myTarget.Steadiness = 0;

			myTarget.Life = EditorGUILayout.IntSlider ("Life", myTarget.Life, 0, 500000);
			ProgressBar (myTarget.Life / 500000, myTarget.Life.ToString());
			
			myTarget.Armor = EditorGUILayout.IntSlider ("Armor", myTarget.Armor, 0, 500000);
			ProgressBar (myTarget.Armor / 500000, myTarget.Armor.ToString());
			
			myTarget.Revitalization = EditorGUILayout.Slider ("Revitalization", myTarget.Revitalization, 0, 100);
			ProgressBar (myTarget.Revitalization / 100.0f, myTarget.Revitalization.ToString());
			
			myTarget.DeathRes = EditorGUILayout.Slider ("Death Resistance", myTarget.DeathRes, 0, 100);
			ProgressBar (myTarget.DeathRes / 100.0f, myTarget.DeathRes.ToString());

			myTarget.Acceleration = EditorGUILayout.Slider ("Acceleration", myTarget.Acceleration, 0, 100);
			ProgressBar (myTarget.Acceleration / 100.0f, myTarget.Acceleration.ToString());

		}
		else if(myTarget.Type == "Right Handed" || myTarget.Type == "Left Handed" || myTarget.Type == "Two Handed"){
			
			myTarget.Life = 0;				 				
			myTarget.Armor = 0;							       			
			myTarget.DeathRes = 0;   
			myTarget.Revitalization = 0;

			myTarget.Damage = EditorGUILayout.IntSlider ("Damage", myTarget.Damage, 0, 500000);
			ProgressBar (myTarget.Damage / 500000, myTarget.Damage.ToString());
			
			myTarget.Steadiness = EditorGUILayout.Slider ("Steadiness", myTarget.Steadiness, 0, 100);
			ProgressBar (myTarget.Steadiness / 100.0f, myTarget.Steadiness.ToString());
			
			myTarget.Source = EditorGUILayout.IntSlider ("Source", myTarget.Source, 0, 500000);
			ProgressBar (myTarget.Source / 500000, myTarget.Source.ToString());
		
			myTarget.Acceleration = EditorGUILayout.Slider ("Acceleration", myTarget.Acceleration, 0, 100);
			ProgressBar (myTarget.Acceleration / 100.0f, myTarget.Acceleration.ToString());

		}
		else if(myTarget.Type == "Spirit"){

			myTarget.Life = EditorGUILayout.IntSlider ("Life", myTarget.Life, 0, 500000);
			ProgressBar (myTarget.Life / 500000, myTarget.Life.ToString());
			
			myTarget.Armor = EditorGUILayout.IntSlider ("Armor", myTarget.Armor, 0, 500000);
			ProgressBar (myTarget.Armor / 500000, myTarget.Armor.ToString());
			
			myTarget.Damage = EditorGUILayout.IntSlider ("Damage", myTarget.Damage, 0, 500000);
			ProgressBar (myTarget.Damage / 500000, myTarget.Damage.ToString());
			
			myTarget.Source = EditorGUILayout.IntSlider ("Source", myTarget.Source, 0, 500000);
			ProgressBar (myTarget.Source / 500000, myTarget.Source.ToString());
			
			myTarget.Acceleration = EditorGUILayout.Slider ("Acceleration", myTarget.Acceleration, 0, 100);
			ProgressBar (myTarget.Acceleration / 100.0f, myTarget.Acceleration.ToString());
			
			myTarget.DeathRes = EditorGUILayout.Slider ("Death Resistance", myTarget.DeathRes, 0, 100);
			ProgressBar (myTarget.DeathRes / 100.0f, myTarget.DeathRes.ToString());
			
			myTarget.Steadiness = EditorGUILayout.Slider ("Steadiness", myTarget.Steadiness, 0, 100);
			ProgressBar (myTarget.Steadiness / 100.0f, myTarget.Steadiness.ToString());
			
			myTarget.Revitalization = EditorGUILayout.Slider ("Revitalization", myTarget.Revitalization, 0, 100);
			ProgressBar (myTarget.Revitalization / 100.0f, myTarget.Revitalization.ToString());
			
		}
		else if(myTarget.Type == "Potion"){
			
			myTarget.Life = 0;				 				
			myTarget.Armor = 0;
			myTarget.Damage = 0;
			myTarget.Source = 0;								
			myTarget.Acceleration = 0;   			
			myTarget.Revitalization = 0;    	 				
			myTarget.Steadiness = 0;         			
			myTarget.DeathRes = 0;   		 		
			
			myTarget.Life = EditorGUILayout.IntSlider ("MaxLife", myTarget.Life, 0, 10000);
			ProgressBar (myTarget.Life / 5000.0f, myTarget.Life.ToString());
			
			myTarget.Source = EditorGUILayout.IntSlider ("Source", myTarget.Source, 0, 10000);
			ProgressBar (myTarget.Source / 5000.0f, myTarget.Source.ToString());	
			
		}
		
		//-----------------Hidden---------------------------------------
		
		
		EditorGUILayout.LabelField("");
		EditorGUILayout.LabelField("--------------------Hidden-----------------------");	  	
		EditorGUILayout.LabelField("");
		
		EditorGUILayout.LabelField("PlayerID: " + myTarget.PlayerID);
		EditorGUILayout.LabelField("ItemID: " + myTarget.ItemID);

		if(GUI.changed){
			
			EditorUtility.SetDirty(target);
			this.Repaint();
			
		}
		
	}
	
	
	void ProgressBar (float value1, string label) {
		// Get a rect for the progress bar using the same margins as a textfield:
		Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
		EditorGUI.ProgressBar (rect, value1, label);
		EditorGUILayout.Space ();
	}
	
	
}