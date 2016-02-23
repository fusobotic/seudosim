using UnityEngine;
using System.Collections;

public class Betting : MonoBehaviour {

	public bool allIn;

	public int coinCount;

	private GameManager gm;

	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	void Update () {
		//print (coinCount);

		if (allIn) {
			iTween.MoveUpdate (gameObject, iTween.Hash ("x", 0, "y", 0, "easeType", "easeInOutExpo", "time", 1f));
		} else {
			iTween.MoveUpdate (gameObject, iTween.Hash ("x", 6.46f, "y", 6.12f, "easeType", "easeInOutExpo", "time", 1f));
		}

		//change counter in top left corner of UI accordin to coinCount
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Coin" && enabled) {
			coinCount++;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Coin" && enabled) {
			coinCount--;
		}
	}
}
