using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopInventory : MonoBehaviour {

	public float spawnDist,
				 moveTime;
	public int moveIndex;

	public GameObject itemPrefab;
	public Text myMoney;

	private Vector3 initLoc;

	private Items itm;
	private GameManager gm;
	
	void Start () {
		initLoc = transform.localPosition;
		itm = GameObject.Find("GameManager").GetComponent<Items>();
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		moveIndex = itm.shopSpot; //gets last spot
		myMoney = GameObject.Find("MyMoney").GetComponent<Text>();
		for(int i =0; i < itm.items.Length; i++){
			Vector3 newLoc = new Vector3 (spawnDist*i,0,0);
			GameObject shopItem = Instantiate(itemPrefab, newLoc,Quaternion.identity) as GameObject;
			shopItem.GetComponent<ShopItem>().ind = i;
			shopItem.transform.SetParent(gameObject.transform, false);
			
		}
	}

	public void Move(bool left){
		//check if index is < 0 or more than itm.items.Length
		if(left){
			if(moveIndex > 0){
				moveIndex--;
			}
		} else if(moveIndex < itm.items.Length - 1){
			moveIndex++;
		}
		itm.shopSpot = moveIndex;
		//iTween.MoveTo(originalpos+(spawnDist*moveIndex), moveTime);
	}

	void Update(){
		myMoney.text = "$"+gm.currentCoins;
		if (Input.GetKeyDown(KeyCode.Escape)){
			MainMenu();
		}
		//iTween.MoveUpdate(gameObject, newLoc, moveTime);
		iTween.MoveUpdate(gameObject, iTween.Hash("x", -moveIndex*spawnDist, "islocal", true, "time", moveTime));
	}

	public void MainMenu(){
		SceneManager.LoadScene("MainMenu");
	}
}
