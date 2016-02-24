using UnityEngine;
using System.Collections;

public class TriggerPull : MonoBehaviour {

	private CylinderRevolve cylinderScript;
	private GameObject cylinder;
	private GameObject hammer;

	public bool halfPulled;

	void Start(){
		cylinder = GameObject.Find("Cylinder");
		cylinderScript = cylinder.GetComponent<CylinderRevolve>();
		hammer = GameObject.Find("Hammer");
	}

	void OnMouseDown(){
		if(!cylinderScript.hammerBack){
			iTween.RotateTo(hammer, iTween.Hash("x", -90, "easeType", "easeOutExpo", "time", .05));
			iTween.RotateAdd (cylinder, iTween.Hash("z", 60, "time", .1, "easeType", cylinderScript.easeType));
			//player sound for pull back and revolveNum (or not if the revolve sound happens automatically)
			halfPulled = true;
			print("double action trigger half pulled");
		} else {
			cylinderScript.Fire();
		}
	}

	void OnMouseUp(){
		if (halfPulled){
			halfPulled = false;
			iTween.RotateTo(hammer, iTween.Hash("x", 0, "easeType", "easeInExpo", "time", .01));
			cylinderScript.Fire();
		}
	}
}
