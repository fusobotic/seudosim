using UnityEngine;
using System.Collections;

public class MusicLerp : MonoBehaviour {

	public AudioSource combatMusic;
	public AudioSource shopMusic;

	public bool isShop = false; //use update to check what scene we're in instead
	//or just use the game manager variables

	void Start(){
		//figure out which scene you're in
		//and immediately set volume of each.
	}

	void Update(){
		//lerp the volumes according to isShop to crossfade music
		//might need to make the music loopable
	}
}
