using UnityEngine;
using System.Collections;

public class textmeshcolor : MonoBehaviour {
	
    public Color color;
	
    void Start() 
    {
        transform.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }
	
}