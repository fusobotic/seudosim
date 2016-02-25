using UnityEngine;
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
			iTween.RotateTo(hammer, iTween.Hash("x", -90, "easeType", "easeOutExpo", "time", .05));
			iTween.RotateAdd (cylinder, iTween.Hash("z", 60, "time", .1, "easeType", cylinderRevolve.easeType));
			//player sound for pull back and revolveNum (or not if the revolve sound happens automatically)
			halfPulled = true;
		} else {
			cylinderRevolve.Fire();
		}
	}

	void OnMouseUp(){
		/*if (gm.curState == "shoot2")
			return;*/
		if (halfPulled){
			halfPulled = false;
			iTween.RotateTo(hammer, iTween.Hash("x", 0, "easeType", "easeInExpo", "time", .01));
			cylinderRevolve.Fire();
		}
	}
}
