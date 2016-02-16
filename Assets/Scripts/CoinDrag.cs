using UnityEngine;
using System.Collections;

public class CoinDrag : MonoBehaviour {

	public float spinSpeed;
	public float distance = 10f;

	private Rigidbody rb;
	private Quaternion startRot;
	private bool rotating = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		startRot = transform.rotation;
	}

	void Update () {
		if (rb.isKinematic){
			iTween.RotateBy (gameObject, iTween.Hash ("z", spinSpeed, "x", -spinSpeed, "y", spinSpeed * .5, "delay", 0, "easeType", "Linear"));
		}
	}

	void OnMouseDrag(){
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);

		//transform.position = objPosition;
		iTween.MoveUpdate( gameObject, objPosition, .3f);
	}

	void OnMouseDown(){
		Rigidbody rb = GetComponent<Rigidbody> ();
		if (rb.useGravity) rb.isKinematic = true;
	}

	void OnMouseUp(){
		Rigidbody rb = GetComponent<Rigidbody> ();
		if (rb.useGravity) rb.isKinematic = false;
	}
}
