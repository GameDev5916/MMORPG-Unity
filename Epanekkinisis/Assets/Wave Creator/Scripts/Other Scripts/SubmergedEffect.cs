using UnityEngine;
using System.Collections;

public class SubmergedEffect : MonoBehaviour {
	
	public GameObject waterBody;
	public Color underWaterColor = new Color (0.53f,0.66f,0.69f,1.0f);
	public float underWaterVisiblity = 0.3f;
	
	bool aboveWaterFogMode;
	Color aboveWaterColor;
	float aboveWaterVisiblity;
	Material aboveWaterSkybox;
	
	bool checkedIfAboveWater = false;
	float waterHeight = 0.0f;
	
	// Use this for initialization
	void Start () {
	
		// Setup Camera
		Camera.main.nearClipPlane = 0.01f;

		if (waterBody.GetComponent<Renderer>().material.shader != Shader.Find ("Wave Creator / Flat")) {
			
			waterHeight = WaveCreatorHelperFunctions.WaveFunction (transform.position.x, transform.position.z, waterBody, false) + waterBody.transform.position.y;
		}
		
		else {
			
			waterHeight = waterBody.transform.position.y;
		}
		
		AssignAboveWaterSettings ();
		
		if (transform.position.y < waterHeight)
			ApplyUnderWaterSettings ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (waterBody.GetComponent<Renderer>().material.shader != Shader.Find ("Wave Creator / Flat")) {
			
			waterHeight = WaveCreatorHelperFunctions.WaveFunction (transform.position.x, transform.position.z, waterBody, false) + waterBody.transform.position.y;
		}
		
		else {
			
			waterHeight = waterBody.transform.position.y;
		}
		
		if (transform.position.y > waterHeight && checkedIfAboveWater == false) {
			
			checkedIfAboveWater = true;
			ApplyAboveWaterSettings ();
			ToggleFlares (true);
		}
		
		if (transform.position.y < waterHeight && checkedIfAboveWater == true) {
			
			checkedIfAboveWater = false;
			AssignAboveWaterSettings ();
			ApplyUnderWaterSettings ();
			ToggleFlares (false);
		}
	}
	
	void AssignAboveWaterSettings () {
		
		aboveWaterFogMode = RenderSettings.fog;
		aboveWaterColor = RenderSettings.fogColor;
		aboveWaterVisiblity = RenderSettings.fogDensity;
		aboveWaterSkybox = RenderSettings.skybox;
	}
	
	void ApplyAboveWaterSettings () {
		
		RenderSettings.fog = aboveWaterFogMode;
		RenderSettings.fogColor = aboveWaterColor;
		RenderSettings.fogDensity = aboveWaterVisiblity;
		RenderSettings.skybox = aboveWaterSkybox;
		
	}
	
	void ApplyUnderWaterSettings () {
		
		RenderSettings.fog = true;
		RenderSettings.fogColor = underWaterColor;
		RenderSettings.fogDensity = underWaterVisiblity;
		RenderSettings.skybox = new Material (Shader.Find("Diffuse"));
	}
	
	void ToggleFlares (bool state) {
		
		LensFlare[] lensFlares = FindObjectsOfType(typeof(LensFlare)) as LensFlare[];
		
		foreach (LensFlare currentFlare in lensFlares) {
			currentFlare.enabled = state;
		}
	}
}
