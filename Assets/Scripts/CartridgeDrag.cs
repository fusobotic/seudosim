using UnityEngine;
using System.Collections;

public class CartridgeDrag : MonoBehaviour {

	public float spinSpeed;
	public float distance = 10f;

	private Rigidbody rb;
	private bool rotating = false;
	private Transform currentSnap;
	private bool willSnap;
	private Vector3 startPos;
	private Vector3 startRot;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		startPos = transform.position;
		startRot = transform.eulerAngles;
	}

	void Update () {
		//modify this later to be a better animation for bullets (showcasing the tip)
		//iTween.RotateBy (gameObject, iTween.Hash ("z", spinSpeed, "x", -spinSpeed, "y", spinSpeed * .5, "delay", 0, "easeType", "Linear"));
	}

	void OnMouseDrag(){
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);

		//transform.position = objPosition;
		iTween.MoveUpdate( gameObject, objPosition, .1f);
		iTween.RotateUpdate (gameObject, new Vector3 (60,-15,0), .3f);
	}

	void OnMouseDown(){
		if (transform.parent != null)
			transform.parent = null;
	}

	void OnMouseUp(){
		if (willSnap) {
			transform.parent = currentSnap.transform;
			iTween.MoveTo ( gameObject, currentSnap.transform.position, 0);
			iTween.RotateTo ( gameObject, currentSnap.transform.eulerAngles, 0);
			iTween.RotateTo (gameObject, new Vector3 (90, 0, 0), 0);
		} else if (!willSnap) {
			transform.parent = null;
			iTween.MoveTo ( gameObject, startPos, .5f);
			iTween.RotateTo ( gameObject, startRot, .5f);
		}
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "Snap") {
			currentSnap = other.gameObject.transform;
			willSnap = true;

		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Snap") {
			willSnap = false;
		}
	}
}
