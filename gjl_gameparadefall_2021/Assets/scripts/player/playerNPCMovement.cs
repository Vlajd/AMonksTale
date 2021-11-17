// only using the basics, damn . . .
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerNPCMovement : MonoBehaviour
{
    [SerializeField] private playerNPCController controller;
    public float walkSpeed = 35f;
    public float sprintSpeed = 60f;
    private float speed;
    public float horizontalMove = 0f;
    private bool jump = false;
    public bool isControlled;

    // Update
    void Update () {

        // initialize
        if (isControlled) {

            // set speed
            if (Input.GetAxisRaw("Sprint") == 1f)
                speed = sprintSpeed;
            else
                speed = walkSpeed;

            // moving
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

            // jump
            if (Input.GetAxisRaw("Jump") == 1f || Input.GetAxisRaw("Vertical") == 1f)
                jump = true;
        }
    }


    // Fixed Update
    void FixedUpdate () {

        // Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false /* Would be Crouch */, jump);
        jump = false;
    }
}