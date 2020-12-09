using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {
    public float radius = 3f;
    public Transform interactionTransform;
    bool isFocus = false;
    Transform player,focus,newFocus;
    bool hasInteracted = false;
    void Update() {
        if (isFocus && hasInteracted == false) { 
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (!hasInteracted && distance <= radius) {
                hasInteracted = true;
                Interact(0);
                //Debug.Log("has interact with " + interactionTransform.name);
            }
        }
    }
    public void OnFocused(Transform playerTransform) {
        

        isFocus = true;
        player = playerTransform;
        hasInteracted = false;

        
    }
    public void OnDefocused() {

        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    public virtual void Interact(int a) {
        // this method is meant to be overwritten
        //Debug.Log("has interract with =" + transform.name);

        player.GetComponent<Animator>().SetFloat("Velocity", 0);

    }
    void OnDrawGizmosSelected() {
        if (interactionTransform == null) {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
  

}
