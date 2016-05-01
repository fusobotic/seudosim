using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScene : MonoBehaviour {

	public float fadeTime,
				 delay;

	private Image im;

	void Start(){
		im = GetComponent<Image>();
		StartCoroutine(Fader());
	}

	void Update(){
		if(Input.anyKeyDown){
			LoadMain();
		}
	}

	void LoadMain(){
		SceneManager.LoadScene("MainMenu");
	}

	IEnumerator Fader(){
		yield return new WaitForSeconds(delay);
		//iTween.FadeTo(gameObject,0f,fadeTime);
		im.CrossFadeAlpha(0,fadeTime,false);
		yield return new WaitForSeconds(fadeTime+delay);
		LoadMain();
	}
}
