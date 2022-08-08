using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	//armor += armor*poso/100;
	
	//Publics
	
	public int PlayerID;
	public string Name;
	public int Level;
	public string Classi;
	public int Coins;
	public string Sex;
	public int Xp;
	
	public GameObject LevelUpfx;
	public GameObject CharonPoint;
	
	public string XXXXXXXXXXXXXXX = "XXXXXXXXXXXXXXX";
	
	public int MaxLife;
	public int MaxSource;
	public int MaxArmor;
	public int MaxDamage;
	
	public float Steadiness;		//(%)
	public float Revitalization;    //(%)
	public float DeathRes;     	 	//(%)
	public float Acceleration;		//(%)
	
	public int[] Status;
	
	public string XXXXXXXXXXXXXXXX = "XXXXXXXXXXXXXXX";
	
	public int MomentaryLife;
	public int MomentarySource;
	public int MomentaryArmor;
	public int MomentaryDamage;
	
	
	// Use this for initialization
	void Start () {
		
		MomentaryLife = MaxLife;
		MomentarySource = MaxSource;
		
		gameObject.SendMessage("Regen");
		
		//--------------------------------
		
		PlayerID = Info.PlayerID;
		
		if(!Info.IsReg && (GameObject.Find("GameManager") || GameObject.Find("Southbridge"))){
			
			if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){
				GameObject.Find("Southbridge").GetComponent<Southbridge>().GetInfo();
			}
			else{
				
				GameObject.Find("GameManager").GetComponent<PlayersManager>().GetInfo();
				
				if(!GameObject.Find("GameManager").GetComponent<PlayersManager>().MonstersSet){		
					GameObject.Find("GameManager").GetComponent<PlayersManager>().SpawnMyPlayer();
				}	
				
			}
			
		}
		
		gameObject.SendMessage("UpdateStatus"); //Skills, items etc
		
	}
	
	
	public void FixedUpdate () {
		
		MomentaryArmor = MaxArmor*MomentaryLife;
		MomentaryDamage = MaxDamage*MomentaryLife;
		
		if(MomentaryLife < 0){
			
			MomentaryLife = 0;
			Instantiate(CharonPoint, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.rotation);
			
		}
		
	}
	
	
	IEnumerator Regen()
	{
		
		yield return new WaitForSeconds((100-Revitalization)/75);
		
		if(MomentaryLife < MaxLife){
			MomentaryLife += 1;
		}
		
		if(MomentarySource < MaxSource){
			MomentarySource += 1;
		}
		
		gameObject.SendMessage("Regen");
		
	}
	
	
	public void UpdateStatus(){	//Called on Start or on Equip/Unequip
		
		MaxLife = 100;
		MaxSource = 100;
		MaxArmor = 50;
		MaxDamage = 10;
		Steadiness = 1.5f;
		Revitalization = 0.1f;
		Acceleration = 1f;
		DeathRes = 1f;
		
		for(int i = 29; i <= 38; i++){
			
			if(transform.parent.GetComponentInChildren<Inventory_Functions>().Items[i]){
				
				MaxLife += transform.parent.GetComponentInChildren<Inventory_Functions>().Items[i].Life;
				MaxSource += transform.parent.GetComponentInChildren<Inventory_Functions>().Items[i].Source;
				MaxArmor += transform.parent.GetComponentInChildren<Inventory_Functions>().Items[i].Armor;
				MaxDamage += transform.parent.GetComponentInChildren<Inventory_Functions>().Items[i].Damage;
				Steadiness += transform.parent.GetComponentInChildren<Inventory_Functions>().Items[i].Steadiness;
				Revitalization += transform.parent.GetComponentInChildren<Inventory_Functions>().Items[i].Revitalization;
				Acceleration += transform.parent.GetComponentInChildren<Inventory_Functions>().Items[i].Acceleration;
				DeathRes += transform.parent.GetComponentInChildren<Inventory_Functions>().Items[i].DeathRes;
				
			}
			
		}		
		
	}
	
	
	public void Infose(string Name1, int Level1, string Class1, int Coins1, string Gender1, int XP1){	//Get Player Info
		
		Name = Name1;
		Level = Level1;
		Classi = Class1;
		Coins = Coins1;
		Sex = Gender1;
		Xp = XP1;
		
	}
	
	
	public void AddXP(int xp){
		
		Xp += xp;
		
		if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){	
			GameObject.Find("Southbridge").GetComponent<Southbridge>().AddXP(xp);
		}
		else{
			GameObject.Find("GameManager").GetComponent<PlayersManager>().AddXP(xp);	
		}
		
		if(Info.CheckLevel(Xp, Level)){
			LevelUp();
		}
		
	}
	
	
	public void LevelUp(){
		
		Level++;
		
		Instantiate(LevelUpfx, transform.parent.position, transform.parent.rotation);
		
		if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){	
			GameObject.Find("Southbridge").GetComponent<Southbridge>().LevelUpen(Level);
		}
		else{
			GameObject.Find("GameManager").GetComponent<PlayersManager>().LevelUpen(Level);
		}
		
	}
	
	
	public void AddCoins(int Coins){
		
		Coins += Coins;
		
		if(GameObject.Find("Terrain").GetComponent<MapInfo>().Name == "Southbridge"){				
			GameObject.Find("Southbridge").GetComponent<Southbridge>().AddCoins(Coins);			
		}
		else{			
			GameObject.Find("Southbridge").GetComponent<Southbridge>().AddCoins(Coins);				
		}
		
	}
	
	
	public IEnumerator MaxLifeRegen(){
		
		yield return new WaitForSeconds(0.5f);
		
		if(MomentaryLife < MaxLife){
			MomentaryLife++;
		}
		
	}
	
	
	public void DamageMe (int dmg){		
		MomentaryLife = (MomentaryLife + MomentaryArmor) - dmg;		
	}
	
}
