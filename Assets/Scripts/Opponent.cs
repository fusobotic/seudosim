﻿using UnityEngine;
using System.Collections;

public class Opponent : MonoBehaviour {

	//most of this will be handled via scripting and not through physical
	//interaction but there will be animations resultant

	public int cartridge;
	//add more functionality for multiple cartridges later

	public float maxWaitTime = 3.5f;

	public int cylinderIndex = 1;

	public bool hammerBack;
	public bool deciding = false;
	public bool firstShot = true;

	public GameManager gm;

	public GameObject blood;
	public GameObject click;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		FillCatriges();
	}

	// Update is called once per frame
	void Update () {
		if(gm.curState == "shoot0" && firstShot){ //decide if he wants to shoot first
			deciding = true;
			firstShot = false;
			StartCoroutine(DelayShot(false));
		}
		if (gm.curState == "shoot2" && !deciding){ //he needs to shoot
			deciding = true;
			StartCoroutine(DelayShot(true));
		}
	}

	void FillCatriges(){
		cartridge = Random.Range(1,7);
		print(cartridge);

		if (Random.value >= .5){
			hammerBack = true;
		} else {
			hammerBack = false;
		}
		//might end up that hammer back is only aeshetic since everything is random anyway
	}

	void PullTrigger(){
		print(cylinderIndex + " " + cartridge);
		if(cartridge == cylinderIndex){
			//Instantiate(blood, transform.position, Quaternion.identity);
			gm.curState = "shoot1"; //do this so that the camera pans before winning
			gm.Win();
		}
		else{
			gm.curState = "shoot1";
			//Instantiate(click, transform.position, Quaternion.identity);
			cylinderIndex++;
		}
		deciding = false;
	}

	IEnumerator DelayShot(bool notFirst){
		if (Random.value >= .5 && !notFirst){ //half chance of just waiting forever
			yield return new WaitForSeconds(Random.Range(1f,maxWaitTime));
			if (gm.curState == "shoot0"){
				PullTrigger();
			}
		}
		else if (notFirst){
			yield return new WaitForSeconds(Random.Range(1f,maxWaitTime));
			PullTrigger();
		}
	}
}
