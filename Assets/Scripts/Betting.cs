using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Betting : MonoBehaviour {

	public bool allIn;

	public int coinCount;

	public GameObject coinFab;
	public Transform coinSpawnPoint;

	public float coinDelay;
	public float randSpread;

	private GameManager gm;
	private Button betButton;

	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		coinSpawnPoint = GameObject.Find("CoinSpawnPoint").transform;
		betButton = GameObject.Find("BetButton").GetComponent<Button>();
		StartCoroutine(SpawnCoins());
	}

	void Update () {
		if (allIn) {
			iTween.MoveUpdate (gameObject, iTween.Hash ("x", 0, "y", 0, "easeType", "easeInOutExpo", "time", 1f));
		} else {
			iTween.MoveUpdate (gameObject, iTween.Hash ("x", 6.46f, "y", 6.12f, "easeType", "easeInOutExpo", "time", 1f));
		}
		if(coinCount == 0 && !Application.isEditor){
			betButton.interactable = false;
		} else {
			betButton.interactable = true;
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

	public void AllIn(){
		allIn = !allIn;
	}

	public IEnumerator SpawnCoins(){
		//maybe make this more interesting in the future
		for (int i = 0;i < 50; i++) {
			Vector3 randPos = new Vector3(Random.Range(-randSpread, randSpread),Random.Range(-randSpread, randSpread),i*.3f);
			Instantiate(coinFab, coinSpawnPoint.position + randPos, coinSpawnPoint.rotation);
			//coin.AddTorque(100, 100, 100);

			yield return new WaitForSeconds (coinDelay);
		}

	}
}
