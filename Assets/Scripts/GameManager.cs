using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int currentBet; //set this value at the beginning of a round
	public int currentCoins;
	public int roundLosses; //just a little easter egg counter

	public string curState;
	public Transform[] camPositions; //easier method than flipping out inividual objects

	public int turnIndex = 0; //0 is either, 1 is player, 2 is opponent

	public static GameManager Instance;

	void Awake(){
		if (Instance)
			DestroyImmediate (gameObject);
		else {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		}
		//get playerprefs coin amount here
		//maybe also assign variables for pickups, stats etc
	}

	void Start () {
		curState = "bet";
		//maybe get other scripts that need to be modified here
	}

	void Update () {
		switch (curState) {
		case "menu":
			//might do away with this case if menu is separate scene
			break;
		case "bet":
			//load up the round with appropriate variable sets according to rules
			break;
		case "load":
			//could just switch to different view or just load a different scene
			break;
		case "shoot0":
			//state for either player shooting
			break;
		case "shoot1":
			//player has to shoot
			break;
		case "shoot2":
			//oppenent has to shoot, restrict player controls
			break;
		case "won":
			//display congrats screen with spawned coin effect or somethin
			break;
		//default: //use this if you need a state for when the game first starts
		}
				
	}

	void lose() {
		currentCoins -= currentBet;
		roundLosses++;
		//turn screen black and then load menu scene
	}

	void win(){
		curState = "won";
		currentCoins += (currentBet * 2);
		currentBet = 0;
	}

	void OnApplicationQuit(){
		//store current values in PlayerPrefs
	}
}
