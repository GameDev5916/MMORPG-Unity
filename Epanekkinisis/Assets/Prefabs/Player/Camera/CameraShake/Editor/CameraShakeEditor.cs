//
// Copyright 2012 Thinksquirrel Software, LLC. All rights reserved.
//
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(_CameraShake))]
#if !UNITY_3_3 && !UNITY_3_4
[CanEditMultipleObjects]
#endif
public class CameraShakeEditor : Editor
{
	public override void OnInspectorGUI()
	{
		GUILayout.Label("Camera Shake v. 1.2.0");
		DrawDefaultInspector();
	}
}
