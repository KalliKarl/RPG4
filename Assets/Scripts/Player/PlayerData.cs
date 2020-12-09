using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{

    public int level, gold;
    public float skillPoint, Experience;
    public float[] position;
    public string[] items, equips;
    public PlayerData(Player player) {
        //GameObject manager = GameObject.Find("GameManager");
        //item = manager.GetComponent<Inventory>().items;
        gold = player.gold;
        level = player.level;
        skillPoint = player.skillPoint;
        Experience = player.experience;

        equips = new string[player.equips.Length];
        for (int i = 0; i < equips.Length; i++) {
            equips[i] = player.equips[i];
        }


        items = new string[player.items.Length];
        for (int i = 0; i < player.items.Length; i++) {
            items[i] = player.items[i];
        }

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }
}
