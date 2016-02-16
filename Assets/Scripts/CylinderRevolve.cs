using UnityEngine;
using System.Collections;

public class CylinderRevolve : MonoBehaviour {

	private bool rotating; 
	void OnMouseDown(){
		
		iTween.RotateAdd (gameObject, iTween.Hash("y", revolveNum(), "time", 5.5f, "easeType", "easeOutExpo"));
	}

	void Update(){
		if (rotating) {
			//trigger sound effects when passing rotations, or just base it on the snap triggers
		}
	}

	int revolveNum(){
		return Random.Range (10, 25) * 60;
	}
}
