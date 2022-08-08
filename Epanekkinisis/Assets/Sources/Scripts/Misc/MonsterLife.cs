using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterLife : MonoBehaviour {
	
	public string Name;
	public string Type;
	public int Level;
	public bool dead;
	public int xp;
	
	//------------------------------------

	public int Aim;

	public int MaxLife;
	public int MaxArmor;
	public int MaxDamage;
	
	public int MomentaryLife;
	public int MomentaryArmor;
	public int MomentaryDamage;
	
	//-------------------------------------
	
	public string classi;
	
	public AudioClip[] Lines;
	
	public List<Damages> Damages = new List<Damages>();
	
	public int PercentLife;
	
	bool found;
	int TotalDamage;
	
	RaycastHit hit;
	
	
	// Use this for initialization
	void Start () {
	
		if(Name.Contains("Charon")){

			MaxLife = GameObject.Find("Character").GetComponent<Player>().MaxLife * Mathf.RoundToInt((GameObject.Find("Character").GetComponent<Player>().DeathRes / 10));
			MaxArmor = GameObject.Find("Character").GetComponent<Player>().MaxArmor * Mathf.RoundToInt((GameObject.Find("Character").GetComponent<Player>().DeathRes / 10));
			MaxDamage = GameObject.Find("Character").GetComponent<Player>().MaxDamage * Mathf.RoundToInt((GameObject.Find("Character").GetComponent<Player>().DeathRes / 10));

		}

		MomentaryLife = MaxLife;
		MomentaryArmor = MaxArmor;
		MomentaryDamage = MaxDamage;

	}
	
	
	// Update is called once per frame
	void Update () {
		
		MomentaryArmor = MaxArmor*MomentaryLife;
		MomentaryDamage = MaxDamage*MomentaryLife;
		
		PercentLife = (100*MomentaryLife/MaxLife);
		
		if(MomentaryLife <= 0){
			
			MomentaryLife = 0;
						
			if(!dead){
				
				GetComponent<ItemDrop>().CreateItem(classi, Level);
				
				//Share XPs
				for(int i = 0; i <= Damages.Count-1; i++){					
					Damages[i].Player.GetComponent<Player>().AddXP(xp*Damages[i].Damage/TotalDamage);					
				}
								
				dead = true;
				
			}
			
			transform.parent.GetComponent<AnimationManager>().CustomAnimation("Die");	 
			
		}
		
	}



	public void DamageMe(int dmg){

		MomentaryLife = (MomentaryLife + MomentaryArmor) - dmg;

	}


	
	void AddDamage( GameObject Player){
		
		found = false;
		
		for(int i = 0; i <= Damages.Count-1; i++){
			
			if(Damages[i].Player == Player){
				
				Damages[i].Damage++;
				found = true;
				
			}
			
		}
		
		if(!found){
			Damages.Add(new Damages(Player));
		}
		
		TotalDamage++;
		
	}
	
}
