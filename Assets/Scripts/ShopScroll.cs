using UnityEngine;
using System.Collections;

public class ShopScroll : MonoBehaviour {

	private ShopInventory si;
	public bool left;
	
	void Start () {
		si = GameObject.Find("Inventory").GetComponent<ShopInventory>();
	}
	
	void OnMouseEnter(){
		print("working");
		si.Move(left);
	}
}
