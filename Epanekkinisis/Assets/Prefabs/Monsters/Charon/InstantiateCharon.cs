using UnityEngine;
using System.Collections;

public class InstantiateCharon : MonoBehaviour {
	
	public GameObject CharonPrefab;
	GameObject Charon;
	
	void OnCollisionEnter(Collision col){
		
		if(col.transform.name.Contains("Terrain")){
			
			Charon = Instantiate(CharonPrefab, transform.position, transform.rotation) as GameObject;
			Charon.SendMessage("CustomAnimation", "Summon");
			gameObject.SendMessage("Summon");
			
		}
		
	}
	
	IEnumerator Summon(){
		
		yield return new WaitForSeconds(5);
		Charon.GetComponent<Charon>().enabled = true;
		
	}
	
}
