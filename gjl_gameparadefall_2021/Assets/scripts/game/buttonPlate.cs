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
    [SerializeField] private GameObject[] releaseItems;
    private int releaseItemsLength;
    private bool isStillInside = false;

    void Start () {

        originalY = transform.position.y;

        if (releaseItems != null)
            releaseItemsLength = releaseItems.Length; 
    }

    void FixedUpdate () {

        isStillInside = false;
    }

    void OnTriggerStay2D (Collider2D col) {

        isStillInside = true;
    }

    public void OnTriggerEnter2D(Collider2D col) {

        if (!col.isTrigger) {

            references ++;
            //buttonPress();
        }
    }

    public void OnTriggerExit2D(Collider2D col) {

        if (!col.isTrigger) {
            
            references --;
            isStillInside = false;
            //buttonRelease(); 
        }
    }

    public void Update() {

        /*for (int i = 0; i < releaseItemsLength; i++) {

            if (Vector3.Distance(transform.position, releaseItems[i].transform.position) > 3f) {
            
                isPressed = false;
            }
            else {

                i = releaseItemsLength;
                isPressed = true;
            }
        }*/

        if (references > 0 && isStillInside) {

            buttonPress();
            isPressed = true;
        }
        else if (references <= 0 && !isStillInside) {

            buttonRelease();
            isPressed = false;
        }

    }

    void buttonPress () {

        if (!isPressed && references > 0){

            transform.position = new Vector3(transform.position.x, transform.position.y - yValue, transform.position.z);
            doorAnim.SetBool("isOpen", true);
        }
    }

    void buttonRelease () {

        if (isPressed && references <= 0) {

            transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
            doorAnim.SetBool("isOpen", false);
        }   
    }
}
