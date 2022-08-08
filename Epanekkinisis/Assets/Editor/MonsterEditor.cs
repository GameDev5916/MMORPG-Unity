using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MonsterLife))]
[CanEditMultipleObjects]

public class MonsterEditor : Editor {

	string[] monstertypes = new string[2] {"Monster", "Special"};

	int MonsterType;


	public override void OnInspectorGUI(){

		MonsterLife myTarget = (MonsterLife)target;
		
		myTarget.Name = EditorGUILayout.TextField(new GUIContent("Name:"), myTarget.Name);

		MonsterType = Array.IndexOf(monstertypes, myTarget.Type);
		MonsterType = EditorGUILayout.Popup("Type:", MonsterType, monstertypes);
		myTarget.Type = monstertypes[MonsterType];

		myTarget.xp = EditorGUILayout.IntField(new GUIContent("XP Given:"), myTarget.xp);

		SerializedProperty Lines = serializedObject.FindProperty("Lines");

		EditorGUILayout.PropertyField(Lines, true);


    	//------------------Stats---------------------------------------
    	  
		
    	EditorGUILayout.LabelField("");
    	EditorGUILayout.LabelField("--------------------Stats-----------------------");	  	
    	EditorGUILayout.LabelField("");
    	
		myTarget.Level = EditorGUILayout.IntSlider ("Level", myTarget.Level, 1, 50);
		ProgressBar (myTarget.Level / 50.0f, myTarget.Level.ToString());
		
		myTarget.MaxLife = EditorGUILayout.IntSlider ("Life", myTarget.MaxLife, 0, 1000);
		ProgressBar (myTarget.MaxLife / 1000.0f, myTarget.MaxLife.ToString());
		
		myTarget.MaxArmor = EditorGUILayout.IntSlider ("Armor", myTarget.MaxArmor, 0, 100);
		ProgressBar (myTarget.MaxArmor / 100.0f, myTarget.MaxArmor.ToString());
		
		myTarget.MaxDamage = EditorGUILayout.IntSlider ("Damage", myTarget.MaxDamage, 0, 100);
		ProgressBar (myTarget.MaxDamage / 100.0f, myTarget.MaxDamage.ToString());
        
		myTarget.Aim = EditorGUILayout.IntSlider ("Aim", myTarget.Aim, 0, 100);
		ProgressBar (myTarget.Aim / 100.0f, myTarget.Aim.ToString());

        //-----------------Hidden---------------------------------------
        
        
        EditorGUILayout.LabelField("");
    	EditorGUILayout.LabelField("--------------------Hidden-----------------------");	  	
    	EditorGUILayout.LabelField("");
        

		EditorGUILayout.LabelField(new GUIContent("Target Class:" + myTarget.classi.ToString()));

		
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