using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject gameManager;
    private bool hasTriggered = false;

    void OnTriggerEnter2D (Collider2D other) {

        if (other.tag == "mainPlayer"){

            hasTriggered = true;
            

            fade.SetActive(true);

            fade.GetComponent<Animator>().Play("endFade");
        }
    }

    void Update () {

        if (hasTriggered){
            player.GetComponent<playerMovement>().isTriggeredEnd = true;
            gameManager.GetComponent<AudioSource>().volume -= 0.01f;    
        }
    }
}
