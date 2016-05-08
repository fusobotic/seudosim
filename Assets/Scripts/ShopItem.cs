using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

	public int ind; //index for Items script, will be modded during spawn for each instance

	public Button buyBtn;
	public GameObject buyObj,
					  equipObj,
					  modelSpawn;

	public Text priceText,
				descriptText;

	private GameManager gm;
	private Items itm;

	void Start(){
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		itm = GameObject.Find("GameManager").GetComponent<Items>();
		if(itm.items[ind].bought){
			//add in some dev mode check to enable free buying
			buyObj.SetActive(false);
			equipObj.SetActive(true);
			//if(itm.items[ind] == Items.equippedDrink || itm.items[ind] == Items.equippedHat || itm.items[ind] == Items.equippedCharm || itm.items[ind] == Items.equippedAnim){
			if(itm.items[ind].equipped){
				equipObj.GetComponent<Toggle>().isOn = true;
			}
		}
		GameObject model = Instantiate(itm.items[ind].model, modelSpawn.transform.position, Quaternion.identity) as GameObject;
		model.transform.parent = gameObject.transform;
		model.transform.localScale *= itm.items[ind].modelScale;
		priceText.text = "$"+ itm.items[ind].price;
		descriptText.text = "\"" +itm.items[ind].description+"\"";
	}

	void Update(){
		if(gm.currentCoins <= itm.items[ind].price){
			priceText.color = new Color32(189,0,0,255);
		} else {
			priceText.color = Color.black;
		}
		if(gm.currentCoins <= itm.items[ind].price){	
			buyBtn.interactable = false;
		} else {
			buyBtn.interactable = true;
		}
		if(Items.equippedDrink != null)print(Items.equippedDrink.name);

	}

	public void Buy(){
		itm.items[ind].Buy();
		//maybe add some juiciness here?
		gm.currentCoins -= itm.items[ind].price;
	}

	public void Equip(){
		if(equipObj.GetComponent<Toggle>().isOn){
			itm.Equip(ind);
			//trigger a sound of some kind.
		} else {
			itm.Unequip(ind);
		}
		/*for (int i = 0; itm.items[].Length; i++){
			if(itm.items[i].typeOf == itm.items[ind].typeOf && i != ind){
				itm.items[i].enabled = false;
			}
		}*/ //revamp this later to update other item equipped buttons
	}
}
