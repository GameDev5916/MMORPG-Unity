using UnityEngine;
using System.Collections;

public class Damages : MonoBehaviour {
	
	public GameObject Player;
	public int Damage;

	public Damages(GameObject Player)
	{
		this.Player = Player;
		this.Damage = 0;
	}

}
