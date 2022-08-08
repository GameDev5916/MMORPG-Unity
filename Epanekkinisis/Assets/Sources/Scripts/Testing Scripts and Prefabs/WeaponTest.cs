using UnityEngine;
using System.Collections;

public class WeaponTest : MonoBehaviour {
	
	public GameObject weap;
	
	// Use this for initialization
	void Start () {
	
		GameObject weapon = Instantiate(weap, transform.position, transform.rotation) as GameObject;
		
		weapon.transform.parent = GameObject.Find("WEAPON").transform;
		
		weapon.transform.localPosition = new Vector3(0, 0, 0);
		
		weapon.transform.localRotation = Quaternion.Euler(270, 0, 0);
		
		weapon.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
