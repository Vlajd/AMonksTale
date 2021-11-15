using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPlateDoor : MonoBehaviour
{
    [SerializeField] private Collider2D doorCollider;
    public GameObject buttonPlate;

    void Update () {

        if (buttonPlate.GetComponent<buttonPlate>().isPressed == true) {

            openDoor();
        }
        else {

            closeDoor();
        }
    }

    void openDoor () {

        doorCollider.enabled = false;
    }

    void closeDoor () {

        doorCollider.enabled = true;
    }
}
