using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class dropEquip : MonoBehaviour, IDropHandler {
    InventorySlot[] slots;
    Item item;
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("DropEquip " + eventData.pointerDrag.name);
        slots = StaticMethods.FindInActiveObjectByName("ItemsParent").GetComponentsInChildren<InventorySlot>();
        int indexSlot = 0;
        Int32.TryParse(eventData.pointerDrag.name, out indexSlot);
        item = slots[indexSlot].GetComponent<InventorySlot>().item;
        if (item != null) {
            item.Use();
        }
    }
}
