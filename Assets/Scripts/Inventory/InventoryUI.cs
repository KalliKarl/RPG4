
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemParents;
    public GameObject inventoryUI;


    Inventory inventory;

    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;
        slots = itemParents.GetComponentsInChildren<InventorySlot>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory")){

            inventoryUI.SetActive(!inventoryUI.activeSelf);
            inventoryUI.GetComponent<RectTransform>().anchoredPosition = Vector3.zero; 

        }
        
    }

    void UpdateUI() {
        for( int i = 0; i < slots.Length; i++) {
            if (i < inventory.items.Count) {

                slots[i].AddItem(inventory.items[i]);
                slots[i].stack = slots[i + 1].stack;
                slots[i].txtStack.text = slots[i + 1].txtStack.text;
            }
            else {
                slots[i].txtStack.enabled = false;
                slots[i].ClearSlot();
            }
        }
    }
}
