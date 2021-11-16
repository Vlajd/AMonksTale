using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGhostMovement : MonoBehaviour
{
    
    public playerGhostController controller;
    [SerializeField] private float walkSpeed = 35f;
    [SerializeField] private float sprintSpeed = 60f;
    private float speed;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    public bool isControlled;


    // Update
    void Update () {

        if (isControlled){

            // set speed
            if (Input.GetAxisRaw("Sprint") == 1f)
                speed = sprintSpeed;
            else
                speed = walkSpeed;

            // moving (horizontal)
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

            // moving (vertical)
            verticalMove = Input.GetAxisRaw("Vertical") * speed;
                
        }
    }


    // Fixed Update
    void FixedUpdate () {

        // Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime, false /* Would be Crouch */, false /* Would be Jump */);
    }
}
