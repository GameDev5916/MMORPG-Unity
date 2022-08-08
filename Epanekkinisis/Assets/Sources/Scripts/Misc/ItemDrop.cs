using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Item))]

public class ItemDrop : MonoBehaviour {
	
	//Publics
	
	public string XXXXXXXXXXXXXXX = "XXXXXXXXXXXXXXX";
	
	Item DropItem;
	public int ItemChance;
	
	int GradeFactor;
	
	public string XXXXXXXXXXXXXXXXXX = "XXXXXXXXXXXXXXX";
	
	public GameObject Coins;
	public int CoinsChance;

	public string XXXXXXXXXXXXXXXXXXX = "XXXXXXXXXXXXXXX";
	
	public GameObject WitcherUnique;
	public GameObject CorsairUnique;
	public GameObject VenatorUnique;
	public GameObject IerokiryxUnique;
	
	public int UniqueChance;
	
	public string XXXXXXXXXXXXXXXXXXXXX = "XXXXXXXXXXXXXXX";
	
	public GameObject MiscItem;
	public int MiscChance;
	
	public string XXXXXXXXXXXXXXXXXXXXXXXX = "XXXXXXXXXXXXXXXXXXX";
	
	GameObject Item1;
	public GameObject Wand, Sphere, Greatsword, Sword, Pistol, Hand_Cannon, Handblade, Handgun, Bow, Thuribulum, Transire, Chain, Armor, Spirit;
	public float DropSpeed = 2.5f;
	int templvl;

	
	//Privates
	
	string[,] WitcherItems = new string[130,2];
	string[,] CorsairItems = new string[140,2];
	string[,] VenatorItems = new string[130,2];
	string[,] IerokiryxItems = new string[130,2];
	
