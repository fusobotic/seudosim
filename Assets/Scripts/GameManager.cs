using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int currentBet; //set this value at the beginning of a round
	public int currentCoins;
	public int roundLosses; //just a little easter egg counter

	public int cartridgeCount;

	public string curState;
	public Transform[] camPositions; //might end up just using a rotational system for this

	public int turnIndex = 0; //0 is either, 1 is player, 2 is opponent

	public static GameManager Instance;

	private GameObject revolver;
	private GameObject camPanner;

	void Awake(){
		if (Instance)
			DestroyImmediate (gameObject);
		else {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		}
		//get playerprefs coin amount here
		//maybe also assign variables for pickups, stats etc
		if (GameObject.Find ("Revolver") != null) {
			iTween.FadeTo (GameObject.Find ("Revolver"), 0, 0);
			//makes the revolver clear until
		}
	}

	void Start () {
		cartridgeCount = 0;
		curState = "bet"; 		//make sure to change this once the main menu is integrated
		//maybe get other scripts that need to be modified here
		revolver = GameObject.Find("Revolver");
		revolver.SetActive(false);
		camPanner = GameObject.Find("CameraPanner");
	}

	void Update () {
		switch (curState) {
		case "menu":
			//update checks for main menu
			break;
		case "bet":
			//load up the round with appropriate variable sets according to rules
			break;
		case "load":
			//run any updates that need to be there for loading cartridges
			break;
		case "spin":
			//state for spinning cylinder and pulling hammer
		case "shoot0":
			//state for either player shooting
			break;
		case "shoot1":
			iTween.MoveUpdate(camPanner, new Vector3 (0f,-3.1f,-0.4f), 1);
			//player has to shoot
			break;
		case "shoot2":
			iTween.MoveUpdate(camPanner, new Vector3 (0f,-3.1f,21.2f), 1);
			//oppenent has to shoot, restrict player controls
			break;
		case "won":
			Destroy(GameObject.Find("Trigger")); 
			//!!make sure to make the trigger object only a collider, not a mesh
			iTween.MoveUpdate(camPanner, new Vector3 (0f,-3.1f,-0.4f), 1);
			//display congrats screen with spawned coin effect or somethin
			break;
		//default: //use this if you need a state for when the game first starts
		}

		//if (Input.GetButtonDown ("Fire1")) Bet ();

	}

	public void Lose() {
		currentCoins -= currentBet;
		roundLosses++;
		//turn screen black and then load menu scene
	}

	public void Win(){
		curState = "won";
		currentCoins += (currentBet * 2);
		currentBet = 0;
		print("yeah you won!");
		//spawn winning animation prefab or something
		//Application.LoadLevel(3); //Load the winstate screen/main menu
	}

	void OnApplicationQuit(){
		//store current values in PlayerPrefs
	}

	void Load(){
		GameObject.Find ("Cylinder").GetComponent<Spinable> ().enabled = false;

		//destroy catridges
		Destroy (GameObject.Find ("StateLoad"));
		//maybe polish this up later

		//rotate the Camera Rig to face the revolver side view
		//iTween.FadeTo(GameObject.Find("Revolver"), 255, 2f); //fades in revolver
		Destroy(GameObject.Find("Confirm"), .15f);
		iTween.MoveAdd(GameObject.Find("Confirm"), iTween.Hash("x", 200, "time", .15));
		Camera.main.orthographic = false;
		iTween.RotateTo(GameObject.Find ("CameraPanner"), iTween.Hash ("y", 90, "time", .15));

		//count how many catridges were put in
		GameObject[] slots;
		slots = GameObject.FindGameObjectsWithTag ("Snap");
		foreach (GameObject slot in slots) {
			cartridgeCount += slot.transform.childCount;
			//maybe feed this into the NPC for correlating shell count?
		}

		GameObject.Find ("Cylinder").GetComponent<CylinderRevolve>().enabled = true;
		revolver.SetActive(true);

		//fade in the Revolver object

		//might need to destroy some other objects with the loading process, like a spawner or something

		curState = "spin";
	}

	void Bet(){

		GameObject mat = GameObject.Find ("DragMat");

		if (mat.GetComponent<Betting> ().allIn) {
			mat.GetComponent<Betting>().enabled = false;
			mat.GetComponent<BoxCollider> ().enabled = false;
			iTween.MoveTo (mat, iTween.Hash ("x", 6.46f, "y", 6.12f, "easeType", "easeInOutExpo", "time", .5));
			currentCoins = currentBet;
		} else {
			currentBet = mat.GetComponent<Betting> ().coinCount;
			mat.GetComponent<Betting>().enabled = false;
			mat.GetComponent<BoxCollider> ().enabled = false;
		}



		Destroy (GameObject.Find ("StateBet")); //make a more sophsitocated transition later, but for now the testing phase needs to be quick

		iTween.MoveTo (GameObject.Find("StateLoad"), new Vector3 (0, 0, 0), 1); //Moves the loading step into the scene

		curState = "load";
	}
}
