using UnityEngine;
using System.Collections;

public class tex_letter : MonoBehaviour {

public Texture[] tex;

private int namo;
private	Vector3 pos;
	private float rand;
	private float t=0f;
	// Use this for initialization
	void Start () {
		this.GetComponent<ParticleSystem>().startDelay=Random.Range(0f,2f);
		this.transform.position= new Vector3(this.transform.position.x,this.transform.position.y+Random.Range(-1.5f,1.5f),this.transform.position.z);
		namo =  Random.Range(0,26);
	this.GetComponent<Renderer>().material.mainTexture = tex[namo];
		pos=this.transform.position;
		rand=Random.Range(.5f,2f);
	}
	
	// Update is called once per frame
	void Update () {
		t+=Time.deltaTime;
	this.transform.position= new Vector3(pos.x,pos.y+Mathf.Sin(t*rand)*3f,pos.z);
	}
}
