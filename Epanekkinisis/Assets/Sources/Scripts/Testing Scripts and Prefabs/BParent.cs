﻿using UnityEngine;
using System.Collections;

public class BParent : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col){

		transform.parent = col.transform;
		
	}
	
}
