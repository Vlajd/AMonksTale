// only using the basics, damn...
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Damn, soo many objects...
    public GameObject playerPos;
    public GameObject ghostPos;
    public GameObject cam;
    public playerController controller;
    public float walkSpeed = 40f;
    public float sprintSpeed = 60f;
    public float characterSmoothing;
    public float ghostSpeedDivide = 3.0f;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private float dist;
    private bool isJump = false;
    private bool isCrouch = false;
    public bool isControlled;
    private int checkFlipCount;
    [SerializeField] private bool isGhost;
    [SerializeField] private bool isNPC;
    [SerializeField] private Collider2D ghostEnableCollider;
    [SerializeField] private Rigidbody2D ghostEnableRigidBody;
    [SerializeField] private SpriteRenderer ghostRender;
    int isParented = 0;

    void Start() {

        if (isGhost == true) {

            ghostEnableRigidBody.isKinematic = true;
            ghostRender.enabled = false;
        }
        else {
            ghostEnableRigidBody.isKinematic = false;
        }

        // Checks every few seconds divided by idk how much for input on E
        InvokeRepeating("toggle", cam.GetComponent<playerCamera>().toggleValue, 1f);
    }

    void Update() {
        
        // see if mate is controlled before giving him controlls
        if (isControlled == true) {
            checkInputs();
        }
        // cancel every motion if mate doesn't have controlls
        else {
            horizontalMove = 0;
            verticalMove = 0;
            isJump = false;
            isCrouch = false;
        }

        // see if ghost is close enough to player to merge
        dist = Vector3.Distance(playerPos.transform.position, ghostPos.transform.position);

        if (isParented == 0 && isGhost == true) {

            ghostPos.transform.position = playerPos.transform.position;
        }
    }

    // Most of Inputs are over here
    void checkInputs() {

        // sprint   left shift
        if (Input.GetAxisRaw("Sprint") == 1) {

            if (isGhost == true) {

                horizontalMove = Input.GetAxisRaw("Horizontal") * sprintSpeed / ghostSpeedDivide;
                verticalMove = Input.GetAxisRaw("Vertical") * sprintSpeed;
            }
            else {

                horizontalMove = Input.GetAxisRaw("Horizontal") * sprintSpeed;
                verticalMove = Input.GetAxisRaw("Vertical") * sprintSpeed;
            }
        }
        else {

            horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;
        }

        // jump     spacebar or W
        if (Input.GetAxisRaw("Vertical") >= 0.5f || Input.GetAxisRaw("Jump") == 1) {

            if (isGhost == true) {
                verticalMove = Input.GetAxisRaw("Vertical") * walkSpeed;
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0f, verticalMove * Time.fixedDeltaTime, 0f), characterSmoothing * Time.fixedDeltaTime);
            }
            
            isJump = true;
        }
        // crouch   left ctrl or S
        else if (Input.GetAxisRaw("Vertical") <= -0.5f || Input.GetAxisRaw("Crouch") == 1) {

            if (isGhost == true) {
                if (transform.position.y >= cam.GetComponent<playerCamera>().lowestY) {
                    verticalMove = Input.GetAxisRaw("Vertical") * walkSpeed;
                    transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0f, verticalMove * Time.fixedDeltaTime, 0f), characterSmoothing * Time.fixedDeltaTime);
                }
            }
            else {
                isCrouch = true;
            }
        }
        // resets Vertical Input
        else if (Input.GetAxisRaw("Vertical") == 0) {

            isCrouch = false;
        }
    }

    // toggles the states of the character, ghost and npc
    void toggle() {

        // Input is E
        if (Input.GetAxisRaw("Toggle") * Time.fixedDeltaTime > 0.0f && Input.GetAxisRaw("Toggle") * Time.fixedDeltaTime <= 3.0f) {

            // Parent Ghost
            if (dist < 1.0f) {
                if (isParented < 1 && isGhost == true) {

                    ghostEnableCollider.enabled = true;
                    ghostEnableRigidBody.isKinematic = false;
                    transform.parent = null;

                    ghostRender.enabled = true;

                    isParented++;

                    // Debug Flip Behavoiur
                    checkFlipCount = controller.flipCount;
                }
                else if (isParented > 0 && isGhost == true) {

                    ghostPos.transform.SetParent(playerPos.transform);
                    ghostRender.enabled = false;

                    isControlled = false;
                    
                    isParented = 0;

                    ghostPos.transform.position = playerPos.transform.position;
                }

                if (isGhost == false) {

                    isControlled = true;
                }
            }
            // switch users
            else if (dist > 1.0f) {
                
                if (isControlled == true) {
                    isControlled = false;
                }
                else if (isControlled == false) {
                    isControlled = true;
                }
            }
        }
    }

    // just inverts the states
    public void hardToggle() {

        if (isControlled == true) {
            isControlled = false;
        }
        else if (isControlled == false) {
            isControlled = true;
        }
    }

    // applies the data
    void FixedUpdate() {

        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouch, isJump);
        isJump = false;
    }
}