using UnityEngine;
using System.Collections;

public class Treeboss : MonoBehaviour {
	
	public int Life;
	public int Damage;
	public int Aim;
	public string classi;
	
	public GameObject Skala1;
	public GameObject Skala2;
	int skala;
	public GameObject fire;
	public bool Active;
	public GameObject skala1;
	public GameObject skala2;
	public GameObject gate;
	
	public GameObject fr1;
	public GameObject fr2;
	public GameObject fr3;
	
	bool done;
	
	void Update () {
	
		if(Active){
		skala1.SendMessage("Starte");
		skala2.SendMessage("Starte");
		gameObject.SendMessage("Attack");
		Active = false;
		}
		
		if(Life <= 0 && !done){
			
		Instantiate(fire, new Vector3(transform.position.x, transform.position.y + 37, transform.position.z - 10), transform.rotation);
			
		fr1.SetActive(true);
		fr2.SetActive(true);
		fr3.SetActive(true);
			
		skala1.AddComponent<Rigidbody>();
		skala2.AddComponent<Rigidbody>();
				
		for(int i = 0; i <= gate.transform.childCount - 1; i++){
			
			gate.transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
					
		}
		
		done = true;
			
	}
		
	}
	
	
	IEnumerator Attack(){

		yield return new WaitForSeconds(4);
		
		skala = Random.Range(1, 3);
		
		if(skala == 1){
	
			Skala1.GetComponent<Throw>().Throwe(Damage, Aim);
			
		}
		else if(skala == 2){

			Skala2.GetComponent<Throw>().Throwe(Damage, Aim);
			
		}
		else if(skala == 3){

			Skala1.GetComponent<Throw>().Throwe(Damage, Aim);
		
			Skala2.GetComponent<Throw>().Throwe(Damage, Aim);
			
		}
		
		gameObject.SendMessage("Attack");
		
	}
	
	
	void OnTriggerEnter(Collider col){
	
		if(col.tag == "Shot"){
			
		classi = col.GetComponent<Shot>().classi;			

		Life -= col.GetComponent<Shot>().Damage;
			
		}
		
	}
	
}
