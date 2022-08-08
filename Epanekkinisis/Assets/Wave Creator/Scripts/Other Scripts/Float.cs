using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {
	
	// Custom variables
	public GameObject waterBody;
	public float offset = 0.0f;
	public float randomRotationFactor = 4.0f;
	
	// Update is called once per frame
	void Update () {
		
		transform.position = new Vector3 (transform.position.x, WaveCreatorHelperFunctions.WaveFunction (transform.position.x, transform.position.z, waterBody, true) + offset + waterBody.transform.position.y, transform.position.z);
		
		// Rotate the object in the waterto produce a more realistic effect
		transform.Rotate ((WaveCreatorHelperFunctions.PerlinVector3 (Time.time) - WaveCreatorHelperFunctions.PerlinVector3 (Time.time - Time.deltaTime)) * randomRotationFactor);
	}
}
