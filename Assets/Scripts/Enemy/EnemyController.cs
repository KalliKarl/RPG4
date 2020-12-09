using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target,spawnPoint;
    NavMeshAgent agent;
    CharacterCombat combat;
    int walkRadius = 30;
    private float walkCoolDown = 0f;
    public bool isAgressive = false;
    Vector3 finalDestination;
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        spawnPoint = transform;
    }

    // Update is called once per frame
    void Update(){
        //AI Walk 
        walkCoolDown -= Time.deltaTime;
        float distance = Vector3.Distance(target.position,  transform.position);
        float distanceWalk = Vector3.Distance(finalDestination, transform.position);
        if (distanceWalk <= 1.2f)
            gameObject.GetComponent<Animator>().SetBool("isWalk", false);
        if (distance <= lookRadius) {

            //Move Target
            if (target.name == "Player" && target.GetComponent<Player>().isDead != true && isAgressive == true && isDead==false) {
                moveTarget();
            }
                
            //Attack target;
            if (distance <= agent.stoppingDistance && isAgressive == true && target.GetComponent<Player>().isDead != true && isDead == false) {
                attackTarget();
                faceTarget();
            }
            if (target.GetComponent<Player>().isDead && isDead == false)
                continueWalk();
        }
        else {// continue walk around
            continueWalk();

        }
    }

    void attackTarget() {
        this.GetComponent<Animator>().SetBool("isWalk", false);
        CharacterStats targetStats = target.GetComponent<CharacterStats>();
        if (targetStats != null) {
            combat.Attack(targetStats, 1);
        }
    }
    void moveTarget() {
        agent.SetDestination(target.position);
        if (gameObject.layer == 11)
            this.GetComponent<Animator>().SetBool("isWalk", true);
    }
    void continueWalk() {
        this.GetComponent<Animator>().SetBool("isAttack",false);
        if (walkCoolDown <= 0) {
            Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
            if (gameObject.layer == 11)
                this.GetComponent<Animator>().SetBool("isWalk", true);
            else
                Debug.Log(gameObject.layer);
            randomDirection += spawnPoint.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
            finalDestination = hit.position;

            agent.SetDestination(finalDestination);
            walkCoolDown = 15f;
        }
    }
    void faceTarget() {

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation , lookRotation, Time.deltaTime * 5);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
