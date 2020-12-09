using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

public class ThirdPersonMovement : MonoBehaviour{
    public CharacterController controller;
    public Joystick joystick;
    public float speed = 3f, turnSmoothTime = 0.1f, turnSmoothVelocity,axis,horizontal,vertical;
    Vector3 gravity = new Vector3(0f,-9.81f,0f);
    Transform cam;
    Animator animator;
    int VelocityHash;
    bool keyUp = false;
    Canvas canvas;
    private void Start() {
        cam = Camera.main.transform;
        animator = GetComponentInChildren<Animator>();
        VelocityHash = Animator.StringToHash("Velocity");
        canvas = StaticMethods.FindInActiveObjectByName("CanvasUI").GetComponent<Canvas>();
        this.GetComponent<NavMeshAgent>().isStopped = true;
    }
    void Update(){
        

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (joystick.Horizontal >= .2f)
            horizontal = joystick.Horizontal;
        else if (joystick.Horizontal <= -.2f)
            horizontal = joystick.Horizontal;
        else if (canvas.GetComponent<MenuUI>().controlType == 0)
            horizontal = 0;
        if (joystick.Vertical >= .2f)
            vertical = joystick.Vertical;
        else if (joystick.Vertical <= -.2f)
            vertical = joystick.Vertical;
        else if(canvas.GetComponent<MenuUI>().controlType == 0)
            vertical = 0;
        axis = Mathf.Clamp01(new Vector2(horizontal,vertical).magnitude);
        
        
        float a = animator.GetFloat(VelocityHash);
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
            keyUp = true;
        }
        if (a > 0 && keyUp) {
            a -= .02f;
            animator.SetFloat(VelocityHash,a);
        }
        if(a < 0) {
            animator.SetFloat(VelocityHash,0);
        }

        if (axis == 0 && canvas.GetComponent<MenuUI>().controlType == 0 && this.GetComponent<NavMeshAgent>().isStopped == true)
            animator.SetFloat(VelocityHash,0);

        move();
        
    }


    void move() {
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f) {
            animator.SetFloat(VelocityHash, axis);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * (speed * axis) * Time.deltaTime + gravity);
        }
        
    }
}
