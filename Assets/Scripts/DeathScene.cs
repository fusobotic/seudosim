using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScene : MonoBehaviour {

	public float fadeTime,
				 whiteRate,
				 delay;

	private Image secondBlack,
				  firstBlack,
				  wht;

	public bool homeMenu = false;
	

	void Start(){
		if (!homeMenu){
			secondBlack = GetComponent<Image>();
			firstBlack = GameObject.Find("FirstBlack").GetComponent<Image>();
			wht = GameObject.Find("White").GetComponent<Image>();
			StartCoroutine(Fader());
		} else {
			wht = GameObject.Find("White").GetComponent<Image>();
			wht.color = Color.white;
			StartCoroutine(MenuFader());
		}
	}

	void Update(){
		if(Input.anyKeyDown && !homeMenu){
			LoadMain();
		}
	}

	void LoadMain(){
		SceneManager.LoadScene("MainMenu");
	}

	IEnumerator Fader(){
		yield return new WaitForSeconds(0.5f);
		firstBlack.CrossFadeAlpha(0,delay,false);
		yield return new WaitForSeconds(delay*1.2f);
		//iTween.FadeTo(gameObject,0f,fadeTime);
		secondBlack.CrossFadeAlpha(0,fadeTime,false);
		yield return new WaitForSeconds(fadeTime+.5f);
		for (float i=0; wht.color.a < 1; i+= whiteRate){
			yield return new WaitForSeconds(fadeTime/100);
			wht.color = new Color (1,1,1,i);
		}
		yield return new WaitForSeconds(fadeTime);
		LoadMain();
	}

	IEnumerator MenuFader(){
		wht.CrossFadeAlpha(0,fadeTime,false);
		yield return new WaitForSeconds(fadeTime);
		Destroy(GameObject.Find("White"));
	}
}
