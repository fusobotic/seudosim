using UnityEngine;
using System.Collections;

public class ShopInventory : MonoBehaviour {

	public float spawnDist,
				 moveTime;
	public int moveIndex;

	public GameObject itemPrefab;

	private Vector3 initLoc;

	private Items itm;
	
	void Start () {
		initLoc = transform.localPosition;
		itm = GameObject.Find("GameManager").GetComponent<Items>();
		moveIndex = itm.shopSpot; //gets last spot
		for(int i =0; i < itm.items.Length; i++){
			Vector3 newLoc = new Vector3 (spawnDist*i,0,0);
			GameObject shopItem = Instantiate(itemPrefab, newLoc,Quaternion.identity) as GameObject;
			shopItem.GetComponent<ShopItem>().ind = i;
			shopItem.transform.SetParent(gameObject.transform, false);
			
		}
	}

	void Move(bool left){
		moveIndex++;
		itm.shopSpot = moveIndex;
		//iTween.MoveTo(originalpos+(spawnDist*moveIndex), moveTime);
	}

	void Update(){
		if(Input.anyKeyDown){
			Move(false);
		}
	}
}
