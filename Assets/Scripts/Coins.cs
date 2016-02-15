using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour {

	public float spinSpeed;

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
}
