using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCameraGetDown : MonoBehaviour
{
    [SerializeField] private float addValue;
    [SerializeField] private GameObject cam;

    void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.CompareTag("MainCamera"))
            cam.GetComponent<playerCamera>().externalOffset += addValue;
    }

    void OnTriggerExit2D (Collider2D other) {

        if (other.gameObject.CompareTag("MainCamera"))
            cam.GetComponent<playerCamera>().externalOffset = 0;
    }
}
