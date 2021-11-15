using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPlate : MonoBehaviour
{
    public bool isPressed = false;

    public void OnTriggerEnter2D(Collider2D collider) {

        isPressed = true;
    }

    public void OnTriggerExit2D(Collider2D collider) {

        isPressed = false;
    }
}
