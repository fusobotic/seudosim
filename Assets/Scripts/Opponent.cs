using UnityEngine;
using System.Collections;

public class Opponent : MonoBehaviour {

	//FOR ANIMATION

	//only focus on animations that are essential for input feedback
	//most of this will be handled via scripting and not through physical
	//interaction but there will be animations resultant

	//add more functionality for multiple cartridges later

	public float maxFirstWaitTime = 2.5f;

	public int cylinderIndex = 1,
			   cartridge,
			   drinkIndex = 0,
			   drinkMin,
			   drinkMax,
			   lethalDrink = 0;
			   

	public float waitChanceMod = 0f,
				 waitTimeMod = 0f,
				 drinkChanceMod = 0f;

	public bool hammerBack;
	public bool deciding = false;
	public bool firstShot = true;

	private GameManager gm;

	public GameObject blood;
	public GameObject click;
	public GameObject[] drinks; //just the glass models with modifiable level

	// Use this for initialization
	void Start () {
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		FillCatriges();

		
		if(Items.equippedDrink != null){
			//Instantiate(drinks[Random.Range(0,drinks.Length)], drinkPos, Quaternion.identity);
		}
	}

	void Update () {
		if(gm.curState == "shoot0" && firstShot){ //decide if he wants to shoot first
			deciding = true;
			firstShot = false;
			StartCoroutine(DelayShot(false));
		}
		if (gm.curState == "shoot2" && !deciding){ //he needs to shoot
			deciding = true;
			StartCoroutine(DelayShot(true));
		}

		if (gm.curState == "shoot1"){
			StopCoroutine(DelayShot(true));
			deciding = false;
		}
	}

	void FillCatriges(){
		cartridge = Random.Range(1,7);

		//doesn't need its own function
		if (Items.equippedDrink != null){
			lethalDrink = Random.Range(drinkMin, drinkMax+1); //drink itself might rewrite this value
		}

		if (Random.value >= .5){
			hammerBack = true;
		} else {
			hammerBack = false;
		}
		//hammer back is only aesthetic in this instance
	}

	void PullTrigger(){
		if(lethalDrink != 0 && Random.value >= (.75 - drinkChanceMod)){
			StartCoroutine(Sip());
			gm.curState = "shoot1";
			// has a chance to take a drink anytime he would normally shoot
		}

		else if(cartridge == cylinderIndex){
			//spawn an explosion as well
			Instantiate(blood, new Vector3 (45f,3.4f,7.6f), Quaternion.Euler(0,90,0));
			gm.curState = "shoot1"; //do this so that the camera pans before winning
			gm.Win();
		}
		else{
			gm.curState = "shoot1";
			Instantiate(click, new Vector3 (38f,3.3f,38f), Quaternion.Euler(18,95,12));
			cylinderIndex++;
		}
		deciding = false;
	}

	public IEnumerator DelayShot(bool notFirst){

		if (Random.value >= (.5 - waitChanceMod) && !notFirst){ //half chance of just waiting forever

			yield return new WaitForSeconds(Random.Range(1f,maxFirstWaitTime));
			if (gm.curState == "shoot0"){
				PullTrigger();
			}
		}
		else if (notFirst){
			yield return new WaitForSeconds(Random.Range(.5f, 2f) + waitTimeMod);
			PullTrigger();
		}
	}

	public IEnumerator Sip(){
		drinkIndex++;
		//play drink anim
		yield return new WaitForSeconds(1); //animation length
		
		if (drinkIndex == lethalDrink){
			gm.Win();
			//killed opponent anim
		}
	}
}
