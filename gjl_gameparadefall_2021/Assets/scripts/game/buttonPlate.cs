using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPlate : MonoBehaviour
{
    public bool isPressed = false;
    public int references = 0;
    [SerializeField] private float yValue = 0;
    private float originalY;
    public Animator doorAnim;

    void Start () {

        originalY = transform.position.y;
    }

    public void OnTriggerEnter2D(Collider2D col) {

        if (!col.isTrigger) {

            references ++;
            buttonPress();
        }
    }

    public void OnTriggerExit2D(Collider2D col) {

        if (!col.isTrigger) {
            
            references --;
            buttonRelease();
        }
    }

    public void Update() {

        if (references > 0) {

            isPressed = true;
        }
        else if (references <= 0) {

            isPressed = false;
        }
        
    }

    void buttonPress () {

        if (!isPressed && references > 0){

            transform.position = new Vector3(transform.position.x, transform.position.y - yValue, transform.position.z);
            doorAnim.Play("anim_DoorOpen");
        }
    }

    void buttonRelease () {

        if (isPressed && references <= 0) {

            transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
            doorAnim.Play("anim_DoorClose");
        }   
    }
}
