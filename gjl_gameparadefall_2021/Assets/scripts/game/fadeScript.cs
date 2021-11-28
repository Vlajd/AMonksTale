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

        if (hasTriggered) {
            player.GetComponent<playerMovement>().isTriggeredEnd = true;
            player.GetComponent<playerMovement>().s_audio[0].volume -= 0.2f * Time.deltaTime;
            gameManager.GetComponent<gameManager>().g_audio[0].volume -= 0.1f * Time.deltaTime;
            gameManager.GetComponent<gameManager>().g_audio[1].volume -= 0.1f * Time.deltaTime;    
        }
    }
}
