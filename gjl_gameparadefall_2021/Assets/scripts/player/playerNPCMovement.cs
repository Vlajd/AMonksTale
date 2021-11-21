// only using the basics, damn . . .
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerNPCMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private playerNPCController controller;
    public float walkSpeed = 35f;
    public float sprintSpeed = 60f;
    private float speed;
    public float horizontalMove = 0f;
    private bool jump = false;
    public bool isControlled;
    [SerializeField] private AudioSource[] audio = new AudioSource[3];
    public bool boxPush = false;

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
            if (Input.GetKeyDown("space") || Input.GetKeyDown("w")) {

                jump = true;
                animator.SetBool("hasJumped", true);
            }
        }

        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (Mathf.Abs(horizontalMove) > 0.01)
            audio[0].volume = 100f;
        else
            audio[0].volume = 0f;

        if(jump)
            audio[1].Play();

        if(boxPush && Mathf.Abs(horizontalMove) > 0.1f && Mathf.Abs(horizontalMove) < walkSpeed)
            audio[2].volume = 1f;
        else
            audio[2].volume = 0f;
    }


    // Fixed Update
    void FixedUpdate () {

        // Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false /* Would be Crouch */, jump);
        jump = false;
    }

    public void hasDoneJumping () {

        animator.SetBool("hasJumped", false);
    }
}