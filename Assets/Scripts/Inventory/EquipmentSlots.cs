using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlots : MonoBehaviour
{
    public GameObject menu;
    public Text[] info;
    public Image icon;
    [SerializeField]
    public Equipment item;
    
    public void AddItem(Equipment newItem) {

        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        this.GetComponent<Button>().interactable = true;
        this.GetComponent<Image>().raycastTarget = true;
    }

    public void Unequipment() { // unequipall delete

        item = null;
        icon.sprite = null;
        icon.enabled = false;
        this.GetComponent<Button>().interactable = false;
        this.GetComponent<Image>().raycastTarget = false;
    }
   
    //Equipment İnfos
    public void OnMouseEnter() {
        if(item != null) {
            menu.SetActive(true);// Menu Active
            menu.GetComponent<RectTransform>().anchoredPosition = StaticMethods.FindInActiveObjectByName("Inventory").GetComponent<RectTransform>().anchoredPosition;
            //Debug.Log(StaticMethods.FindInActiveObjectByName("Inventory").GetComponent<RectTransform>().anchoredPosition);
            //Debug.Log(StaticMethods.FindInActiveObjectByName("EquipParent").GetComponent<RectTransform>().anchoredPosition);
            //Debug.Log(StaticMethods.FindInActiveObjectByName("EquipSlot1").GetComponent<RectTransform>().anchoredPosition);
            //menu.GetComponent<RectTransform>().anchoredPosition +=new Vector2(0f,10f);
            if (item.equipSlot.ToString() == "Weapon") {//Weapons infos
                info[1].text = "Weapon Type :" + item.wepType.ToString();
                float maxdamage = item.damageModifier + ((item.damageModifier / 100f) * 15);
                info[3].text = "Phy. atk. pwr. : " + item.damageModifier.ToString() + "~" + maxdamage;
                info[5].gameObject.SetActive(true);
                info[6].gameObject.SetActive(true);
                info[5].text = "Attack Distance: " + item.range.ToString() + "m";
                info[6].text = "Critical " + item.critical.ToString() + "(100%)";
            }
            else if (item.armorType.ToString() != "none") {//Armor infos
                info[1].text = "Armor Type : " + item.armorType.ToString();
                info[3].text = "Phy. def. pwr. :" + item.armorModifier.ToString();
                info[5].gameObject.SetActive(false);
                info[6].gameObject.SetActive(false);
            }
            else if(item.equipSlot.ToString() == "Shield") {// shield infos
                info[1].text = "Shield Type : " + item.equipSlot.ToString();
                info[3].text = "Phy. def. pwr. :" + item.armorModifier.ToString();
                info[5].gameObject.SetActive(true);
                info[5].text = "Block Change :" + item.block.ToString();
                info[6].gameObject.SetActive(false);
            }
            if (item.plus > 0)// Plus item name
                info[0].text = item.name.ToString()+" ( +"+item.plus.ToString()+" )"; 
            else  // Normal item name
                info[0].text = item.name.ToString();

            info[12].text = "Mounting Part : " + item.equipSlot;
            info[2].text = "Degree : " + item.degree.ToString() +" degrees"; // Degree
            info[4].text = "Durability : " + item.durability.ToString()+"/"+ item.durability.ToString(); //Durability
            info[7].text = "Required Level : " + item.level.ToString(); // İtem Level
            
            if (item.gender.ToString() != "none")//cinsiyet
                info[11].gameObject.SetActive(true);
            else
                info[11].gameObject.SetActive(false);
            info[11].text = item.gender.ToString();
            
            if (item.strBuff != 0)// STR BUFF
                info[8].gameObject.SetActive(true);
            else
                info[8].gameObject.SetActive(false);
            info[8].text = "Str " + item.strBuff.ToString()+"/"+item.strBuff.ToString() + " increase";

            if (item.intBuff != 0)// INT BUFF
                info[9].gameObject.SetActive(true);
            else
                info[9].gameObject.SetActive(false);
            info[9].text ="Int "+ item.intBuff.ToString()+"/"+item.intBuff.ToString() +" increase";

            if (item.durBuff != 0)// Durability BUFF
                info[10].gameObject.SetActive(true);
            else
                info[10].gameObject.SetActive(false);
            info[10].text = "Durability" +item.durBuff.ToString() +"% increase";
            
        }

    }
    public void OnMouseExit() {
        //Debug.Log("Exit" + this.name.ToString());
        menu.SetActive(false);
        
    }

}
