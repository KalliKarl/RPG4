using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    #region Singelton

    public static PlayerManager instance;

    private void Awake() {
        instance = this;
    }

    #endregion

    [SerializeField] CapsuleCollider capsul;
    [SerializeField] CharacterController characterController;
    [SerializeField] SphereCollider sphere;
    [SerializeField] Rigidbody rb;
    public GameObject player;

    private void Start() {
        
        capsul = player.GetComponent<CapsuleCollider>();
        characterController = player.GetComponent<CharacterController>();
        sphere = player.GetComponent<SphereCollider>();
        rb = player.GetComponent<Rigidbody>();
    }

    public void killPlayer() {

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        capsul.enabled = false;
        characterController.enabled = false;
        sphere.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        Animator animator = player.GetComponent<Animator>();
        animator.SetBool("isDead",true);
        player.GetComponent<Player>().isDead = true;
        Debug.Log("DEAD!");
    }
}
