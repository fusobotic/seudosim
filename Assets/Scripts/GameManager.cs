using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	void Awake(){
		if (Instance)
			DestroyImmediate (gameObject);
		else {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		}
	}

	void Start () {
	
	}

	void Update () {
	}
}
