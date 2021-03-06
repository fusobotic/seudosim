﻿using UnityEngine;
using System.Collections;

public class CylinderRevolve : MonoBehaviour {

	//script for spinning cylinder before round officially begins
	//also has functionality for detecting hammer back
	//firing, also camera mod for first fire
	//try to put camera movement in the Game Manager at some point

	public float spinTime;
	public string easeType;

	public int randMin;
	public int randMax;

	public bool hammerBack;

	private GameObject hammer;

	private bool rotating;
	private bool rotated;

	private GameManager gm;
	public GameObject clickSound;

	void Awake(){
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		hammer = GameObject.Find ("Hammer");
	}

	void OnMouseDown(){
		if (!rotated && enabled)
			StartCoroutine (Spin());
	}

	IEnumerator Spin(){
		rotated = true;
		rotating = true;
		int num = revolveNum();
		StartCoroutine(SpinClicks(num));
		iTween.RotateAdd (gameObject, iTween.Hash("z", num*60, "time", spinTime, "easeType", "linear")); //formerly easeOutQuart ease
		yield return new WaitForSeconds(spinTime + .15f);
		rotating = false;
		hammer.GetComponent<Collider>().enabled = false; //disables hammer being toggled anymore
		if (hammer.transform.localEulerAngles.x > 2) {
			hammerBack = true;
		}else{
			hammerBack = false;
		}

		GameObject.Find("TriggerRot").GetComponent<Collider>().enabled = true;
		iTween.MoveTo(GameObject.Find ("CameraPanner"), iTween.Hash ("z", 9.8, "y", -3.1, "time", .5f));
		Camera.main.fieldOfView = 46;
		gm.curState = "shoot0";



		//checks if the hammer is back or not

		//maybe also disable collider for the cylinder to prevent any collision/triggers on bullet objects
	}

	IEnumerator SpinClicks(int num){
		for(int i=0;i<num;i++){
			Instantiate(clickSound,transform.position, Quaternion.identity);
			yield return new WaitForSeconds(spinTime/num);
		}
	}

	void Update(){
		if (transform.localEulerAngles.y % 60 <= 1 && rotating) {
			//print(transform.localEulerAngles);
			//Instantiate(clickSound,transform.position, Quaternion.identity);
		}
	}

	int revolveNum(){
		return Random.Range (randMin, randMax);
	}

	public void Fire(){
		gm.curState = "shoot2";
		if(hammerBack){
			hammerBack = false;
			//iTween.RotateAdd (gameObject, iTween.Hash("z", 60, "time", .5, "easeType", easeType));
			iTween.RotateTo(hammer, iTween.Hash("x", 0, "easeType", "easeOutExpo", "islocal", true, "time", .25));

		}
		GameObject[] cartridges = GameObject.FindGameObjectsWithTag("Cartridge");
		foreach (GameObject cartridge in cartridges ){
			CartridgeDrag script = cartridge.GetComponent<CartridgeDrag>();
			gm.playerAnim.SetTrigger("Click");
			gm.playerAnim.SetTrigger("Idle");
			StartCoroutine(script.Fired());
		}


		//maybe make the entire screen a button for this?

		
	}
}
