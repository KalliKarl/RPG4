using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotArrow : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float thrust;
    public Vector3 CmFwd;
    public GameObject Boom;
    bool explosion = false;
    Transform lastPosition;
    void Start() {
        //GameObject.Find("Player").transform.forward
        CmFwd = Camera.main.transform.forward;
        CmFwd += new Vector3(0f,0.05f,0f);
        rb.AddForce(CmFwd * thrust);
        Destroy(gameObject, 30);
    }

    // Update is called once per frame
    void Update(){
        if(rb.velocity.magnitude >0 )
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name != "Player") {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            this.transform.parent = other.transform;
            if (!explosion) {
                GameObject patladi =Instantiate(Boom, this.transform.position,this.transform.rotation);
                Destroy(patladi,1);
                explosion = true;
            }
           
            Interactable interactable = other.gameObject.GetComponentInParent<Interactable>();
            if (interactable != null) {
                GameObject.Find("Player").GetComponent<PlayerControl>().SetFocus(interactable);
                if(other.gameObject.layer == 11) {
                    int a;
                    if (other.gameObject.name == "Head")
                        a = 2;
                    else if (other.gameObject.name == "Body")
                        a = 1;
                    else
                        a = 1;
                    other.gameObject.GetComponentInParent<Enemy>().Interact(a);
                }
                    
            }
            rb.velocity = Vector3.zero;
            this.GetComponent<BoxCollider>().enabled = false;
            this.GetComponent<BoxCollider>().isTrigger = false;
            Debug.Log("Arrow Hit : " + other.gameObject.name);
        }
    }
    private void FixedUpdate() {
        
    }
}
