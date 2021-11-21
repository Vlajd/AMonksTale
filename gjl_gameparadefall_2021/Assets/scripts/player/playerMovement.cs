// only using the basics, damn . . .
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private playerController controller;
    [SerializeField] private GameObject[] item = new GameObject[5];
    [SerializeField] private float itemPickUpRange = 1f;
    public float walkSpeed = 35f;
    public float sprintSpeed = 60f;
    private float speed;
    public float horizontalMove = 0f;
    private bool jump = false;
    public bool isControlled;
    public bool playerIsInBounds;
    public bool isCarrying = false;
    public bool isTriggeredEnd = false;
    [SerializeField] private AudioSource[] audio = new AudioSource[3];
    public bool boxPush = false;


    // Update
    void Update () {

        if (isControlled){

            // set speed
            if (Input.GetAxisRaw("Sprint") == 1f)
                speed = sprintSpeed;
            else
                speed = walkSpeed;

            // moving
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

            // jump
            if (Input.GetKeyDown("space") || Input.GetKeyDown("w")) {

                animator.SetBool("hasJumped", true);
                jump = true;
            }

            if (Input.GetKeyDown("e")) {

                Meditating();

                for (int i = 0; i < 5; i++) {

                    if (Vector3.Distance(item[i].transform.position, transform.position) < itemPickUpRange && !isCarrying && playerIsInBounds) {
                        
                        if (!item[i].GetComponent<itemController>().playerPickUp && !isCarrying) {

                            item[i].GetComponent<itemController>().playerPickUp = true;
                            isCarrying = true;
                        }
                    }
                    else if (item[i].GetComponent<itemController>().playerPickUp && isCarrying) {

                            item[i].GetComponent<itemController>().playerPickUp = false;
                            isCarrying = false;
                        }
                }
            }
        }

        if (isTriggeredEnd)
            horizontalMove = walkSpeed;

        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (Mathf.Abs(horizontalMove) > 0.01)
            audio[0].volume = 1f;
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

    void OnDrawGizmosSelected () {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, itemPickUpRange);
    }

    public void hasDoneJumping () {

        animator.SetBool("hasJumped", false);
    }

    void Meditating () {

        animator.SetBool("hasMoved", true);
    }
}