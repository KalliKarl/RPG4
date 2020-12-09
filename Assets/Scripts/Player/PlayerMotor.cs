using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public Vector3 point2 = Vector3.zero;
    public Animator animator;
    int VelocityHash;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        VelocityHash = Animator.StringToHash("Velocity");
    }

    void Update(){
        if(target != null){
            agent.SetDestination(target.position);
            FaceTarget();            
        }
        if (point2 != Vector3.zero) {
            float distonse = Vector3.Distance(agent.transform.position, point2);
            if (distonse < agent.stoppingDistance) {
                animator.SetFloat(VelocityHash, 0f); 
                //Debug.Log("STOP" + distonse);
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                point2 = Vector3.zero;
                

            }
            else {
                //Debug.Log("else Continue " + distonse);
                animator.SetFloat(VelocityHash, 1f);
                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                
            }
        }
        
    }
    public void MoveToPoint(Vector3 point){
        point2 = point;
        agent.SetDestination(point);

       // Debug.Log("MoveToPoint " + point);
       
    }
    
    public void FollowTarget( Interactable newTarget){
        agent.stoppingDistance = newTarget.radius * .6f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;
        agent.isStopped = true;
    }

    public void StopFollowingTarget(){
        target = null;
        agent.updateRotation = true;
    }
    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = lookRotation;//Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
    }
}