	string[] Types = new string[11];
	
	
	// Use this for initialization
	void Start () {	
		
		Types[0] = "Hat";
		Types[1] = "Panoply";
		Types[2] = "Gloves";
		Types[3] = "Boots";
		Types[4] = "Belt";
		Types[5] = "Pauldrons";
		Types[6] = "Spirit";
		Types[7] = "Right Handed";
		Types[8] = "Left Handed";
		Types[9] = "Two Handed";
		Types[10] = "Potion";		
		
		SetArrays();
		
		DropItem = GetComponent<Item>();
		
	}
	
	
	public void CreateItem(string classi, int level){
		

		int random2 = UnityEngine.Random.Range(1, 100); //Coins
		int random3 = UnityEngine.Random.Range(1, 100);	//Unique
		int random4 = UnityEngine.Random.Range(1, 100);	//Misc
		int random5 = UnityEngine.Random.Range(1, 100);	//Item
		


		//------------------------------------------------------------------------------------------------------------------------------------------------------------------
		
		if(random2 <= CoinsChance){
			
			GameObject Coins1 = Instantiate(Coins, new Vector3(UnityEngine.Random.Range(transform.position.x - 3, transform.position.x + 3), UnityEngine.Random.Range(transform.position.y - 3, transform.position.y + 3), transform.position.z), transform.rotation) as GameObject;
			Coins1.GetComponent<Item>().Coins = UnityEngine.Random.Range(DropItem.Level, DropItem.Level*2);
			
		}
		
		//------------------------------------------------------------------------------------------------------------------------------------------------------------------
		
		
		
		
		
		//------------------------------------------------------------------------------------------------------------------------------------------------------------------
		
		if(random3 <= MiscChance){
			
			Instantiate(MiscItem, new Vector3(UnityEngine.Random.Range(transform.position.x - 3, transform.position.x + 3), UnityEngine.Random.Range(transform.position.y - 3, transform.position.y + 3), transform.position.z), transform.rotation);
			
		}
		
		//------------------------------------------------------------------------------------------------------------------------------------------------------------------
		
		
		
		
		//------------------------------------------------------------------------------------------------------------------------------------------------------------------
		
		if(random4 <= UniqueChance && WitcherUnique != null && CorsairUnique != null && VenatorUnique != null && IerokiryxUnique != null){
			
			if(classi == "Witcher"){
				Instantiate(WitcherUnique, new Vector3(UnityEngine.Random.Range(transform.position.x - 3, transform.position.x + 3), UnityEngine.Random.Range(transform.position.y - 3, transform.position.y + 3), transform.position.z), transform.rotation);
			}
			else if(classi == "Corsair"){
				Instantiate(CorsairUnique, new Vector3(UnityEngine.Random.Range(transform.position.x - 3, transform.position.x + 3), UnityEngine.Random.Range(transform.position.y - 3, transform.position.y + 3), transform.position.z), transform.rotation);
			}
			else if(classi == "Venator"){
				Instantiate(VenatorUnique, new Vector3(UnityEngine.Random.Range(transform.position.x - 3, transform.position.x + 3), UnityEngine.Random.Range(transform.position.y - 3, transform.position.y + 3), transform.position.z), transform.rotation);
			}
			else if(classi == "Ierokiryx"){
				Instantiate(IerokiryxUnique, new Vector3(UnityEngine.Random.Range(transform.position.x - 3, transform.position.x + 3), UnityEngine.Random.Range(transform.position.y - 3, transform.position.y + 3), transform.position.z), transform.rotation);
			}
			
		}
		
		//------------------------------------------------------------------------------------------------------------------------------------------------------------------
		
		
		
		
		//------------------------------------------------------------------------------------------------------------------------------------------------------------------
		
		if(random5 <= ItemChance){
			
			DropItem.Type = Types[UnityEngine.Random.Range(0, 9)];
			
			if(DropItem.Type != "Potion" || DropItem.Type != "Misc"){
				
				int GradeRoll = UnityEngine.Random.Range(1, 100);
				
				if(GradeRoll <= 40){
					DropItem.Grade = "Simplex";
					GradeFactor = 1;
				}
				else if(GradeRoll > 40 && GradeRoll <= 70){
					DropItem.Grade = "Enhanced";
					GradeFactor = 2;
				}
				else if(GradeRoll > 70 && GradeRoll <= 85){
					DropItem.Grade = "Enchanted";
					GradeFactor = 3;
				}
				else if(GradeRoll > 85 && GradeRoll <= 95){
					DropItem.Grade = "Malfeasant";
					GradeFactor = 4;
				}
				else if(GradeRoll > 95){
					DropItem.Grade = "Extinct";
					GradeFactor = 5;
				}
				
				
				DropItem.Coins = UnityEngine.Random.Range(Mathf.Abs(level - Mathf.RoundToInt(level/2)), level + Mathf.RoundToInt(level/2));
				DropItem.Class = classi;
				DropItem.Level = level;

				if(level < 3){
					templvl = 3;
				}
				else if(level > 67){
					templvl = 67;
				}
				else{
					templvl = level;
				}

				if(classi == "Witcher"){ 		
					DropItem.ItemName = WitcherItems[UnityEngine.Random.Range(templvl-2, templvl+2),0];
				}
				else if(classi == "Corsair"){
					DropItem.ItemName = CorsairItems[UnityEngine.Random.Range(templvl-2, templvl+2),0];
				}
				else if(classi == "Venator"){
					DropItem.ItemName = VenatorItems[UnityEngine.Random.Range(templvl-2, templvl+2),0];
				}
				else if(classi == "Ierokiryx"){
					DropItem.ItemName = IerokiryxItems[UnityEngine.Random.Range(templvl-2, templvl+2),0];
				}
				
				
				if(DropItem.ItemName.Contains("Wand")){
					DropItem.Type = "Right Handed";
				}
				else if(DropItem.ItemName.Contains("Sphere")){
					DropItem.Type = "Left Handed";
				}
				else if(DropItem.ItemName.Contains("Sword")){
					DropItem.Type = "Two Handed";
				}
				else if(DropItem.ItemName.Contains("Greatsword")){
					DropItem.Type = "Two Handed";
				}
				else if(DropItem.ItemName.Contains("Pistol")){
					DropItem.Type = "Left Handed";
				}
				else if(DropItem.ItemName.Contains("Hand Cannon")){
					DropItem.Type = "Two Handed";
				}
				else if(DropItem.ItemName.Contains("Handblade")){
					DropItem.Type = "Right Handed";
				}
				else if(DropItem.ItemName.Contains("Handgun")){
					DropItem.Type = "Left Handed";
				}
				else if(DropItem.ItemName.Contains("Bow")){
					DropItem.Type = "Two Handed";
				}
				else if(DropItem.ItemName.Contains("Thuribulum")){
					DropItem.Type = "Right Handed";
				}
				else if(DropItem.ItemName.Contains("Transire")){
					DropItem.Type = "Left Handed";
				}
				else if(DropItem.ItemName.Contains("Chain")){
					DropItem.Type = "Two Handed";
				}
				else if(DropItem.ItemName.Contains("Hat")){
					DropItem.Type = "Hat";
				}
				else if(DropItem.ItemName.Contains("Panoply")){
					DropItem.Type = "Panoply";
				}
				else if(DropItem.ItemName.Contains("Gloves")){
					DropItem.Type = "Gloves";
				}
				else if(DropItem.ItemName.Contains("Boots")){
					DropItem.Type = "Boots";
				}
				else if(DropItem.ItemName.Contains("Belt")){
					DropItem.Type = "Belt";
				}
				else if(DropItem.ItemName.Contains("Pauldrons")){
					DropItem.Type = "Pauldrons";
				}			
				else if(DropItem.ItemName.Contains("Spirit")){
					DropItem.Type = "Spirit";
				}								
				
				
				
				if(DropItem.Grade != "Unique"){
					DropItem.ItemName = DropItem.Grade + " " + DropItem.ItemName;
				}
				
				
				
				if(DropItem.Type == "Right Handed"){
					
					DropItem.Damage = GradeFactor*DropItem.Level*UnityEngine.Random.Range(100, 500);
					DropItem.Steadiness = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					DropItem.Source = GradeFactor*DropItem.Level*UnityEngine.Random.Range(1, 100);
					DropItem.Acceleration = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					
				}
				else if(DropItem.Type == "Left Handed"){
					
					DropItem.Damage = GradeFactor*DropItem.Level*UnityEngine.Random.Range(500, 1000);
					DropItem.Steadiness = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					DropItem.Source = GradeFactor*DropItem.Level*UnityEngine.Random.Range(100, 200);
					
				}
				else if(DropItem.Type == "Two Handed"){
					
					DropItem.Life = GradeFactor*DropItem.Level*UnityEngine.Random.Range(500, 1000);
					DropItem.Armor = GradeFactor*DropItem.Level*UnityEngine.Random.Range(500, 1000);
					DropItem.Damage = UnityEngine.Random.Range(GradeFactor, DropItem.Level);
					DropItem.DeathRes = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					
				}
				else if(DropItem.Type == "Spirit"){
					
					DropItem.Life = UnityEngine.Random.Range(1, DropItem.Level+50);
					DropItem.Armor = UnityEngine.Random.Range(1, DropItem.Level+50);
					DropItem.Damage = UnityEngine.Random.Range(1, DropItem.Level+50);
					DropItem.Source = UnityEngine.Random.Range(1, DropItem.Level+50);
					DropItem.Acceleration = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					DropItem.Revitalization = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					DropItem.DeathRes = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					DropItem.Steadiness = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);

				}
				else if(DropItem.ItemName.Contains("Gloves")){
					
					DropItem.Life = GradeFactor*DropItem.Level*UnityEngine.Random.Range(250, 350);
					DropItem.Armor = GradeFactor*DropItem.Level*UnityEngine.Random.Range(100, 200);
					DropItem.Revitalization = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					DropItem.Acceleration = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					
				}	
				else if(DropItem.ItemName.Contains("Panoply")){
					
					DropItem.Life = GradeFactor*DropItem.Level*UnityEngine.Random.Range(700, 1000);
					DropItem.Armor = GradeFactor*DropItem.Level*UnityEngine.Random.Range(500, 1000);
					DropItem.Acceleration = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					DropItem.Revitalization = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					DropItem.DeathRes = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					
				}
				else if(DropItem.ItemName.Contains("Boots")){
					
					DropItem.Life = GradeFactor*DropItem.Level*UnityEngine.Random.Range(1, 200);
					DropItem.Armor = GradeFactor*DropItem.Level*UnityEngine.Random.Range(100, 200);
					DropItem.Acceleration = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					DropItem.Revitalization = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					
				}
				else if(DropItem.ItemName.Contains("Belt")){
					
					DropItem.Life = GradeFactor*DropItem.Level*UnityEngine.Random.Range(250, 350);
					DropItem.Armor = GradeFactor*DropItem.Level*UnityEngine.Random.Range(100, 200);
					DropItem.Revitalization = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					DropItem.Acceleration = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					
				}
				else if(DropItem.ItemName.Contains("Pauldrons")){
					
					DropItem.Life = GradeFactor*DropItem.Level*UnityEngine.Random.Range(250, 350);
					DropItem.Armor = GradeFactor*DropItem.Level*UnityEngine.Random.Range(100, 200);
					DropItem.Revitalization = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					DropItem.Acceleration = UnityEngine.Random.Range((0.1f*level*GradeFactor)/2, 0.1f*level*GradeFactor);
					
				}
				

				DropItem.Icon = Resources.Load("Icons/" + DropItem.ItemName) as Texture;
				DropItem.Model = Resources.Load("Models/" + DropItem.ItemName) as GameObject;


				if(DropItem.ItemName.Contains("Wand")){
					Item1 = Instantiate(Wand, transform.position, transform.rotation) as GameObject;
				}
				else if(DropItem.ItemName.Contains("Sphere")){
					Item1 = Instantiate(Sphere, transform.position, transform.rotation) as GameObject;
				}
				else if(DropItem.ItemName.Contains("Sword")){
					Item1 = Instantiate(Sword, transform.position, transform.rotation) as GameObject;
				}
				else if(DropItem.ItemName.Contains("Greatsword")){
					Item1 = Instantiate(Greatsword, transform.position, transform.rotation) as GameObject;
				}
				else if(DropItem.ItemName.Contains("Pistol")){
					Item1 = Instantiate(Pistol, transform.position, transform.rotation) as GameObject;
				}
				else if(DropItem.ItemName.Contains("Hand Cannon")){
					Item1 = Instantiate(Hand_Cannon, transform.position, transform.rotation) as GameObject;
				}
				else if(DropItem.ItemName.Contains("Handblade")){
					Item1 = Instantiate(Handblade, transform.position, transform.rotation) as GameObject;
				}
				else if(DropItem.ItemName.Contains("Handgun")){
					Item1 = Instantiate(Handgun, transform.position, transform.rotation) as GameObject;
				}
				else if(DropItem.ItemName.Contains("Bow")){
					Item1 = Instantiate(Bow, transform.position, transform.rotation) as GameObject;
				}
				else if(DropItem.ItemName.Contains("Thuribulum")){
					Item1 = Instantiate(Thuribulum, transform.position, transform.rotation) as GameObject;
				}
				else if(DropItem.ItemName.Contains("Transire")){
					Item1 = Instantiate(Transire, transform.position, transform.rotation) as GameObject;
				}
				else if(DropItem.ItemName.Contains("Chain")){
					Item1 = Instantiate(Chain, transform.position, transform.rotation) as GameObject;
				}
				else if(DropItem.ItemName.Contains("Spirit")){
					Item1 = Instantiate(Spirit, transform.position, transform.rotation) as GameObject;
				}
				else{
					Item1 = Instantiate(Armor, transform.position, transform.rotation) as GameObject;
				}					
				
				Item1.GetComponent<Item>().ItemName = DropItem.ItemName;
				Item1.GetComponent<Item>().Class = DropItem.Class;
				Item1.GetComponent<Item>().Grade = DropItem.Grade;
				Item1.GetComponent<Item>().Type = DropItem.Type;
				Item1.GetComponent<Item>().Coins = DropItem.Coins;
				Item1.GetComponent<Item>().Level = DropItem.Level;
				Item1.GetComponent<Item>().Icon = DropItem.Icon;
				Item1.GetComponent<Item>().Model = DropItem.Model;
				Item1.GetComponent<Item>().Life = DropItem.Life;
				Item1.GetComponent<Item>().Armor = DropItem.Armor;
				Item1.GetComponent<Item>().Damage = DropItem.Damage;
				Item1.GetComponent<Item>().Source = DropItem.Source;
				Item1.GetComponent<Item>().Steadiness = DropItem.Steadiness;
				Item1.GetComponent<Item>().Revitalization = DropItem.Revitalization;
				Item1.GetComponent<Item>().DeathRes = DropItem.DeathRes;
				Item1.GetComponent<Item>().Acceleration = DropItem.Acceleration;				
				
				Item1.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-90, 90), 90, UnityEngine.Random.Range(-90, 90)) * DropSpeed);
			
			}
			
			if(DropItem.Type == "Potion"){
				DropItem.Enchantment = "Regeneration Rate: " + DropItem.Revitalization + "%";
			}
			
		}
		
	}
	
	
	
	void SetArrays(){
		
		WitcherItems[0,0] = "Starter's Wand";
		WitcherItems[0,1] = "1";
		
		WitcherItems[1,0] = "Starter's Sphere";
		WitcherItems[1,1] = "1";
		
		WitcherItems[2,0] = "Starter's Greatsword";
		WitcherItems[2,1] = "1";
		
		WitcherItems[3,0] = "Shabbily Panoply";
		WitcherItems[3,1] = "3";
		
		WitcherItems[4,0] = "Shabbily Hat";
		WitcherItems[4,1] = "3";
		
		WitcherItems[5,0] = "Shabbily Boots";
		WitcherItems[5,1] = "3";
		
		WitcherItems[6,0] = "Shabbily Gloves";
		WitcherItems[6,1] = "3";
		
		WitcherItems[7,0] = "Shabbily Pauldrons";
		WitcherItems[7,1] = "3";
		
		WitcherItems[8,0] = "Shabbily Belt";
		WitcherItems[8,1] = "3";
		
		WitcherItems[10,0] = "Magical Book";
		WitcherItems[10,1] = "5";
		
		
		
		WitcherItems[11,0] = "Ancient Wand";
		WitcherItems[11,1] = "7";
		
		WitcherItems[12,0] = "Ancient Sphere";
		WitcherItems[12,1] = "7";
		
		WitcherItems[13,0] = "Ancient Greatsword";
		WitcherItems[13,1] = "7";
		
		WitcherItems[14,0] = "Lithic Panoply";
		WitcherItems[14,1] = "8";
		
		WitcherItems[15,0] = "Lithic Hat";
		WitcherItems[15,1] = "8";
		
		WitcherItems[16,0] = "Lithic Boots";
		WitcherItems[16,1] = "8";
		
		WitcherItems[17,0] = "Lithic Gloves";
		WitcherItems[17,1] = "8";
		
		WitcherItems[18,0] = "Lithic Pauldrons";
		WitcherItems[18,1] = "8";
		
		WitcherItems[19,0] = "Lithic Belt";
		WitcherItems[19,1] = "8";
		
		WitcherItems[21,0] = "Arkan Book";
		WitcherItems[21,1] = "9";	
		
		
		
		//---------------------------------------
		
		
		
		CorsairItems[0,0] = "Deck Hand’s Sword";
		CorsairItems[0,1] = "1";
		
		CorsairItems[1,0] = "Deck Hand’s Pistol";
		CorsairItems[1,1] = "1";
		
		CorsairItems[2,0] = "Deck Hand’s Hand Cannon";
		CorsairItems[2,1] = "1";
		
		CorsairItems[3,0] = "Cabin Boy’s Panoply";
		CorsairItems[3,1] = "3";
		
		CorsairItems[4,0] = "Cabin Boy’s Hat";
		CorsairItems[4,1] = "3";
		
		CorsairItems[5,0] = "Cabin Boy’s Boots";
		CorsairItems[5,1] = "3";
		
		CorsairItems[6,0] = "Cabin Boy’s Gloves";
		CorsairItems[6,1] = "3";
		
		CorsairItems[7,0] = "Cabin Boy’s Pauldrons";
		CorsairItems[7,1] = "3";
		
		CorsairItems[8,0] = "Cabin Boy’s Belt";
		CorsairItems[8,1] = "3";
		
		CorsairItems[9,0] = "Silver Mojo";
		CorsairItems[9,1] = "5";
		
		
		
		CorsairItems[10,0] = "Gunner's Sword";
		CorsairItems[10,1] = "7";
		
		CorsairItems[11,0] = "Gunner's Pistol";
		CorsairItems[11,1] = "7";
		
		CorsairItems[12,0] = "Gunner's Hand Cannon";
		CorsairItems[12,1] = "7";
		
		CorsairItems[13,0] = "Sailor's Panoply";
		CorsairItems[13,1] = "8";
		
		CorsairItems[14,0] = "Sailor's Hat";
		CorsairItems[14,1] = "8";
		
		CorsairItems[15,0] = "Sailor's Boots";
		CorsairItems[15,1] = "8";
		
		CorsairItems[16,0] = "Sailor's Gloves";
		CorsairItems[16,1] = "8";
		
		CorsairItems[17,0] = "Sailor's Pauldrons";
		CorsairItems[17,1] = "8";
		
		CorsairItems[18,0] = "Sailor's Belt";
		CorsairItems[18,1] = "8";
		
		CorsairItems[19,0] = "Coinsen Mojo";
		CorsairItems[19,1] = "9";	
		
		
		
		//----------------------------------------------
		
		
		
		VenatorItems[0,0] = "Begginer's Handblade";
		VenatorItems[0,1] = "1";
		
		VenatorItems[1,0] = "Begginer's Handgun";
		VenatorItems[1,1] = "1";
		
		VenatorItems[2,0] = "Begginer's Bow";
		VenatorItems[2,1] = "1";
		
		VenatorItems[3,0] = "Firebird Panoply";
		VenatorItems[3,1] = "3";
		
		VenatorItems[4,0] = "Firebird Hat";
		VenatorItems[4,1] = "3";
		
		VenatorItems[5,0] = "Firebird Boots";
		VenatorItems[5,1] = "3";
		
		VenatorItems[6,0] = "Firebird Gloves";
		VenatorItems[6,1] = "3";
		
		VenatorItems[7,0] = "Firebird Pauldrons";
		VenatorItems[7,1] = "3";
		
		VenatorItems[8,0] = "Firebird Belt";
		VenatorItems[8,1] = "3";
		
		VenatorItems[9,0] = "Furry Feathers";
		VenatorItems[9,1] = "5";
		
		
		
		VenatorItems[10,0] = "Hunter's Handblade";
		VenatorItems[10,1] = "7";
		
		VenatorItems[11,0] = "Hunter's Handgun";
		VenatorItems[11,1] = "7";
		
		VenatorItems[12,0] = "Hunter's Bow";
		VenatorItems[12,1] = "7";
		
		VenatorItems[13,0] = "Wooden Panoply";
		VenatorItems[13,1] = "8";
		
		VenatorItems[14,0] = "Wooden Hat";
		VenatorItems[14,1] = "8";
		
		VenatorItems[15,0] = "Wooden Boots";
		VenatorItems[15,1] = "8";
		
		VenatorItems[16,0] = "Wooden Gloves";
		VenatorItems[16,1] = "8";
		
		VenatorItems[17,0] = "Wooden Pauldrons";
		VenatorItems[17,1] = "8";
		
		VenatorItems[18,0] = "Wooden Belt";
		VenatorItems[18,1] = "8";
		
		VenatorItems[19,0] = "Tripper's Feathers";
		VenatorItems[19,1] = "9";	
		
		
		
		//--------------------------------------
		
		
		
		IerokiryxItems[0,0] = "Revenger's Thuribulum";
		IerokiryxItems[0,1] = "1";
		
		IerokiryxItems[1,0] = "Revenger's Transire";
		IerokiryxItems[1,1] = "1";
		
		IerokiryxItems[2,0] = "Revenger's Chain";
		IerokiryxItems[2,1] = "1";
		
		IerokiryxItems[3,0] = "Rohbor Panoply";
		IerokiryxItems[3,1] = "3";
		
		IerokiryxItems[4,0] = "Rohbor Hat";
		IerokiryxItems[4,1] = "3";
		
		IerokiryxItems[5,0] = "Rohbor Boots";
		IerokiryxItems[5,1] = "3";
		
		IerokiryxItems[6,0] = "Rohbor Gloves";
		IerokiryxItems[6,1] = "3";
		
		IerokiryxItems[7,0] = "Rohbor Pauldrons";
		IerokiryxItems[7,1] = "3";
		
		IerokiryxItems[8,0] = "Rohbor Belt";
		IerokiryxItems[8,1] = "3";
		
		IerokiryxItems[9,0] = "Blackred Lepidem";
		IerokiryxItems[9,1] = "5";
		
		
		
		IerokiryxItems[10,0] = "Harvester's Thuribulum";
		IerokiryxItems[10,1] = "7";
		
		IerokiryxItems[11,0] = "Harvester's Transire";
		IerokiryxItems[11,1] = "7";
		
		IerokiryxItems[12,0] = "Harvester's Chain";
		IerokiryxItems[12,1] = "7";
		
		IerokiryxItems[13,0] = "Slayer's Panoply";
		IerokiryxItems[13,1] = "8";
		
		IerokiryxItems[14,0] = "Slayer's Hat";
		IerokiryxItems[14,1] = "8";
		
		IerokiryxItems[15,0] = "Slayer's Boots";
		IerokiryxItems[15,1] = "8";
		
		IerokiryxItems[16,0] = "Slayer's Gloves";
		IerokiryxItems[16,1] = "8";
		
		IerokiryxItems[17,0] = "Slayer's Pauldrons";
		IerokiryxItems[17,1] = "8";
		
		IerokiryxItems[18,0] = "Slayer's Belt";
		IerokiryxItems[18,1] = "8";
		
		IerokiryxItems[19,0] = "Blackpriest Lepidem";
		IerokiryxItems[19,1] = "9";	
		
	}
	
}
