using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {
	
	public float AnimationSpeed;
	public bool IsPlaying;
	public Animation animationComp;


	// Use this for initialization
	void Start () {

		animationComp = GetComponentInChildren<Animation>();
		animationComp["run"].speed = AnimationSpeed;
		GameObject.Find("PlayersManager").GetComponent<PlayersManager>().Monsters.Add(gameObject);

	}	
	
	
	public IEnumerator CustomAnimation(string Clip){

		IsPlaying = true;
		animationComp.CrossFade(Clip);
		
		yield return new WaitForSeconds(animationComp.GetClip(Clip).length);

		IsPlaying = false;

		if(Clip == "die"){
			Destroy(gameObject);	
		}
		
	}
	
	
}
