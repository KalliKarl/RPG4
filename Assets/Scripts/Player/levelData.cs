using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelData : MonoBehaviour {

    public float[] expPlayer= new float[20];
    public float playerExpRate = 0.4392f;
    void Start(){
        expPlayer[0] = 10;

        for (int i = 1; i < expPlayer.Length; i++) {
            expPlayer[i] = (expPlayer[i - 1] + 15 + (expPlayer[i - 1] * playerExpRate));
        }
    }

    
}
