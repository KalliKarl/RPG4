using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticMethods 
{
    public static GameObject FindInActiveObjectByName(string name) {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++) {
            if (objs[i].hideFlags == HideFlags.None) {
                if (objs[i].name == name) {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
    public static void refreshStack() {
        InventorySlot[] slots;
        slots = StaticMethods.FindInActiveObjectByName("ItemsParent").GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < Inventory.instance.items.Count; i++) { // Update inventory stack count text UI
            slots[i].stack = Inventory.instance.items[i].stack;
            slots[i].txtStack.text = slots[i].stack.ToString();
            slots[i].GetComponent<Image>().raycastTarget = true;
        }
        for (int i = Inventory.instance.items.Count; i <20 ; i++) {
            slots[i].GetComponent<Image>().raycastTarget = false;
        }
    }
}
