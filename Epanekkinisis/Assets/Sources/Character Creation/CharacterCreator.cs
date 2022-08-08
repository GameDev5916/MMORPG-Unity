using UnityEngine;
using System.Collections;

public class CharacterCreator : MonoBehaviour {
	
	public Texture black;
	public float height1;
	public float height2;
	public bool created;
	public bool done;

	void OnGUI () {

		if(!done){

			if(created){
				height1 -= .5f;
				height2 += .5f;
			}

			GUI.DrawTexture(new Rect(0,height1,Screen.width,179), black);
			GUI.DrawTexture(new Rect(0,height2,Screen.width,179), black);
			
			if(height1 < 200f && height2 > 630f){

				done = true;

				transform.FindChild("Start Camera").SendMessage("Move");

			}
			
		}
		
	}
	
}
