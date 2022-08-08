using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public Texture Ierokiryx;
	public Texture Witcher;
	public Texture Corsair;
	public Texture Venator;
	
	public string[] Description;
	
	public GUISkin skin;

	public string Sex;
	public string Name;
	public string Classi;

	public bool Sent;

	void OnGUI () {
		
		if(!GetComponent<CharacterCreator>().created){
	
			GUI.skin = skin;
			
			if(new Rect(170, 50, 240, 100).Contains(Event.current.mousePosition) && Input.GetMouseButtonUp(0)){
				Classi = "Ierokiryx";
			}
			if(new Rect(400, 50, 470, 100).Contains(Event.current.mousePosition) && Input.GetMouseButtonUp(0)){
				Classi = "Witcher";
			}
			if(new Rect(630, 50, 700, 100).Contains(Event.current.mousePosition) && Input.GetMouseButtonUp(0)){
				Classi = "Corsair";
			}
			if(new Rect(860, 50, 930, 100).Contains(Event.current.mousePosition) && Input.GetMouseButtonUp(0)){
				Classi = "Venator";
			}

			GUI.DrawTexture(new Rect(160, 60, 100, 100), Ierokiryx);
			GUI.DrawTexture(new Rect(390, 60, 100, 100), Witcher);
			GUI.DrawTexture(new Rect(620, 60, 100, 100), Corsair);
			GUI.DrawTexture(new Rect(850, 60, 100, 100), Venator);
			
			if (Classi == "Ierokiryx"){
				skin.label.normal.textColor = Color.yellow;
				skin.textArea.normal.textColor = Color.yellow;
				GUI.TextArea (new Rect(0, 250, 370, 200), Description[0], 500);
			}
			else if(Classi == "Witcher"){
				skin.label.normal.textColor = Color.red;
				skin.textArea.normal.textColor = Color.red;
				GUI.TextArea(new Rect(0, 250, 370, 200), Description[1], 500);
			}
			else if(Classi == "Corsair"){
				skin.label.normal.textColor = Color.cyan;
				skin.textArea.normal.textColor = Color.cyan;
				GUI.TextArea(new Rect(0, 250, 370, 300), Description[2], 500);
			}
			else if(Classi == "Venator"){
				skin.label.normal.textColor = Color.green;
				skin.textArea.normal.textColor = Color.green;
				GUI.TextArea(new Rect(0, 250, 370, 200), Description[3], 500);
			}

			if(GUI.Button(new Rect(860, 270, 100, 30), "Male")){
				Sex = "Male";
			}
			if(GUI.Button(new Rect(940, 295, 100, 30), "Female")){
				Sex = "Female";
			}

			skin.label.normal.textColor = Color.yellow;
			GUI.Label(new Rect(142, 15, 130, 30), "Ierokiryx");
			skin.label.normal.textColor = Color.red;
			GUI.Label(new Rect(375, 15, 130, 30), "Witcher");
			skin.label.normal.textColor = Color.cyan;
			GUI.Label(new Rect(607, 15, 130, 30), "Corsair");
			skin.label.normal.textColor = Color.green;
			GUI.Label(new Rect(840, 15, 130, 30), "Venator");		

			Name = GUI.TextField(new Rect(Screen.width/2 - 150/2, 450, 150, 200), Name, 10);		
			
			GUI.Label(new Rect(Screen.width/2 - 150, 550, 300, 30), GameObject.Find("Southbridge").GetComponent<Southbridge>().msg);

			GUI.Button(new Rect(Screen.width/2 - 150/2 + 25, 500, 100, 40), "Start");

			if(!Sent && (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter)){

				Sent = true;
				GameObject.Find("Southbridge").GetComponent<Southbridge>().Character(Name.Trim(), Sex, Classi);

			}

			                                                                                                                               

		}
		
	}	

}
