using UnityEngine;
using System.Collections;

public class SimpleRemoteInterpolation : MonoBehaviour {

	// Extremely simple and dumb interpolation script
	private Vector3 desiredPos;
	private Quaternion desiredRot;
	
	private float dampingFactor = 5f;
	
	void Start() {
		desiredPos = this.transform.position;
		desiredRot = this.transform.rotation;
	}
	
	public void SetTransform(Vector3 pos, Quaternion rot, bool interpolate) {
		// If interpolation, then set the desired pos+rot - else force set (for spawning new models)
		if (interpolate) {
			desiredPos = pos;
			desiredRot = rot;
		} else {
			this.transform.position = pos;
			this.transform.rotation = rot;
		}
	}
	
	void Update () {
		// Really dumb interpolation, but works for this example
		this.transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * dampingFactor);
		this.transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, Time.deltaTime * dampingFactor);
	}
}
