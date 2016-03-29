using UnityEngine;
using System.Collections;


public class Items : MonoBehaviour {

	public enum ItemType{Drink, Hat, Charm, Anim};

	[System.Serializable]
	public class Item {
		public string name;
		public GameObject model;
		public bool bought;
		public int price;
		public string callName; //could do something different
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

	public Item[] items;
	public Ammo[] ammos;

	// Use this for initialization

	void Equip (){




	}

	void Decriment (){

	}
}
