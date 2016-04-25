using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour {

	void OnMouseDown(){
		print(transform.eulerAngles.x);
		if (enabled && transform.localEulerAngles.x == 0){
			iTween.RotateTo(gameObject, iTween.Hash("x", 45, "easeType", "easeOutExpo", "islocal", true, "time", .25));
		} else if (enabled){
			iTween.RotateTo(gameObject, iTween.Hash("x", 0, "easeType", "easeInExpo", "islocal", true, "time", .15));
		}
	}
}
