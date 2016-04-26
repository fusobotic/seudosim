using UnityEngine;
using System.Collections;

public class Drink : MonoBehaviour {

	public string customFunc;
	public float customFuncParam;

	private GameManager gm;

	public int passOutMin,
			   passOutMax,
			   passOutLimit,
			   passOutIndex = 0;


	
	void Start (){
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		passOutLimit = Random.Range(passOutMin, passOutMax+1);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.E) && (gm.curState == "shoot1" || gm.curState == "shoot0")){
			StartCoroutine(Sip());
		}
		print(passOutIndex + " / " + passOutLimit);
	}

	public IEnumerator Sip(){
		if (customFunc != ""){
			//if there is custom functionality execute it on this script
			if(customFuncParam != null){
				BroadcastMessage(customFunc, customFuncParam);
			} else {
				BroadcastMessage(customFunc); 
				//maybe clear the string and float then call drink again?
			}
		} else {
			gm.playerAnim.SetTrigger("Drink");
			//regular drink function
			
			passOutIndex++;
			//play short animation
			yield return new WaitForSeconds (gm.playerAnim.GetCurrentAnimatorStateInfo(0).length); //for anim to finish, sub anim length
			if(passOutIndex == passOutLimit){
				gm.playerAnim.SetTrigger("PassOut");
				gm.Lose();
				//maybe play drink death animation?
			} else {
				gm.playerAnim.SetTrigger("Idle");
				gm.curState = "shoot2";
			}

			//maybe also reduce the size of the drink here
			//to indicate how much has been drunk
		}
	}

	void OnMouseDown(){
		if(gm.curState == "shoot1" || gm.curState == "shoot0"){
			Sip();
		}
	}
	//other custom functions here
}
