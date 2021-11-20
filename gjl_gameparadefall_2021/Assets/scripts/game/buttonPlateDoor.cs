using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Experimental.Rendering.Universal {

public class buttonPlateDoor : MonoBehaviour
{
    [SerializeField] private Collider2D doorCollider;
    public GameObject buttonPlate;
    [SerializeField] private ShadowCaster2D shadow;

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
        shadow.castsShadows = false;
    }

    void closeDoor () {

        doorCollider.enabled = true;
        shadow.castsShadows = false;
    }
}
}