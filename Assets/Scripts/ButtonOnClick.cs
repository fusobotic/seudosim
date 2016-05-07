using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonOnClick : MonoBehaviour {
	//script for calling Game Manager since
	//OnClick doesn't research after start

	// Update is called once per frame

	public Drink drinkScript;

	public void Clicked () {
		GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		if(gameObject.name == "BetButton"){
			gm.StartCoroutine(gm.Bet());
		} else if (gameObject.name == "Confirm"){
			gm.Load();
		} else if (gameObject.name == "PlayAgain"){
			SceneManager.LoadScene("MainMechanicAnim");
		} else if (gameObject.name == "DrinkButton"){
			drinkScript.SipButton();
		} else if (gameObject.name == "MainMenu"){
			SceneManager.LoadScene("MainMenu");
		}
	}
}
