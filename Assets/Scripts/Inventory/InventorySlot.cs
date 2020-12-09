using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour{
    
    public Image icon;
    public Button removeButton;
    public Text txtStack;
    public int stack= 1;
    public Item item;

    public  void AddItem(Item newItem) {

        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        if (newItem.stackable) {

            txtStack.enabled = true;
            txtStack.text = stack.ToString();
        }
        //activeButton.interactable = true;

        StaticMethods.refreshStack();


    }
    public void ClearSlot() {

        item = null;
        icon.sprite = null;
        icon.enabled = false;
        //activeButton.interactable = false;
        StaticMethods.refreshStack();
    }

    public void UseItem() {
        if (item != null) {
            item.Use();
        }
    }
}
