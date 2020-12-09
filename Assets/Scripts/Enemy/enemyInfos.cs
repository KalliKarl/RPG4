using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyInfos : MonoBehaviour{



    public float mobExpRate = 1.7f;
    public float[] expMob= new float[20];
    public int[] deffance = { 10, 12, 13, 15, 16, 17 };
    public int[] damage = {18,21,23,25,27,29 };
    public int[] maxHealth = { 54,55,85,83,119,114};
    public string[] names = { "Manguang" , "Small - Eye","Big - Eye","Old-Weasel","Weasel","WaterGhost"};



    void Start() {
        expMob[0] = 3;
        for (int i = 1; i < expMob.Length; i++) {
            expMob[i] = expMob[i - 1] + mobExpRate;
        }
    }
}
