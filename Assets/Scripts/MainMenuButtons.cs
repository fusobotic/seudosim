using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {

	public GameObject snap;

	void Update () {
		iTween.MoveUpdate(gameObject, snap.transform.position, 0f);
	}
}
