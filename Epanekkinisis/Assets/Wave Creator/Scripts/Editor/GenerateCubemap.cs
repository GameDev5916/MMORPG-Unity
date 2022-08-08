using UnityEngine;
using UnityEditor;
using System.Collections;

public static class GenerateCubemap {

	public static Cubemap SkyboxToCubemap (bool showError) {
		
		// Create Cubemap
		Cubemap cube = new Cubemap (512, TextureFormat.ARGB32, true);
		
		if (RenderSettings.skybox != null) {
			
			// Make sure all the textures are readable
			// Get paths
			string path1 = AssetDatabase.GetAssetPath (RenderSettings.skybox.GetTexture ("_FrontTex"));
			string path2 = AssetDatabase.GetAssetPath (RenderSettings.skybox.GetTexture ("_BackTex"));
			string path3 = AssetDatabase.GetAssetPath (RenderSettings.skybox.GetTexture ("_LeftTex"));
			string path4 = AssetDatabase.GetAssetPath (RenderSettings.skybox.GetTexture ("_RightTex"));
			string path5 = AssetDatabase.GetAssetPath (RenderSettings.skybox.GetTexture ("_UpTex"));
			string path6 = AssetDatabase.GetAssetPath (RenderSettings.skybox.GetTexture ("_DownTex"));
			
			// Get import settings
			TextureImporter importSettings1 = AssetImporter.GetAtPath (path1) as TextureImporter;
			TextureImporter importSettings2 = AssetImporter.GetAtPath (path2) as TextureImporter;
			TextureImporter importSettings3 = AssetImporter.GetAtPath (path3) as TextureImporter;
			TextureImporter importSettings4 = AssetImporter.GetAtPath (path4) as TextureImporter;
			TextureImporter importSettings5 = AssetImporter.GetAtPath (path5) as TextureImporter;
			TextureImporter importSettings6 = AssetImporter.GetAtPath (path6) as TextureImporter;
			
			// Change to readable
			importSettings1.textureType = TextureImporterType.Advanced;
			importSettings2.textureType = TextureImporterType.Advanced;
			importSettings3.textureType = TextureImporterType.Advanced;
			importSettings4.textureType = TextureImporterType.Advanced;
			importSettings5.textureType = TextureImporterType.Advanced;
			importSettings6.textureType = TextureImporterType.Advanced;
			importSettings1.isReadable = true;
			importSettings2.isReadable = true;
			importSettings3.isReadable = true;
			importSettings4.isReadable = true;
			importSettings5.isReadable = true;
			importSettings6.isReadable = true;
			
			// Reimport
			AssetDatabase.ImportAsset(path1);
			AssetDatabase.ImportAsset(path2);
			AssetDatabase.ImportAsset(path3);
			AssetDatabase.ImportAsset(path4);
			AssetDatabase.ImportAsset(path5);
			AssetDatabase.ImportAsset(path6);
			
			// Array contains skybox textures
			Texture2D [] skyboxTexs = new Texture2D[6];
			
			// Get Skybox textures
			skyboxTexs [0] = RenderSettings.skybox.GetTexture ("_FrontTex") as Texture2D;
			skyboxTexs [1] = RenderSettings.skybox.GetTexture ("_BackTex") as Texture2D;
			skyboxTexs [2] = RenderSettings.skybox.GetTexture ("_LeftTex") as Texture2D;
			skyboxTexs [3] = RenderSettings.skybox.GetTexture ("_RightTex") as Texture2D;
			skyboxTexs [4] = RenderSettings.skybox.GetTexture ("_UpTex") as Texture2D;
			skyboxTexs [5] = RenderSettings.skybox.GetTexture ("_DownTex") as Texture2D;
			
			// Resize textures
			skyboxTexs [0] = RescaleTex (skyboxTexs [0], 512);
			skyboxTexs [1] = RescaleTex (skyboxTexs [1], 512);
			skyboxTexs [2] = RescaleTex (skyboxTexs [2], 512);
			skyboxTexs [3] = RescaleTex (skyboxTexs [3], 512);
			skyboxTexs [4] = RescaleTex (skyboxTexs [4], 512);
			skyboxTexs [5] = RescaleTex (skyboxTexs [5], 512);
			
			// Set colour arrays
			Color [] frontTexCols = skyboxTexs [0].GetPixels ();
			Color [] backTexCols = skyboxTexs [1].GetPixels ();
			Color [] leftTexCols = skyboxTexs [2].GetPixels ();
			Color [] rightTexCols = skyboxTexs [3].GetPixels ();
			Color [] upTexCols = skyboxTexs [4].GetPixels ();
			Color [] downTexCols = skyboxTexs [5].GetPixels ();
			
			// Reverse arrays
			System.Array.Reverse (frontTexCols);
			System.Array.Reverse (backTexCols);
			System.Array.Reverse (leftTexCols);
			System.Array.Reverse (rightTexCols);
			System.Array.Reverse (upTexCols);
			System.Array.Reverse (downTexCols);
			
			// Set cubemap pixels
			cube.SetPixels (frontTexCols, CubemapFace.PositiveZ);
			cube.SetPixels (backTexCols, CubemapFace.NegativeZ);
			cube.SetPixels (leftTexCols, CubemapFace.NegativeX);
			cube.SetPixels (rightTexCols, CubemapFace.PositiveX);
			cube.SetPixels (upTexCols, CubemapFace.PositiveY);
			cube.SetPixels (downTexCols, CubemapFace.NegativeY);
			
			// Apply changes
			cube.Apply ();
			
			cube.name = "Skybox Reflection Cubemap";
			
		} 
		
		else {
			if (showError)
				Debug.LogWarning ("No skybox detected");
		}
		
		return cube;
	}

	public static Texture2D RescaleTex (Texture2D tex, int size) {
		
		float texScale = tex.width / Mathf.Round (size);
		
		Texture2D rescaledImage = new Texture2D (size, size);
		
		for (int x = 0; x < size; x++) {
			for (int y = 0; y < size; y++) {
				
				rescaledImage.SetPixel (x, y, tex.GetPixel (Mathf.RoundToInt (x * texScale), Mathf.RoundToInt (y * texScale)));
			}
		}
		
		rescaledImage.Apply ();
		
		return rescaledImage;
	}
}
