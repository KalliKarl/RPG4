using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : Interactable {

	
	public Item item;   // Item to put in the inventory if picked up
	public int itemStack = 1;
	InventorySlot[] slots;
	public GameObject itemsParent;
	bool found;
	public override void Interact(int a) {
		base.Interact(0);

		itemsParent = StaticMethods.FindInActiveObjectByName("ItemsParent");
		PickUp();
	}

	// Pick up the item
	void PickUp() {
        #region LogPrint
        //Debug.Log("Picking up " + item.name);
		GameObject logContent = GameObject.Find("logContent");
		Color renk = new Color();
		renk = Color.red;	
		logContent.GetComponent<logViewer>().entryLog("Picking Up" + item.name , renk);
        #endregion
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

		bool stack = item.stackable;

        if (stack) {
			if (Inventory.instance.items.Count >= 0) {
				for (int i = 0; i < Inventory.instance.items.Count; i++) {

					if (Inventory.instance.items[i].name == item.name) {//if stackable item already added inventory
						Inventory.instance.items[i].stack += itemStack;
						Debug.Log(Inventory.instance.items[i].name +"   -  : "+ Inventory.instance.items[i].stack);
						found = true;
                    }
				}
                if (!found) {// if stackable item adding inventory first time
					Inventory.instance.Add(item);// Add to inventory
				}
			}
        }
        else {
			if(item.name == "gold") {
				GameObject.Find("Player").GetComponent<Player>().gold += this.GetComponent<goldCoin>().goldAmount;
				StaticMethods.FindInActiveObjectByName("txtGold").GetComponent<Text>().text = GameObject.Find("Player").GetComponent<Player>().gold.ToString();
			}
            else {
				Inventory.instance.Add(item);   // Add to inventory
			}
			
		}

        
		found = false;
		Destroy(gameObject);    // Destroy item from scene
		StaticMethods.refreshStack();
	}
}
