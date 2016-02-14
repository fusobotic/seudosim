using UnityEngine;
using System.Collections;

public class Betting : MonoBehaviour {

	public bool allIn;

	private int coinCount;

	void Start () {
	
	}

	void Update () {
		//print (coinCount);

		if (allIn) {
			iTween.MoveUpdate (gameObject, iTween.Hash ("x", 0, "z", 0, "easeType", "easeInOutExpo", "time", 1f));
		} else {
			iTween.MoveUpdate (gameObject, iTween.Hash ("x", 6.46f, "y", 6.12f, "easeType", "easeInOutExpo", "time", 1f));
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Coin") {
			coinCount++;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Coin") {
			coinCount--;
		}
	}

}
