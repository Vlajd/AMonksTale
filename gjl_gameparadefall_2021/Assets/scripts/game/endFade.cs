using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endFade : MonoBehaviour
{
    public GameObject sceneManager;

    void OnTriggerEnter2D (Collider2D other) {

        if(other.gameObject.CompareTag("mainPlayer")) {

            Debug.Log("isInside");
            toMain();
            Cursor.lockState = CursorLockMode.None;
        }            
    }

    public void toMain () {

        sceneManager.GetComponent<sceneManager>().toMainMenu();
    }
}
