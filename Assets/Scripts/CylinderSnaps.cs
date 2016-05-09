using UnityEngine;
using System.Collections;

public class CylinderSnaps : MonoBehaviour {

	//!!!!!make sure to check the catridge drag height in order to prevent bullets from being too close to cylinder

	public float overlapHeight;
	private CapsuleCollider coll;
	private float initHeight;
	public bool occupied,
				snapable = true;

	private Vector3 oldEulerAngles;

	void Start(){
		coll = GetComponent<CapsuleCollider> ();
		initHeight = coll.height;
		oldEulerAngles = GameObject.Find("Cylinder").transform.rotation.eulerAngles;
	}

	void Update(){
		if(oldEulerAngles == GameObject.Find("Cylinder").transform.rotation.eulerAngles){
			coll.enabled = true;
		} else {
			oldEulerAngles = GameObject.Find("Cylinder").transform.rotation.eulerAngles;
			coll.enabled = false;
		}

		if(Input.GetMouseButtonUp(0) && !occupied){
			coll.height = initHeight;
		} else if (Input.GetMouseButtonUp(0) && occupied){
			coll.height = overlapHeight;
		}
		if (Input.GetMouseButtonDown(0) && !occupied){
			coll.height = initHeight;
		}
		if(occupied){
			coll.height = overlapHeight;
		}

	}

	void OnTriggerStay(Collider other){
		if ((other.tag == "Cartridge" && !Input.GetMouseButton(0)) || (other.tag == "Cartridge" && Input.GetMouseButtonUp(0))) {
			occupied = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Cartridge") {
			occupied = false;
		}
	}

}
