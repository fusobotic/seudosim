using UnityEngine;
using System.Collections;


public class Items : MonoBehaviour {

	public enum ItemType{Drink, Hat, Charm, Anim};

	[System.Serializable]
	public class Item {
		public string name;
		public GameObject model;
		public bool bought;
		//public bool equipped = false;
		public int price;
		public string callName; //when equipped execute this
		public ItemType typeOf;

		public void Buy(){
			bought = true;
		}

	}

	[System.Serializable]
	public class Ammo {
		public string name;
		public GameObject model;
		public int number;
		public int price;
		//don't need callname since it will be a bullet specific detection
		//with a function directly on the bullet itself

		public void Buy(){
			number++;
		}

		public void Buy(int num){
			number += num;
		}

		public void Expend(){
			number--;
		}
	}

	//[HideInInspector]//use this once you're done testing
	public Item equippedDrink = null,
				equippedHat = null,
				equippedCharm = null,
				equippedAnim = null;
	//spawn these gameobjects at the start of the match
	//you're able to equip them in the shop

	public Item[] items;
	public Ammo[] ammos;

	 

	// Use this for initialization

	public void Equip (int i){
		Item toEquip = items[i];
		ItemType curType = toEquip.typeOf;

		if (curType == ItemType.Drink){
			equippedDrink = toEquip;
		}
		else if (curType == ItemType.Hat){
			equippedHat = toEquip;
		}
		else if (curType == ItemType.Charm){
			equippedCharm = toEquip;
		}
		else if (curType == ItemType.Anim){
			equippedAnim = toEquip;
		}

		if(toEquip.callName != ""){
			BroadcastMessage(toEquip.callName);
			//if there's a specail effect or something particular to to object, do it
		}

		//add in some type of congrats message here.

		/*foreach(Item i in items){
			if(i.typeOf == curType){
				i.equipped = false;
			}
		}

		toEquip.equipped = true;*/ 
		//only use this method if you want to have a different system
	}

	void Update(){

	}
	
}
