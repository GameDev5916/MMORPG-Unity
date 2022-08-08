using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class WaveCreator : EditorWindow {
	
	// Plane length along the x axis
	int width = 100;
	
	// Plane length along the z axis
	int length = 100;

	int meshResIndex = 0;
	string[] meshResOptions = {"High Res Mesh", "Mid Res Mesh", "Low Res Mesh"};

	int shaderIndex = 0;
	string[] shaderOptions = {"All Features", "No Edge Fade", "No Edge Fade or Specularity"};
	
	GameObject waterBody = null;
	string folderPath = "";
	Material waterMaterial = null;
	
	[MenuItem ("GameObject/Create Other/Water Body")]
	static void Init () {
		
		EditorWindow.GetWindow (typeof (WaveCreator), true, "Water Body Creator", true);
	}
	
	void OnGUI () {
		
		Space ();
		Input ();
		if (Button("Create Water Body"))
		{
			// 1. Creating the object
			CreateWaterBody ();
			PositionWaterBody ();
			
			// 2. Asset and Folder creation
			CreateAssetsFolder ();
			CreateMaterial ();
			CreateCubemap ();
			CreateHeightmap ();
			CreatePrefab ();
		}
	}
	
	void Space () {
		
		EditorGUILayout.Space ();
	}
	
	void Input () {

		EditorGUILayout.BeginHorizontal ("label");

		EditorGUILayout.LabelField ("Make sure your scene is saved before creating a Water Body.");

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ("label");
		
		EditorGUILayout.LabelField ("If you are unsure on these options they are explained in the readme file.");
		
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.Space ();

		EditorGUILayout.BeginHorizontal ("label");
		
		width = EditorGUILayout.IntField ("Water Width: ", width);

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ("label");
		
		length = EditorGUILayout.IntField ("Water Length: ", length);

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ("label");

		EditorGUILayout.LabelField ("Performance Options: ", GUILayout.Width (146));

		meshResIndex = EditorGUILayout.Popup (meshResIndex, meshResOptions);

		shaderIndex = EditorGUILayout.Popup (shaderIndex, shaderOptions);

		EditorGUILayout.EndHorizontal ();
	}
	
	bool Button (string name) {

		EditorGUILayout.BeginHorizontal ("label");
		
		bool buttonOutput = GUILayout.Button (name);

		EditorGUILayout.EndHorizontal ();

		return buttonOutput;
	}
	
	void CreateWaterBody () {
		
		// Create the game object
		waterBody = new GameObject ();
		// Add Mesh Filter
		waterBody.AddComponent <MeshFilter>();
		// Assign Mesh to Mesh Filter
		AssignMesh (meshResIndex);
		// Add Mesh Renderer
		waterBody.AddComponent <MeshRenderer>();
		(waterBody.GetComponent ("MeshRenderer") as MeshRenderer).castShadows = false;
	}

	void AssignMesh (int i) {

		switch (i)
		{
		case 0:
			waterBody.GetComponent<MeshFilter>().sharedMesh = Resources.Load ("Water Mesh High Res", typeof (Mesh)) as Mesh;
			break;
		case 1:
			waterBody.GetComponent<MeshFilter>().sharedMesh = Resources.Load ("Water Mesh Mid Res", typeof (Mesh)) as Mesh;
			break;
		case 2:
			waterBody.GetComponent<MeshFilter>().sharedMesh = Resources.Load ("Water Mesh Low Res", typeof (Mesh)) as Mesh;
			break;
		}
	}

	void PositionWaterBody () {

		// If there is a terrain place 1/5 up it
		if (Terrain.activeTerrain != null) {
			
			waterBody.transform.position = new Vector3 (width / 2, Terrain.activeTerrain.terrainData.size.y / 5, length / 2) + Terrain.activeTerrain.transform.position;	
		}
		
		// If not place at a height of 0
		else {
			
			waterBody.transform.position = new Vector3 (width / 2, 0, length / 2);
		}
		
		// Scale Water Body to length and width
		waterBody.transform.localScale = new Vector3 (width, 1, length);
	}
	
	void CreateAssetsFolder () {
		
		string [] scenePath = EditorApplication.currentScene.Split(char.Parse("/"));
		
		string sceneName = scenePath [scenePath.Length - 1].Split(char.Parse("."))[0];
		
		folderPath = "Assets/Wave Creator/Scene Assets/" + sceneName + " Water Assets";
		
		System.IO.Directory.CreateDirectory (folderPath);
	}
	
	void CreateMaterial () {

		switch (shaderIndex)
		{
		case 0:
			waterMaterial = new Material (Shader.Find ("FX/Waves/Wave Creator +"));
			break;
		case 1:
			waterMaterial = new Material (Shader.Find ("FX/Waves/Wave Creator Mobile +"));
			break;
		case 2:
			waterMaterial = new Material (Shader.Find ("FX/Waves/Wave Creator Mobile"));
			break;
		}
		
		waterMaterial.CopyPropertiesFromMaterial (Resources.Load ("Template Water Material", typeof (Material)) as Material);

		waterBody.GetComponent<MeshRenderer>().material = waterMaterial;
		
		AssetDatabase.CreateAsset(waterMaterial, folderPath + "/Water Material.mat");
	}
	
	void CreateCubemap () {

		Cubemap cubeMap = GenerateCubemap.SkyboxToCubemap (false);
		
		waterMaterial.SetTexture ("_Cube", cubeMap);
		
		AssetDatabase.CreateAsset(cubeMap, folderPath + "/Skybox Reflection Cubemap.cubemap");
	}
	
	void CreateHeightmap () {

		if (Terrain.activeTerrain != null) {
			
			byte [] bytes = WaveCreatorHelperFunctions.CreateHeightmap().EncodeToPNG ();
			
			File.WriteAllBytes(folderPath + "/Heightmap.png", bytes);
			
			AssetDatabase.Refresh();
			
			waterMaterial.SetTexture ("_Heightmap", AssetDatabase.LoadAssetAtPath(folderPath + "/Heightmap.png", typeof (Texture)) as Texture);	
		}
		else {

			waterMaterial.SetTexture ("_Heightmap", Resources.Load ("Clear Heightmap") as Texture);	
		}
	}
	
	void CreatePrefab () {
		
		GameObject waterPrefab = PrefabUtility.CreatePrefab (folderPath + "/Water Body.prefab", waterBody);
		
		PrefabUtility.InstantiatePrefab (waterPrefab);
		
		DestroyImmediate (waterBody);
	}
}