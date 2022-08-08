using UnityEngine;
using System.Collections;

public class Thought : MonoBehaviour {

	public string Text;
	public int Secs;

	void OnTriggerEnter(){
		GameObject.FindWithTag("Target").GetComponent<Target>().Think(Text, Secs);
	}

}
