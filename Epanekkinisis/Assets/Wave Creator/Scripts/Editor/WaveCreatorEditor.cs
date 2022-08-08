using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class WaveCreatorEditor : MaterialEditor {
	
	bool [] foldouts = new bool [10];
	
	public override void OnInspectorGUI () {
		
		// Assign Material
		Material [] targetMat = new Material [1];
		
		targetMat [0] = target as Material;
		
		string [] options = {"Directional", "Circular"};
		
		// Get Properties
		
		MaterialProperty deepColorProp = MaterialEditor.GetMaterialProperty (targetMat, "_DeepColor");
		MaterialProperty shallowColorProp = MaterialEditor.GetMaterialProperty (targetMat, "_ShallowColor");
		MaterialProperty shallowTwoDeep = MaterialEditor.GetMaterialProperty (targetMat, "_HeightAdjustment");

		MaterialProperty edgeBlend = MaterialEditor.GetMaterialProperty (targetMat, "_EdgeFade");
		
		MaterialProperty mainTexProp = MaterialEditor.GetMaterialProperty (targetMat, "_MainTex");
		MaterialProperty normalProp = MaterialEditor.GetMaterialProperty (targetMat, "_BumpMap"); 
		
		MaterialProperty specColorProp = MaterialEditor.GetMaterialProperty (targetMat, "_SpecColor");
		MaterialProperty shininessProp = MaterialEditor.GetMaterialProperty (targetMat, "_Shininess");
		
		MaterialProperty cubeProp = MaterialEditor.GetMaterialProperty (targetMat, "_Cube");
		MaterialProperty reflectionPowProp = MaterialEditor.GetMaterialProperty (targetMat, "_ReflectPow");
		
		MaterialProperty foamColorProp = MaterialEditor.GetMaterialProperty (targetMat, "_FoamColor");
		MaterialProperty foamTexProp = MaterialEditor.GetMaterialProperty (targetMat, "_FoamTex");
		MaterialProperty mapProp = MaterialEditor.GetMaterialProperty (targetMat, "_Map");
		MaterialProperty shorelineFoamAdjustmentProp = MaterialEditor.GetMaterialProperty (targetMat, "_ShorelineFoamAdjustment");
		MaterialProperty waveFoamAdjustmentProp = MaterialEditor.GetMaterialProperty (targetMat, "_WaveFoamAdjustment");
		
		MaterialProperty ampProp = MaterialEditor.GetMaterialProperty (targetMat, "_Amp");
		MaterialProperty steepnessProp = MaterialEditor.GetMaterialProperty (targetMat, "_Steepness");
		MaterialProperty freqProp = MaterialEditor.GetMaterialProperty (targetMat, "_Freq");
		MaterialProperty velocityProp = MaterialEditor.GetMaterialProperty (targetMat, "_Velocity");
		
		MaterialProperty dirTypeProp = MaterialEditor.GetMaterialProperty (targetMat, "_DirectionType");
		MaterialProperty dirxProp = MaterialEditor.GetMaterialProperty (targetMat, "_Dirx");
		MaterialProperty dirzProp = MaterialEditor.GetMaterialProperty (targetMat, "_Dirz");
		
		MaterialProperty waveFadeoutProp = MaterialEditor.GetMaterialProperty (targetMat, "_WaveFadeout");
		
		MaterialProperty heightmapProp = MaterialEditor.GetMaterialProperty (targetMat, "_Heightmap");
		
		MaterialProperty terrainMaxHeightProp = MaterialEditor.GetMaterialProperty (targetMat, "_TerrainMaxHeight");
		MaterialProperty terrainMinHeightProp = MaterialEditor.GetMaterialProperty (targetMat, "_TerrainMinHeight");
		
		EditorGUILayout.Space ();
		
		terrainMaxHeightProp.floatValue = WaveCreatorHelperFunctions.GetTerrainMaxHeight ();
		terrainMinHeightProp.floatValue = WaveCreatorHelperFunctions.GetTerrainMinHeight ();
		
		if (Foldout ("Colours", 0)) {
			
			EditorGUILayout.Space ();
			ColorProperty (deepColorProp, "Deep Water Colour");
			ColorProperty (shallowColorProp, "Shallow Water Colour");
			RangeProperty (shallowTwoDeep, "Shallow to Deep Adjustment");
			EditorGUILayout.Space ();
		}

		if (Foldout ("Edge Fade", 1)) {
			
			EditorGUILayout.Space ();
			RangeProperty (edgeBlend, "Edge Fade");
			EditorGUILayout.Space ();
		}
		
		if (Foldout ("Textures", 2)) {
			
			EditorGUILayout.Space ();
			TextureProperty (mainTexProp, "Water Texture");
			TextureProperty (normalProp, "Normal Texture");
			EditorGUILayout.Space ();
		}
		
		if (Foldout ("Lighting", 3)) {
			
			EditorGUILayout.Space ();
			ColorProperty (specColorProp, "Specular Colour");
			FloatProperty (shininessProp, "Shininess");
			EditorGUILayout.Space ();
		}
		
		if (Foldout ("Reflection", 4)) {
			
			EditorGUILayout.Space ();
			EditorGUILayout.HelpBox ("If you change your skybox you will need to regenerate the cubemap to get correct reflection.", MessageType.Warning);
			if (GUILayout.Button("Regenerate Cubemap")) {

				string [] scenePath = EditorApplication.currentScene.Split(char.Parse("/"));
				
				string sceneName = scenePath [scenePath.Length - 1].Split(char.Parse("."))[0];
				
				string assetPath = "Assets/Wave Creator/Scene Assets/" + sceneName + " Water Assets";
				
				AssetDatabase.DeleteAsset(assetPath + "/Skybox Reflection Cubemap.cubemap");

				AssetDatabase.CreateAsset (GenerateCubemap.SkyboxToCubemap(true), assetPath + "/Skybox Reflection Cubemap.cubemap");

				AssetDatabase.Refresh();

				targetMat[0].SetTexture ("_Cube", AssetDatabase.LoadMainAssetAtPath (assetPath + "/Skybox Reflection Cubemap.cubemap") as Texture);
			}
			TextureProperty (cubeProp, "Cubemap");
			RangeProperty (reflectionPowProp, "Reflection Power");
			EditorGUILayout.Space ();
		}
		
		if (Foldout ("Foam", 5)) {
			
			EditorGUILayout.Space ();
			ColorProperty (foamColorProp, "Foam Colour");
			TextureProperty (foamTexProp, "Foam Texture");
			TextureProperty (mapProp, "Foam Map (for aperiodic tiling)");
			RangeProperty (shorelineFoamAdjustmentProp, "Shoreline Foam Amount");
			RangeProperty (waveFoamAdjustmentProp, "Wave Foam Amount");
			EditorGUILayout.Space ();
		}
		
		if (Foldout ("Waves",6)) {
			
			EditorGUILayout.Space ();
			FloatProperty (ampProp, "Height");
			RangeProperty (steepnessProp, "Steepness");
			RangeProperty (freqProp, "Frequency");
			FloatProperty (velocityProp, "Velocity");
			
			EditorGUILayout.Space ();
			
			dirTypeProp.floatValue = EditorGUILayout.Popup ("Wave Movement", Mathf.RoundToInt (dirTypeProp.floatValue), options);
			
			if (dirTypeProp.floatValue == 0) {
				
				FloatProperty (dirxProp, "X Direction: ");
				FloatProperty (dirzProp, "Z Direction: ");
				
				dirxProp.floatValue = Mathf.Clamp01(dirxProp.floatValue);
				dirzProp.floatValue = Mathf.Clamp01(dirzProp.floatValue);
			}
			
			else {
				
				FloatProperty (dirxProp, "Move Towards X: ");
				FloatProperty (dirzProp, "Move Towards Z: ");
			}
			
			EditorGUILayout.Space ();
			
			FloatProperty (waveFadeoutProp, "Wave Fadeout");
			
			EditorGUILayout.Space ();
		}
		
		if (Foldout ("Heightmap", 7)) {
			
			EditorGUILayout.Space ();
			
			EditorGUILayout.HelpBox ("If you change your terrain you will need to regenerate the heightmap to get the correct shoreline foam and shallow and deep effects.", MessageType.Warning);
			
			if (GUILayout.Button("Regenerate Heightmap")) {
				
				string [] scenePath = EditorApplication.currentScene.Split(char.Parse("/"));
				
				string sceneName = scenePath [scenePath.Length - 1].Split(char.Parse("."))[0];
				
				string assetPath = "Assets/Wave Creator/Scene Assets/" + sceneName + " Water Assets";
				
				AssetDatabase.DeleteAsset(assetPath + "/Heightmap.png");
				
				byte [] bytes = WaveCreatorHelperFunctions.CreateHeightmap().EncodeToPNG ();
				
				File.WriteAllBytes(assetPath + "/Heightmap.png", bytes);
				
				AssetDatabase.Refresh();
			}
			
			EditorGUILayout.Space ();
			
			TextureProperty (heightmapProp, "");
			
			EditorGUILayout.Space ();
		}
		
		EditorGUILayout.Space ();
	}
	
	bool Foldout (string label, int i) {
		
		foldouts[i] = EditorGUILayout.Foldout (foldouts[i], label);
		
		return foldouts[i];
	}
}