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

	private Collider hammer;

	private bool rotating;
	private bool rotated;

	private GameManager gm;

	void Start(){
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		//hammer = GameObject.Find ("Hammer");
	}

	void OnMouseDown(){
		if (!rotated && enabled)
			StartCoroutine (Spin());
	}

	IEnumerator Spin(){
		rotated = true;
		rotating = true;
		iTween.RotateAdd (gameObject, iTween.Hash("z", revolveNum(), "time", spinTime, "easeType", easeType));
		yield return new WaitForSeconds(spinTime + 1f);
		rotating = false;
		//hammer.GetComponent<Collider>().enabled = false; //disables hammer being toggled anymore
		//gm.curState = "shoot0";
		/*if (hammer.transform.eulerAngles.y >= 69) {
			//put in some angles for off and on, attach a simple click iTween object to the trigger
			hammerBack = true;
		}else{
			hammerBack = false;
		}
		*/
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
}
