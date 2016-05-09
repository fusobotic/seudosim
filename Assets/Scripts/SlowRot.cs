using UnityEngine;
using System.Collections;

public class SlowRot : MonoBehaviour {

	public float rotTime;

	void Update () {
		transform.Rotate(Vector3.forward, rotTime * Time.deltaTime);
	}
}
