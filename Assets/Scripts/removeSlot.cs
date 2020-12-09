using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class removeSlot : MonoBehaviour, IDropHandler {
    InventorySlot[] slots;
    Item item;
   
    public void OnDrop(PointerEventData eventData) {
        StaticMethods.FindInActiveObjectByName("YesOrNo").SetActive(true);
        slots = StaticMethods.FindInActiveObjectByName("ItemsParent").GetComponentsInChildren<InventorySlot>();
        drop(eventData.pointerDrag.name);
        int indexSlot = 0;
        Int32.TryParse(eventData.pointerDrag.name,out indexSlot);
        item = slots[indexSlot].GetComponent<InventorySlot>().item;

    }
    public void drop(string a) {
        Debug.Log("Dropped " + a);
    }
    public void removeThatItem() {

        Inventory.instance.Remove(item);
        StaticMethods.refreshStack();
        StaticMethods.FindInActiveObjectByName("YesOrNo").SetActive(false);
    }

}
