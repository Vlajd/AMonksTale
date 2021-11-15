using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
    
    public playerController controller;
    public float walkSpeed = 40f;
    public float sprintSpeed = 60f;
    float horizontalMove = 0f;
    bool isJump = false;
    bool isCrouch = false;
    public bool isControlled;
    bool hasToggled = false;
    [SerializeField] private bool isGhost;
    [SerializeField] private Collider2D ghostEnableCollider;
    [SerializeField] private Rigidbody2D ghostEnableRigidBody;
    int i = 0;

    void Start () {

        if (isGhost == true) {

            ghostEnableRigidBody.isKinematic = true;
        }
        else {
            ghostEnableRigidBody.isKinematic = false;
        }

        InvokeRepeating("toggle", 0.3f, 0.3f);
    }

    void Update() {

        if (isControlled == true) {
            checkInputs();
        }
        else {
            horizontalMove = 0;
            isJump = false;
            isCrouch = false;
        }
    }

    void checkInputs() {

        if (Input.GetAxisRaw("Sprint") == 1 && Input.GetAxisRaw("Vertical") >= 0) {

            horizontalMove = Input.GetAxisRaw("Horizontal") * sprintSpeed;
        }
        else {
            
            horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;
        }

        if (Input.GetAxisRaw("Vertical") >= 0.5f || Input.GetAxisRaw("Jump") == 1) {
            
            isJump = true;
        }
        else if (Input.GetAxisRaw("Vertical") <= -0.5f || Input.GetAxisRaw("Crouch") == 1) {

            isCrouch = true;
        }
        else if (Input.GetAxisRaw("Vertical") == 0) {

            isCrouch = false;
        }
    }

    void toggle () {
        if (Input.GetAxisRaw("Toggle") * Time.fixedDeltaTime > 0.0f && Input.GetAxisRaw("Toggle") * Time.fixedDeltaTime <= 3.0f && hasToggled == false) {
            hasToggled = true;

            if (i < 1) {
            
                if (isGhost == true) {
                
                    ghostEnableCollider.enabled = true;
                    ghostEnableRigidBody.isKinematic = false;
                    transform.parent = null;

                    isGhost = false;
                }

                i++;
            }

            if (isControlled == true) {
                isControlled = false;
            }
            else if (isControlled == false) {
                isControlled = true;
            }

            hasToggled = false;
        }
    }

    public void hardToggle () {

        hasToggled = true;

        if (i < 1) {
            
            if (isGhost == true) {
                
                ghostEnableCollider.enabled = true;
                ghostEnableRigidBody.isKinematic = false;
                transform.parent = null;

                isGhost = false;
            }

            i++;
        }

        if (isControlled == true) {
            isControlled = false;
        }
        else if (isControlled == false) {
            isControlled = true;
        }

        hasToggled = false;
    }


    void FixedUpdate () {

        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouch, isJump);
        isJump = false;
    }
}