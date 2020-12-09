using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Potion")]
public class Potion : Item
{
    public bool potion = false;
    public int Hp;
    public override void Use()
    {
        base.Use();
        InventorySlot[] slots;
        GameObject player = GameObject.Find("Player");
        int can = player.GetComponent<PlayerStats>().currentHealth;
        int maxcan = player.GetComponent<PlayerStats>().maxHealth;
        
        if (can > 0 && can < maxcan){
            player.GetComponent<PlayerStats>().Healthmodifer(Hp);// Add HP to player
            slots = StaticMethods.FindInActiveObjectByName("ItemsParent").GetComponentsInChildren<InventorySlot>();
        
            if (Inventory.instance.items.Count > 0){
                for (int i = 0; i < Inventory.instance.items.Count; i++){
                    if (Inventory.instance.items[i].name == this.name){//if found in inventory
                        if (slots[i].stack < 1 && slots[i].stack == 0){//if its last one remove from inventory
            
                            removeFromInventory();
                            slots[i].txtStack.enabled = false;

                        }
                        else {
                            Inventory.instance.items[i].stack -= 1;

                            StaticMethods.refreshStack();
                        }
                    }
                }
            }
        }
        else {
            Debug.LogWarning("Health Full or you died.");
        }
    }
}
