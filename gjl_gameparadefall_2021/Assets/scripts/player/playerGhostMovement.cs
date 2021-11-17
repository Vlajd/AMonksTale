using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGhostMovement : MonoBehaviour
{
    
    [SerializeField] private playerGhostController controller;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private float walkSpeed = 35f;
    [SerializeField] private float sprintSpeed = 60f;
    private float speed;
    public float horizontalMove = 0f;
    public float verticalMove = 0f;
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

            // cancel if past world limmits
            if (transform.position.y < gameManager.GetComponent<gameManager>().minY && verticalMove < 0f)
                verticalMove = 1f;
            else if (transform.position.y > gameManager.GetComponent<gameManager>().maxY && verticalMove > 0f)
                verticalMove = -1f;
                
        }
    }


    // Fixed Update
    void FixedUpdate () {

        // Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime, false /* Would be Crouch */, false /* Would be Jump */);
    }
}
