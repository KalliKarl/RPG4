using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSpot : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) {
        Destroy(this.gameObject.GetComponent<Rigidbody>());
    }
}
