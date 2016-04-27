using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int currentBet; //set this value at the beginning of a round
	public int currentCoins;
	public int roundLosses; //just a little easter egg counter

	public int cartridgeCount;

	public string curState;

	public GameObject[] opponents;

	public static GameManager Instance;

	private GameObject[] revolver;
	private GameObject playerModel;
	private GameObject camPanner;
	private GameObject cartridges;
	private GameObject cylinder;
	private GameObject cylinderModel;
	private GameObject cartConfirm;
	private GameObject playAgain;

	private Vector3 cartridgesLoc;
	private Vector3 cylinderLoc;

	public Animator playerAnim;

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
		RoundStart();
	}

	void RoundStart(){
		print("working");
		cartridgeCount = 0;
		Physics.gravity = new Vector3(0, 0, 9.81f);
		curState = "bet";
		//revolver = GameObject.FindGameObjectsWithTag("PlayerRevolver");
		playerModel = GameObject.Find("PlayerModel");
		camPanner = GameObject.Find("CameraPanner");
		cartridges = GameObject.Find("Cartridges");
		cylinder = GameObject.Find("Cylinder");
		cylinderModel = GameObject.Find("CylinderModel");
		cartConfirm = GameObject.Find("Confirm");
		playAgain = GameObject.Find("PlayAgain");
		playerAnim = GameObject.Find("PlayerModel").GetComponent<Animator>();

		

		/*if(opponents.Length != 0){
			Instantiate(opponents[Random.Range(0,opponents.Length+1)], new Vector3(37.9f, -15.3f, 24.9f), Quaternion.identity);
		}*/
		
		playAgain.SetActive(false);
		cartConfirm.SetActive(false);
		//revolver.SetActive(false);

		cylinderModel.transform.parent = cylinder.transform;

		playerModel.SetActive(false);

		cartridgesLoc = cartridges.transform.position;
		cylinderLoc = cylinder.transform.position;

		cartridges.transform.localPosition = new Vector3(-5.85f, .44f, 0);
		cylinder.transform.localPosition = new Vector3(10.8f, 2.2174f, -2.0914f);
		
	}

	void OnLevelWasLoaded(int level){
		if(level == 1){
			RoundStart();
		}
	}

	void Update () {
		//print(currentCoins);
		switch (curState) {
		case "menu":
			//update checks for main menu
			break;
		case "bet":
			//load up the round with appropriate variable sets according to rules
			break;
		case "load":
			int cCount = 0;
			foreach (GameObject cart in GameObject.FindGameObjectsWithTag("Cartridge")){
				if (cart.transform.parent.tag == "Snap"){
					cCount++;
				}
			}
			cartridgeCount = cCount;
			if (cartridgeCount == 0  && !Application.isEditor){ //disables button toggle for editor
				cartConfirm.GetComponent<Button>().interactable = false;
			} else {
				cartConfirm.GetComponent<Button>().interactable = true;
			}
			if (cartConfirm.activeSelf == false){
					cartConfirm.SetActive(true);
			}
			//iTween.MoveUpdate(cylinder, cylinderLoc, 1f); //not working because of rigidbody?
			cylinder.transform.localPosition = cylinderLoc;
			iTween.MoveUpdate(cartridges, cartridgesLoc, 1f);

			//run any updates that need to be there for loading cartridges
			break;
		case "spin":
			//state for spinning cylinder and pulling hammer
		case "shoot0":
			//state for either player shooting
			break;
		case "shoot1":
			iTween.MoveUpdate(camPanner, new Vector3 (0f,-3.1f,-0.4f), 1.5f);
			//player has to shoot
			break;
		case "shoot2":
			iTween.MoveUpdate(camPanner, new Vector3 (0f,-3.1f,21.2f), 1.5f);
			//oppenent has to shoot, restrict player controls
			break;
		case "won":
			Destroy(GameObject.Find("Trigger"));
			//!!make sure to make the trigger object only a collider, not a mesh
			iTween.MoveUpdate(camPanner, new Vector3 (0f,-3.1f,-0.4f), 1.5f);
			//display congrats screen with spawned coin effect or somethin
			break;
		//default: //use this if you need a state for when the game first starts
		}
		
		//if (Input.GetButtonDown ("Fire1")) Bet ();

	}

	public void Lose() {
		print("lost");
		//opponent.anim.SetTrigger("Won");
		GameObject.Find("Opponent").GetComponent<Opponent>().enabled = false;
		curState = "lose";
		currentCoins -= currentBet;
		roundLosses++;
		//SceneManager.LoadScene("Death");
		//turn screen black and then load menu scene
	}

	public void Win(){
		curState = "won";
		GameObject.Find("TriggerRot").GetComponent<Collider>().enabled = false;
		currentCoins += (currentBet * 2);
		currentBet = 0;
		print(currentCoins);
		playAgain.SetActive(true);
		//print("yeah you won!");
		//spawn winning animation prefab or something
		//Application.LoadLevel(3); //Load the winstate screen/main menu
	}

	public void MenuPlay(){
		SceneManager.LoadScene("MainMechanic");
	}

	public void MenuQuit(){
		Application.Quit();
	}

	void OnApplicationQuit(){
		//store current values in PlayerPrefs
	}

	public IEnumerator Bet(){

		GameObject mat = GameObject.Find ("DragMat");

		if (mat.GetComponent<Betting>().allIn) {
			mat.GetComponent<Betting>().enabled = false;
			mat.GetComponent<BoxCollider>().enabled = false;
			iTween.MoveTo (mat, iTween.Hash ("x", 6.46f, "y", 6.12f, "easeType", "easeInOutExpo", "time", .5));
			currentCoins = currentBet;
		} else {
			currentBet = mat.GetComponent<Betting> ().coinCount;
			mat.GetComponent<Betting>().enabled = false;
			mat.GetComponent<BoxCollider>().enabled = false;
		}
		


		Destroy (GameObject.Find ("StateBet")); //make a more sophsitocated transition later, but for now the testing phase needs to be quick
		Destroy(GameObject.Find("BetButton"));
		Destroy(GameObject.Find("CoinNum"));
		Destroy(GameObject.Find("AllIn"));

		GameObject.Find ("DragMat").GetComponent<Collider>().enabled = false;

		Physics.gravity = new Vector3(0, -18, 0);
		print(Time.timeScale);
		//also move other UI off screen here
		yield return new WaitForSeconds (1.5f); //wait for coins to drop
		
		GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
		for(int i = 0; i < coins.Length; i++){
			Destroy(coins[i]);
		}



		iTween.MoveTo (GameObject.Find("StateLoad"), new Vector3 (0, 0, 0), 1); //Moves the loading step into the scene

		curState = "load";
		//yield return new WaitForSeconds (1.5f);

	}

	public void Load(){
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
		iTween.MoveTo(GameObject.Find ("CameraPanner"), iTween.Hash ("y", 1.5, "time", .15));

		//count how many catridges were put in
		GameObject[] slots;
		slots = GameObject.FindGameObjectsWithTag ("Snap");
		foreach (GameObject slot in slots) {
			cartridgeCount += slot.transform.childCount;
			//maybe feed this into the NPC for correlating shell count?
		}

		GameObject.Find ("Cylinder").GetComponent<CylinderRevolve>().enabled = true;
		//revolver.SetActive(true);
		playerModel.SetActive(true);
		cylinder.transform.parent = GameObject.Find("PlayerModel").transform.GetChild(1).GetChild(1).transform;

		//fade in the Revolver object

		//might need to destroy some other objects with the loading process, like a spawner or something

		curState = "spin";
	}


	/*
	//Section for SHOP FUNCTIONALITY
	//Should have things related to coins n stuff
	*/

	public void BuyCoins(int a){
		currentCoins += a;
		//do some sort of animation on the coin icon maybe?
	}

	
}
