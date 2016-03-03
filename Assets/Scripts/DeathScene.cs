using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour {

	void Update(){
		if(Input.anyKeyDown){
			LoadMain();
		}
	}

	void LoadMain(){
		SceneManager.LoadScene("MainMechanic");
	}
}
