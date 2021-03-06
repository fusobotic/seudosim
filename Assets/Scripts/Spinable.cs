﻿using UnityEngine;
using System.Collections;

public class Spinable : MonoBehaviour {

	//script for snappable spinning, specifically for the revolver cylinder loading
	//also useful for the main menu and shop?

	public bool snapping = false;

	private float closest;
	private float baseAngle = 0.0f;
	private bool dragging = false;
	private float curRot;

	void Start(){

	}

	void OnMouseDown(){
		if (!enabled)
			return;
		Vector3 dir = Camera.main.WorldToScreenPoint (transform.position);
		dir = Input.mousePosition - dir;
		baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
	}

	void OnMouseDrag(){
		if (!enabled)
			return;
		dragging = true;	
		Vector3 dir = Camera.main.WorldToScreenPoint (transform.position);
		dir = Input.mousePosition - dir;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - baseAngle;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void OnMouseUp(){
		if (!enabled)
			return;
		dragging = false;
		curRot = transform.rotation.eulerAngles.z;
		if (curRot <= 30) {
			closest = 0;
		} else if (curRot <= 90) {
			closest = 60;
		} else if (curRot <= 150) {
			closest = 120;
		} else if (curRot <= 210) {
			closest = 180;
		} else if (curRot <= 270) {
			closest = 240;
		} else if (curRot <= 330) {
			closest = 300;
		} else if (curRot <= 360) {
			closest = 360;
		}
		if (snapping) iTween.RotateTo (gameObject, iTween.Hash ("z", closest, "easeType", "easeOutBounce"));
	}

	void Update(){
		if (!enabled)
			return;
	}
}
