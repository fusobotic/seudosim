using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Drink : MonoBehaviour {

	public string customFunc;
	public float customFuncParam;

	private GameManager gm;

	private GameObject drinkButton;

	public int passOutMin,
			   passOutMax,
			   passOutLimit,
			   passOutIndex = 0;


	


	void Awake (){
		if(Items.equippedDrink == null){
			Destroy(GameObject.Find("DrinkButton"));
			return;
		}
		drinkButton = GameObject.Find("DrinkButton");
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		passOutLimit = Random.Range(passOutMin, passOutMax+1);
		drinkButton.SetActive(false);
	}

	void Update(){
		print("Drinks: " + passOutIndex + " / " + passOutLimit);
		if (gm.curState == "shoot1"){
			drinkButton.SetActive(true);
			drinkButton.GetComponent<Button>().interactable = true;
		} else if(gm.curState == "won"){
			drinkButton.SetActive(false);
		} else {
			drinkButton.GetComponent<Button>().interactable = false;
		}
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
			yield return new WaitForSeconds (gm.playerAnim.GetCurrentAnimatorStateInfo(0).length - .1f); //for anim to finish, sub anim length
			if(passOutIndex == passOutLimit){
				gm.playerAnim.SetTrigger("PassOut");
				gm.StartCoroutine(gm.Lose());
				//maybe play drink death animation?
			} else {
				gm.playerAnim.SetTrigger("Idle");
				gm.curState = "shoot2";
			}

			//maybe also reduce the size of the drink here
			//to indicate how much has been drunk
		}
	}

	public void SipButton(){
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (!(gm.curState == "shoot1" || gm.curState == "shoot0")) return;
		StartCoroutine(Sip());
	}
	//other custom functions here
}
