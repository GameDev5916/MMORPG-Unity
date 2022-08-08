using UnityEngine;
using System.Collections;

public class force : MonoBehaviour {
	
	public float radius;
    public float power;
	public Collider[] colliders;
	
	// Use this for initialization
	void Start () {
	
		Vector3 explosionPos = transform.position;
        colliders = Physics.OverlapSphere(explosionPos, radius);
		
        foreach (Collider hit in colliders) {
				
        if (!hit){
			continue;				
		}
            
        if (hit.GetComponent<Rigidbody>())
            hit.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
		
	}
	
	// Update is called once per frame
	void Update () {

		
		
	}
}
