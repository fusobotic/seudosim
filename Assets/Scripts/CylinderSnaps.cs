using UnityEngine;
using System.Collections;

public class CylinderSnaps : MonoBehaviour {

	//!!!!!make sure to check the catridge drag height in order to prevent bullets from being too close to cylinder

	public float overlapHeight;
	private CapsuleCollider coll;
	private float initHeight;
	public bool occupied;


	void Start(){
		coll = GetComponent<CapsuleCollider> ();
		initHeight = coll.height;
	}

	void Update(){
		if(Input.GetMouseButtonUp(0) && !occupied){
			coll.height = initHeight;
		} else if (Input.GetMouseButtonUp(0) && occupied){
			coll.height = overlapHeight;
		}
		if (Input.GetMouseButtonDown(0) && !occupied){
			coll.height = initHeight;
		}
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "Cartridge" && !Input.GetMouseButton(0)) {
			occupied = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Cartridge" && !Input.GetMouseButton(1)) {
			occupied = false;
		}
	}

}
