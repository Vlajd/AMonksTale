using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPlate : MonoBehaviour
{
    public bool isPressed = false;
    public int references = 0;

    public void OnTriggerEnter2D(Collider2D col) {

        references = references + 1;
    }

    public void OnTriggerExit2D(Collider2D col) {

        references = references - 1;
    }

    public void Update() {

        if (references > 0) {

            isPressed = true;
        }
        else {

            isPressed = false;
        }
    }
}
