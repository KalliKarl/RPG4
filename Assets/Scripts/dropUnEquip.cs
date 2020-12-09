using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class dropUnEquip : MonoBehaviour, IDropHandler{
    EquipmentSlots[] slots;
    Equipment item;
    public void OnDrop(PointerEventData eventData){
        Debug.Log("Drop Inventory " + eventData.pointerDrag.name);
        int indexSlot = 0;
        Int32.TryParse(eventData.pointerDrag.name, out indexSlot);
        StaticMethods.FindInActiveObjectByName("GameManager").GetComponent<EquipmentManager>().Unequip(indexSlot);
    }
}