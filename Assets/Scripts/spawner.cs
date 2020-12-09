using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject[] enemies,spots;  
    private Vector3 spawnValues;
    public float spawnWait,range;
    public int startWait, mobIndex,limit,amount; 
    public bool stop,spot;
    void Start(){
        spawnValues = this.transform.position;
     
    }
    public void startCorotine() {
        StartCoroutine(waitSpawner());
        stop = false;
    }
    IEnumerator waitSpawner() {
        yield return new WaitForSeconds(startWait);

        while (!stop) {

            int randSpot = Random.Range(0,8);

            if (spot) {
                spawnValues = spots[randSpot].transform.position;
            }
            else {

                spawnValues = new Vector3(Random.Range(transform.position.x + range, transform.position.x - range), transform.position.y, Random.Range(transform.position.z + range, transform.position.z - range));

            }


            GameObject enemyAI = Instantiate(enemies[mobIndex], spawnValues, gameObject.transform.rotation) as GameObject;
            enemyAI.transform.parent = this.transform;
            if (enemyAI != null)
                amount++;
            if (amount >= limit)
                stop = true;
            yield return new WaitForSeconds(spawnWait);
        } 
        
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void OnTriggerEnter(Collider other) {
        //Debug.Log(other.name);

        Destroy(this.gameObject.GetComponent<Rigidbody>());
        if (other.name == "Player") {
            StartCoroutine(waitSpawner());
            this.GetComponent<spawner>().stop = false;
        }

    }
    private void OnTriggerExit(Collider other) {
        if (other.name == "Player") {
            Transform[] ts = gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform child in ts) {
                if (child.gameObject.layer == 11)
                    Destroy(child.gameObject);
            }

            this.GetComponent<spawner>().amount = 0;
        }
    }
}
