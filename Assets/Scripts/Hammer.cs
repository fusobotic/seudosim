using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour {

	void OnMouseDown(){
		if (enabled && transform.eulerAngles.x == 0){
			iTween.RotateTo(gameObject, iTween.Hash("x", -90, "easeType", "easeOutExpo", "time", .25));
		} else if (enabled){
			iTween.RotateTo(gameObject, iTween.Hash("x", 0, "easeType", "easeInExpo", "time", .15));
		}
	}
}
