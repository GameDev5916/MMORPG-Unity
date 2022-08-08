using UnityEngine;
using System.Collections;

public class PlaneRotation : MonoBehaviour {
    	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0f, 1f, 0f) * Time.deltaTime);
	}
}
