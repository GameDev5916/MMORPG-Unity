using UnityEngine;
using System.Collections;

public class SphereHover : MonoBehaviour {

    public float Speed = 0.005f;
    public float Offset = 1f;

	// Update is called once per frame
	void Update ()
    {
        transform.position = transform.position + new Vector3(0f, Mathf.Sin(Time.time + Offset) * Speed, 0f);  
	}
}
