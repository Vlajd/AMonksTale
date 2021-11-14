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

    void Update() {

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

    void FixedUpdate () {

        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouch, isJump);
        isJump = false;
    }
}
