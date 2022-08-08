using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {
	
	public GameObject Target, target;
	
	public bool none, mouse, recognizer;
	
	public string XXXXXXXXXXXXXX = "XXXXXXXXXXXXXXX";
	
	public bool Quests;
	public bool Skills;
	public bool Shope;
	public bool Character;
	public bool Inventory;
	public bool WorldMap;
	public bool NPC;
	public bool Chat;
	public bool Riddles;
	public bool Compass;
	
	
	void Start () {

		Screen.SetResolution(1117, 608, false);
		Cursor.visible = false;
		target = Instantiate(Target, new Vector3(0.5f, 0.5f, 0), Quaternion.identity) as GameObject;
		
		Info.SetLevels();
		Info.SetQuests();

	}


	// Update is called once per frame
	void Update () {
		
		if(Screen.width < 1117 || Screen.height < 608){
			Screen.SetResolution(1117, 608, false);
		}
				
		if(!recognizer && none && Input.GetMouseButtonDown(0)){
			recognizer = true;
			gameObject.SendMessage("Check");
		}

		if(recognizer && none && Input.GetMouseButtonUp(0)){
			gameObject.SendMessage("WaitNcheK");
		}

		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
			
			GetComponent<Chat>().render = !GetComponent<Chat>().render;				
			gameObject.SendMessage("Check");
			
		}
		
		if(Input.GetKeyUp(KeyCode.E) && GetComponentInChildren<Pickup>().TempItem.GetComponent<NPC>()){
						
			GetComponentInChildren<NPCQuests>().CurrentNPC = GetComponentInChildren<Pickup>().TempItem;

			CloseAll("NPCQuests");
			transform.GetComponentInChildren<NPCQuests>().render = !transform.GetComponentInChildren<NPCQuests>().render;
			
			gameObject.SendMessage("Check");
			
		}
		
		if(Input.GetKeyUp(KeyCode.E) && GetComponentInChildren<Pickup>().TempItem.GetComponent<Riddle>()){
			
			transform.GetComponentInChildren<Riddles>().Name = GetComponentInChildren<Pickup>().TempItem.GetComponent<Riddle>().Name;
			transform.GetComponentInChildren<Riddles>().Question = GetComponentInChildren<Pickup>().TempItem.GetComponent<Riddle>().Question;
			transform.GetComponentInChildren<Riddles>().Answer = GetComponentInChildren<Pickup>().TempItem.GetComponent<Riddle>().Answer;
			
			CloseAll("Riddles");
			transform.GetComponentInChildren<Riddles>().render = !transform.GetComponentInChildren<Riddles>().render;
			
			gameObject.SendMessage("Check");
			
		}
		
		if(Input.GetKeyUp(KeyCode.Q)){
		
			CloseAll("Quests");
			transform.GetComponentInChildren<Quests>().render = !transform.GetComponentInChildren<Quests>().render;
			
			gameObject.SendMessage("Check");
			
		}
		
		if(Input.GetKeyUp(KeyCode.N)){
			
			CloseAll("Skills");
			transform.GetComponentInChildren<Skills>().render = !transform.GetComponentInChildren<Skills>().render;
			
			gameObject.SendMessage("Check");
			
		}
		
		if(Input.GetKeyUp(KeyCode.C)){

			CloseAll("Character");
			transform.GetComponentInChildren<Character>().render = !transform.GetComponentInChildren<Character>().render;
			
			gameObject.SendMessage("Check");
			
		}
				
		if(Input.GetKeyUp(KeyCode.I)){
			
			CloseAll("Inventory");
			transform.GetComponentInChildren<Inventory_GUI>().render = !transform.GetComponentInChildren<Inventory_GUI>().render;
			
			gameObject.SendMessage("Check");
			
		}
		
		if(Input.GetKeyUp(KeyCode.M)){
			
			CloseAll("WorldMap");
			transform.GetComponentInChildren<WorldMap>().render = !transform.GetComponentInChildren<WorldMap>().render;
			
			gameObject.SendMessage("Check");
			
		}
		
		if(Input.GetKeyUp(KeyCode.Z)){
			
			GetComponentInChildren<Compass>().render = !GetComponentInChildren<Compass>().render;

		}
		
	}
	
	
	public void Shop(){
		
		CloseAll("Shop");
		transform.GetComponentInChildren<Shop>().render = !transform.GetComponentInChildren<Shop>().render;
		
		GetComponentInChildren<Shop>().CurrentNPC = GetComponentInChildren<Pickup>().TempItem;

		gameObject.SendMessage("Check");
		
	}
	

	void CloseAll(string exe){
		
		if(exe != "Quests"){
			gameObject.GetComponentInChildren<Quests>().render = false;
		}
		if(exe != "Skills"){
			gameObject.GetComponentInChildren<Skills>().render = false;
		}
		if(exe != "Character"){
			gameObject.GetComponentInChildren<Character>().render = false;
		}
		if(exe != "Inventory"){
			gameObject.GetComponentInChildren<Inventory_GUI>().render = false;
		}
		if(exe != "NPCQuests"){
			gameObject.GetComponentInChildren<NPCQuests>().render = false;
		}
		if(exe != "WorldMap"){
			gameObject.GetComponentInChildren<WorldMap>().render = false;
		}
		if(exe != "Shop"){
			gameObject.GetComponentInChildren<Shop>().render = false;
		}
		if(exe != "Riddles"){
			gameObject.GetComponentInChildren<Riddles>().render = false;
		}
		
	}
	
	
	public void Check()
	{
		
		Chat = GetComponent<Chat>().render;
		Quests = GetComponentInChildren<Quests>().render;
		Skills = GetComponentInChildren<Skills>().render;
		Shope = GetComponentInChildren<Shop>().render;
		Character = GetComponentInChildren<Character>().render;
		Inventory = GetComponentInChildren<Inventory_GUI>().render;
		NPC = GetComponentInChildren<NPCQuests>().render;
		Riddles = GetComponentInChildren<Riddles>().render;
		WorldMap = GetComponentInChildren<WorldMap>().render;
		Compass = GetComponentInChildren<Compass>().render;


		if(!Chat && !Quests && !Skills && !Shope && !Character && !Inventory && !WorldMap && !NPC && !Riddles){
			none = true;
		}
		else{
			none = false;
		}

		
		if(none){

			if(!recognizer){
			
				mouse = false;
				GetComponent<MouseLook>().enabled = true;
				GetComponent<swap_cams>().ActiveCam.GetComponent<MouseLook>().enabled = true;
				target.SetActive(true);

			}
			else{

				mouse = false; 
				GetComponent<MouseLook>().enabled = false;
				GetComponent<swap_cams>().ActiveCam.GetComponent<MouseLook>().enabled = false;	
				target.SetActive(true);

			}

		}
		else{
			
			mouse = true; 
			GetComponent<MouseLook>().enabled = false;
			GetComponent<swap_cams>().ActiveCam.GetComponent<MouseLook>().enabled = false;	
			target.SetActive(false);

		}
		
		Cursor.visible = mouse;
		
	}


	IEnumerator WaitNcheK () {

		yield return new WaitForSeconds(.5f);
		recognizer = false;
		gameObject.SendMessage("Check");
				
	}
	
}
