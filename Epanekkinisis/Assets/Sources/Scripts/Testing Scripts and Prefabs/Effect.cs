using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {
	
	public GameObject Prefab;	

	public float MinSize;
	public float MaxSize;
	public float MinEnergy;
	public float MaxEnergy;
	public float MinEmission;
	public float MaxEmission;
	
	public Vector3 WorldVelocity;
	public Vector3 LocalVelocity;
	
	public bool Rotation;
	
	public Color Color1;
	public Color Color2;
	public Color Color3;
	public Color Color4;
	public Color Color5;
			
	public bool Autodestruct;
	
	public Material[] Materials;
	
	//---------------------------
	
	public bool CreateHW;
	public bool CreateSW;
	public bool CreateChunks;
	public bool CreateMulti;
		
	public int HowManyMulti;
	public int MultiDuration;
	
	public Vector3 Direction;
	public int DirectedForce;
	
	public int ExplosionForce;
	
	public GameObject Chunk;
	public int HowManyChunks;
	public int ChunksDuration;
	
	public Material HeatWaveMaterial;
	public float HDuration;
	
	public Material ShockWaveMaterial;
	public float SDuration;
	
	
	// Use this for initialization
	void Start () {
		
			
			if(CreateMulti){
			
			for(int i = 0; i <= HowManyMulti; i++){
				
			GameObject FX = Instantiate(Prefab, new Vector3(Random.Range(transform.position.x-5, transform.position.x+5), Random.Range(transform.position.y-5, transform.position.y+5), Random.Range(transform.position.z-5, transform.position.z+5)), transform.rotation) as GameObject;
			
			FX.GetComponent<ParticleEmitter>().minSize = MinSize;
			FX.GetComponent<ParticleEmitter>().maxSize = MaxSize;
			
			FX.GetComponent<ParticleEmitter>().minEnergy = MinEnergy;
			FX.GetComponent<ParticleEmitter>().maxEnergy = MaxEnergy;
				
			FX.GetComponent<ParticleEmitter>().minEmission = MinEmission;
			FX.GetComponent<ParticleEmitter>().maxEmission = MaxEmission;
				
			FX.GetComponent<ParticleEmitter>().worldVelocity = WorldVelocity;
			FX.GetComponent<ParticleEmitter>().localVelocity = LocalVelocity;
				
			FX.GetComponent<ParticleEmitter>().rndRotation = Rotation;				
				
			FX.GetComponent<ParticleAnimator>().colorAnimation[0] = Color1;
			FX.GetComponent<ParticleAnimator>().colorAnimation[1] = Color2;
			FX.GetComponent<ParticleAnimator>().colorAnimation[2] = Color3;
			FX.GetComponent<ParticleAnimator>().colorAnimation[3] = Color4;
			FX.GetComponent<ParticleAnimator>().colorAnimation[4] = Color5;
			
			FX.GetComponent<ParticleAnimator>().autodestruct = Autodestruct;
				
			for(int x = 0; i <= Materials.Length-1; x++){
			
				FX.GetComponent<ParticleRenderer>().materials[x] = Materials[x];
					
			}
				
			}
				
			}
			
		
	}
	
	
	// Update is called once per frame
	void Update () {
	
		
		
	}
	
	
}
