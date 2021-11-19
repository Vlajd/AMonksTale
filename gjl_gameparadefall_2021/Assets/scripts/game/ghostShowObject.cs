using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostShowObject : MonoBehaviour
{
    [SerializeField] private GameObject ghost;

    void Start () {

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update () {
        
        if (!ghost.GetComponent<playerGhostMovement>().isControlled)
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        else if (ghost.GetComponent<playerGhostMovement>().isControlled)
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        else
            Debug.Log("ValueNotSet");
    }
}
