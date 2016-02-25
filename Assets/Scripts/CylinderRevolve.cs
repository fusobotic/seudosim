using UnityEngine;
using System.Collections;

public class CylinderRevolve : MonoBehaviour {

	//script for spinning cylinder before round officially begins
	//also has functionality for detecting hammer back

	public float spinTime;
	public string easeType;

	public int randMin;
	public int randMax;

	public bool hammerBack;

	private GameObject hammer;

	private bool rotating;
	private bool rotated;

	private GameManager gm;

	void Start(){
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
		iTween.RotateAdd (gameObject, iTween.Hash("z", revolveNum(), "time", spinTime, "easeType", easeType));
		yield return new WaitForSeconds(spinTime + .25f);
		rotating = false;
		hammer.GetComponent<Collider>().enabled = false; //disables hammer being toggled anymore
		if (hammer.transform.eulerAngles.x > 2) {
			hammerBack = true;
		}else{
			hammerBack = false;
		}

		GameObject.Find("Trigger").GetComponent<Collider>().enabled = true;
		gm.curState = "shoot0";

		//checks if the hammer is back or not

		//maybe also disable collider for the cylinder to prevent any collision/triggers on bullet objects
	}

	void Update(){
		if (rotating) {
			//trigger sound effects when passing rotations, or just base it on the snap triggers
		}
	}

	int revolveNum(){
		return Random.Range (randMin, randMax) * 60;
	}

	public void Fire(){
		if(hammerBack){
			hammerBack = false;
			//iTween.RotateAdd (gameObject, iTween.Hash("z", 60, "time", .5, "easeType", easeType));
			iTween.RotateTo(hammer, iTween.Hash("x", 0, "easeType", "easeInExpo", "time", .01));

		}else{

		}
		GameObject[] cartridges = GameObject.FindGameObjectsWithTag("Cartridge");
		foreach (GameObject cartridge in cartridges ){
			CartridgeDrag script = cartridge.GetComponent<CartridgeDrag>();
			StartCoroutine(script.Fired());
		}
	}
}
