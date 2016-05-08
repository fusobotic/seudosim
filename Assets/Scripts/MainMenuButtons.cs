using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

	public GameObject snap;

	public void MenuPlay(){
		SceneManager.LoadScene("MainMechanicAnim");
	}

	public void MenuQuit(){
		Application.Quit();
	}

	public void MenuShop(){
		SceneManager.LoadScene("Shop");
	}

	void Update () {
		iTween.MoveUpdate(gameObject, snap.transform.position, 0f);
	}
}
