 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    
    public int level, gold;
    public float experience, skillPoint;
    public float previousLvl, nextLvl, diffrence, expPercent;
    public string[] items, equips;
    public GameObject lvlUpEffect;
    GameObject gameManager;
    Inventory playerInventory;
    Item item;
    itemManager itManager;
    Equipment[] currentEquipment;
    [SerializeField]
    PlayerData data;
    public bool isDead;

    public object currentEquip { get; internal set; }

    public void Start() {
        GameObject goldTxt = StaticMethods.FindInActiveObjectByName("txtGold");
        goldTxt.GetComponent<Text>().text = gold.ToString();
        
    }

    public void AddExperience(float IncomingExp) {

        experience += IncomingExp;
        if (experience > this.GetComponent<levelData>().expPlayer[level - 1]) {// lvl up
            int i = 0; 
            bool checking = true;
            while (checking) {
                if (experience > this.GetComponent<levelData>().expPlayer[i]) {
                    i++;
                }
                else {
                    checking = false;
                    level = i + 1;

                    this.GetComponent<PlayerStats>().maxHealth += 14;
                    this.GetComponent<PlayerStats>().maxMana += 14;
                    this.GetComponent<PlayerStats>().Healthmodifer(this.GetComponent<PlayerStats>().maxHealth);
                    GameObject.Find("HpBarRight").GetComponent<Image>().fillAmount = 1;
                    GameObject _stats = GameObject.Find("Stats");
                    _stats.GetComponent<HpMpStatsUI>().MaxHp.text = this.GetComponent<PlayerStats>().maxHealth.ToString();
                    _stats.GetComponent<HpMpStatsUI>().CurHp.text = this.GetComponent<PlayerStats>().currentHealth.ToString();
                    Instantiate(lvlUpEffect, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.Euler(-90, 0, 0), this.transform);
                }
            }
        }
        UIUpdate();
    }

    public void AddSkillPoint(float IncomingSp) {
        skillPoint += IncomingSp;
        UIUpdate();
    }
    public void UIUpdate() {
        GameObject stats = GameObject.Find("Stats");
        stats.GetComponent<HpMpStatsUI>().Level.text = "Level : " + level.ToString();
        stats.GetComponent<HpMpStatsUI>().Exp.text = "Expereince : " + experience.ToString();
        stats.GetComponent<HpMpStatsUI>().Sp.text = "SkillPoint : " + skillPoint.ToString();
        previousLvl =0;
        if (level > 1) {
            previousLvl = this.GetComponent<levelData>().expPlayer[level - 2];
        }
        nextLvl = this.GetComponent<levelData>().expPlayer[level-1];
        diffrence = nextLvl - previousLvl;
        expPercent = (experience - previousLvl) / diffrence;
        expPercent = (float)System.Math.Round(expPercent,4);
        GameObject.Find("ExpPercent").GetComponent<Text>().text = ((float)System.Math.Round(expPercent, 4) * 100).ToString() + " %";
        GameObject.Find("ExpBar").GetComponent<Image>().fillAmount = expPercent;
    }
    public void SavePlayer() {

        GameObject itemler = GameObject.Find("ItemManager");
        itManager = itemler.GetComponent<itemManager>();
        gameManager = GameObject.Find("GameManager");
        playerInventory = gameManager.GetComponent<Inventory>();
        currentEquipment = gameManager.GetComponent<EquipmentManager>().CurrentEq();
        
        equips = new string[currentEquipment.Length];
        for(int i = 0; i < equips.Length; i++) {
            if(currentEquipment[i]!=null )
                equips[i] = currentEquipment[i].name;
            
        }
        
        items = new string[playerInventory.items.Count];
        for (int i = 0; i < items.Length; i++) {
            items[i] = playerInventory.items[i].name;
        }
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer() {
        GameObject itemler = GameObject.Find("ItemManager");
        itManager = itemler.GetComponent<itemManager>();
        data = SaveSystem.LoadPlayer();
        if(Inventory.instance.items.Count >0)
        Inventory.instance.items.Clear();

        level = data.level;
        skillPoint = data.skillPoint;
        experience = data.Experience;
        gold = data.gold;

        for (int i =0; i<itManager.items.Count;i++) {
            for(int j = 0; j < data.items.Length ;j++) {
                
                string a = itManager.items[i].name;
                string b = data.items[j].ToString();
                if ( a == b ) {
                    item = itManager.items[i];
                    Inventory.instance.Add(item);
                }
            }
        }

        for (int i = 0; i < itManager.items.Count; i++) {
            for (int j = 0; j < data.equips.Length; j++) {
                if (data.equips[j] != null) {
                    string a = itManager.items[i].name;
                    string b = data.equips[j].ToString();

                    if (a == b) {
                        //item = itManager.items[i];
                        Equipment eq1 = (Equipment)itManager.items[i];
                        EquipmentManager.instance.Equip(eq1);
                    }
                }
            }
        }


        GameObject itemManager = GameObject.Find("itemManager");

        GameObject goldTxt = StaticMethods.FindInActiveObjectByName("txtGold");
        goldTxt.GetComponent<Text>().text = gold.ToString();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}