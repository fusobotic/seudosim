using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicLerp : MonoBehaviour {

	public AudioSource combatMusic;
	public AudioSource shopMusic;

	public bool isShop = false; //use update to check what scene we're in instead
	//or just use the game manager variables

	public float lerpRate;

	void Start(){
		//figure out which scene you're in
		//and immediately set volume of each.
		if(SceneManager.GetActiveScene().name != "Shop"){
			isShop = false;
			shopMusic.volume = 0;
			combatMusic.volume = 1;
		} else {
			isShop = true;
			combatMusic.volume = 0;
			shopMusic.volume = 1;
		}
	}

	void Update(){
		//lerp the volumes according to isShop to crossfade music
		//might need to make the music loopable
		if(SceneManager.GetActiveScene().name != "Shop"){
			if(combatMusic.volume != 1){
				combatMusic.volume += lerpRate * Time.deltaTime;
			}
			if(shopMusic.volume != 0){
				shopMusic.volume -= lerpRate * Time.deltaTime;
			}
		} else {
			if(combatMusic.volume != 0){
				combatMusic.volume -= lerpRate * Time.deltaTime;
			}
			if(shopMusic.volume != 1){
				shopMusic.volume += lerpRate * Time.deltaTime;
			}
		}
	}
}
