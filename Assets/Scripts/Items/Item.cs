using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item" , menuName = "Inventory/Item")]
public class Item : ScriptableObject{

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false, stackable = false;
    public int stack = 1, price;
    InventorySlot[] slots;

    public GameObject itemsParent;

    public virtual void Use() {
        //some action here
        //use item
        //Debug.Log("Using " + name);

    }

    public void removeFromInventory() {
        Inventory.instance.Remove(this);

        StaticMethods.refreshStack();

    }
   
}
