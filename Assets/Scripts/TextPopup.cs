using UnityEngine;
using System.Collections;

public class TextPopup : MonoBehaviour {

	public float tweenTime;
	public string easeType;

	// Use this for initialization
	void Start () {
		iTween.ScaleTo(gameObject, iTween.Hash("x", 1, "y", 1, "z", 1, "easeType", easeType, "time", tweenTime, "oncomplete", "Killme"));
	}

	void Killme(){
		Destroy(gameObject);
	}
}
