using UnityEngine;
using System.Collections;

public class SoundClip : MonoBehaviour {

	public AudioClip[] clips;
	public AudioSource aS;
	public float pitchLow;
	public float pitchHigh;

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}

	void Start () {
		aS = GetComponent<AudioSource>();
		if (clips.Length > 1){
			aS.clip = clips[Random.Range(0,clips.Length+1)];
		}
		if(pitchLow != 0 || pitchHigh != 0){
			aS.pitch = Random.Range(pitchLow, pitchHigh);
		}
		aS.Play();
		Destroy(gameObject, aS.clip.length);
	}
}
