using UnityEngine;
using System.Collections;

public class LoadingFade : MonoBehaviour {
	
	GUIText Loading;
	bool way;
	public float fade;
	public float speed;
	public string[] lols;

	// Use this for initialization
	void Start () {
		Cursor.visible = true;
		Loading = GameObject.Find("GUI").GetComponent<GUIText>();
		Loading.pixelOffset = new Vector2(Random.Range(-500, 200), Random.Range(250, -250));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(Loading.color.a <= 0){
			way = false;
			Loading.text = lols[Random.Range(0, lols.Length-1)];
			Loading.pixelOffset = new Vector2(Random.Range(-500, 200), Random.Range(250, -250));
			Loading.fontSize = Random.Range(20, 60);
		}
		else if(Loading.color.a >= 1){
			way = true;
		}
		
		
		fade = Loading.color.a;
		
		
		if(way){
			fade -= speed;
			Loading.color = new Color(0.898039215686275f, 0.894117647058824f, 0.886274509803922f, fade);
		}
		else if(!way){
			fade += speed;
			Loading.color = new Color(0.898039215686275f, 0.894117647058824f, 0.886274509803922f, fade);
		}
		
	}
}
