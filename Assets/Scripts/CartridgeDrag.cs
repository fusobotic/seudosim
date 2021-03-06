using UnityEngine;
using System.Collections;

public class CartridgeDrag : MonoBehaviour {

	public float spinSpeed;
	public float distance = 10f;

	private Rigidbody rb;
	private bool rotating = false;
	private Transform currentSnap;
	private bool willSnap;
	private Vector3 startPos;
	private Vector3 startRot;
	private bool chambered = false;
	private GameManager gm;
	public GameObject sC,
					  dryFire,
					  bang;

	//private GameManager gm; //probably don't need this yet

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		//startPos = transform.position;
		startRot = transform.eulerAngles;
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		//gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void Update () {
		//print(startPos);
	}

	void OnMouseDrag(){
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);

		//transform.position = objPosition;
		iTween.MoveUpdate( gameObject, objPosition, .1f);
		iTween.RotateUpdate (gameObject, new Vector3 (60,20,0), .3f);
		//iTween.RotateUpdate (gameObject, new Vector3 (90,0,0), .3f);
	}

	void OnMouseDown(){
		if (transform.parent.name == "Cartridges") {
			startPos = transform.position;
		}
		if (transform.parent != null){
			transform.parent = null;
		}
	}

	void OnMouseUp(){
		if (willSnap) {
			transform.parent = currentSnap.transform;
			iTween.MoveTo ( gameObject, currentSnap.transform.position, 0);
			iTween.RotateTo ( gameObject, currentSnap.transform.eulerAngles, 0);
			iTween.RotateTo (gameObject, new Vector3 (90, 0, 0), 0);
			Instantiate(sC,transform.position, Quaternion.identity);
		} else if (!willSnap) {
			transform.parent = GameObject.Find("Cartridges").transform;
			iTween.MoveTo ( gameObject, startPos, .5f);
			iTween.RotateTo ( gameObject, startRot, .5f);
		}
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "Snap") {
			currentSnap = other.gameObject.transform;
			willSnap = true;

		}
		if (other.name == "BulletTrigger"){
			chambered = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Snap") {
			willSnap = false;
		}
		if (other.name == "BulletTrigger"){
			chambered = false;
		}
	}

	public IEnumerator Fired(){
		if (chambered){
			GameObject.Find("Opponent").GetComponent<Opponent>().StopAllCoroutines();
			GameObject.Find("Opponent").GetComponent<Opponent>().enabled = false;
			yield return new WaitForSeconds (.01f); //for the hammer to fall
			gm.playerAnim.ResetTrigger("Click");
			gm.playerAnim.ResetTrigger("Idle");
			Instantiate(bang, transform.position, Quaternion.identity);
			gm.playerAnim.SetTrigger("Death");
			gm.StartCoroutine(gm.Lose()); //calls lose function on GameManger
		} else {
			//for when it wasn't fired
			yield return new WaitForSeconds(.05f);
			Instantiate(dryFire, transform.position, Quaternion.identity);
		}
	}
}
