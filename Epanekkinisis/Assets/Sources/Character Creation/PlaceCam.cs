using UnityEngine;
using System.Collections;

public class PlaceCam : MonoBehaviour {
	
	Vector3 velocity = Vector3.zero;
	public bool go;
	public Transform cam;
	public GameObject player;
	

	// Update is called once per frame
	void Update () {

		if(go){
			
			transform.position = Vector3.SmoothDamp(transform.position, cam.position, ref velocity, 1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, cam.rotation, Time.time * 0.01f);

		}
		
	}
	
	void Move () {
		go = true;
		gameObject.SendMessage("MoveBorders");
	}
	
	IEnumerator MoveBorders()
	{
		
		yield return new WaitForSeconds(4.5f);

		GameObject Player = Instantiate(player, transform.position, transform.rotation) as GameObject; 

		Player.GetComponentInChildren<Player>().Name = GameObject.Find("Start").GetComponent<Menu>().Name;
		Player.GetComponentInChildren<Player>().Classi = GameObject.Find("Start").GetComponent<Menu>().Classi;
		Player.GetComponentInChildren<Player>().Sex = GameObject.Find("Start").GetComponent<Menu>().Sex;
		Player.GetComponentInChildren<Player>().PlayerID = Info.PlayerID;

		Destroy(cam.gameObject);
		Destroy(transform.root.gameObject);

	}
	
	
}
