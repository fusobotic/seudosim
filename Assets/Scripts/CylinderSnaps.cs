using UnityEngine;
using System.Collections;

public class CylinderSnaps : MonoBehaviour {

	//!!!!!make sure to check the catridge drag height in order to prevent bullets from being too close to cylinder

	public float overlapHeight;
	private CapsuleCollider coll;
	private float initHeight;


	void Start(){
		coll = GetComponent<CapsuleCollider> ();
		initHeight = coll.height;
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "Catridge" && !Input.GetMouseButton(0)) {
			coll.height = overlapHeight;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Catridge" && !Input.GetMouseButton(1)) {
			coll.height = initHeight;
		}
	}
}
