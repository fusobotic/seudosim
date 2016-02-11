using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour {

	public float spinSpeed;

	private Rigidbody rb;
	private Quaternion startRot;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		startRot = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//print (rb.velocity);
		//rb.AddTorque.transform.Rotate(rb.velocity);
		//transform.Rotate(Mathf.Lerp(startRot, transform.rotation, Time.deltaTime));
	}
}
