using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemController : MonoBehaviour
{
    public int ItemIndex;
    [SerializeField] private GameObject player;
    public bool playerPickUp = false;

    void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.CompareTag("mainPlayer")) {

            player.GetComponent<playerMovement>().playerIsInBounds = true;
        }
    }

    void OnTriggerExit2D (Collider2D other) {

        if (other.gameObject.CompareTag("mainPlayer")) {

            player.GetComponent<playerMovement>().playerIsInBounds = false;
        }
    }

    void Update () {

        if (playerPickUp && player.GetComponent<playerMovement>().isCarrying)
            gameObject.transform.position = player.transform.position;
    }
}
