using UnityEngine;
using System.Collections;

public class MoveTexture : MonoBehaviour {

	public float x, y, TimeSpeed;


	void  Update (){

		Vector2 offset = GetComponent<Renderer>().material.mainTextureOffset;
		
		offset += new Vector2(x, y)*Time.deltaTime*TimeSpeed;
		
		GetComponent<Renderer>().material.mainTextureOffset = offset;
		
	}

}
