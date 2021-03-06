﻿using UnityEngine;
using System.Collections;

public class TriggerPull : MonoBehaviour {

	private CylinderRevolve cylinderRevolve;
	private GameObject cylinder;
	private GameObject hammer;
	private GameManager gm;

	public bool halfPulled;

	void Start(){
		cylinder = GameObject.Find("Cylinder");
		cylinderRevolve = cylinder.GetComponent<CylinderRevolve>();
		hammer = GameObject.Find("Hammer");
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void OnMouseDown(){
		/*if (gm.curState == "shoot2")
			return;
			//simple way of waiting for AI to shoot back*/
		
		if(!cylinderRevolve.hammerBack){
			gm.playerAnim.SetTrigger("HalfPull");
			gm.curState = "shoot1";
			iTween.RotateTo(gameObject, iTween.Hash("x", 45, "easeType", "easeOutExpo", "time", .25));
			iTween.RotateAdd (cylinder, iTween.Hash("z", 60, "time", .1, "easeType", cylinderRevolve.easeType));
			iTween.RotateTo(hammer, iTween.Hash("x", 45, "easeType", "easeOutExpo", "islocal", true, "time", .15));
			//player sound for pull back and revolveNum (or not if the revolve sound happens automatically)
			halfPulled = true;
		} else if (cylinderRevolve.hammerBack) {
			gm.playerAnim.SetTrigger("FullPull");
			StopCoroutine(GameObject.Find("Opponent").GetComponent<Opponent>().DelayShot(true));
			GameObject.Find("Opponent").GetComponent<Opponent>().deciding = false;
			cylinderRevolve.Fire();
		}
	}

	void OnMouseUp(){
		/*if (gm.curState == "shoot2")
			return;*/
		if (halfPulled){
			halfPulled = false;
			//iTween.RotateTo(hammer, iTween.Hash("x", 0, "easeType", "easeInExpo", "time", .01));
			iTween.RotateTo(hammer, iTween.Hash("x", 0, "easeType", "easeOutExpo", "islocal", true, "time", .01));
			cylinderRevolve.Fire();
			gm.playerAnim.SetTrigger("FullPull");
		}

	}
}
