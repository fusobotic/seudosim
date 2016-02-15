using UnityEngine;
using System.Collections;

public class MouseDrag : MonoBehaviour {
	public float distance = 10f;

	void OnMouseDrag(){
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);

		//transform.position = objPosition;
		iTween.MoveUpdate( gameObject, objPosition, 0.1f);
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
