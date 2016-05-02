using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour {

	public GameObject hammerOpen;
	public GameObject hammerClosed;

	void OnMouseDown(){
		if (enabled && transform.localEulerAngles.x < 40){
			iTween.RotateTo(gameObject, iTween.Hash("x", 45, "easeType", "easeOutExpo", "islocal", true, "time", .15));
			Instantiate(hammerOpen, transform.position, Quaternion.identity);
		} else if (enabled){
			iTween.RotateTo(gameObject, iTween.Hash("x", 0, "easeType", "easeInExpo", "islocal", true, "time", .25));
			Instantiate(hammerClosed, transform.position, Quaternion.identity);
		}
	}
}
