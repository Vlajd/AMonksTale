using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostShowMesh : MonoBehaviour
{
    [SerializeField] private GameObject ghost;

    void Start () {

        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    void Update () {
        
        if (!ghost.GetComponent<playerGhostMovement>().isControlled)
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        else if (ghost.GetComponent<playerGhostMovement>().isControlled)
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        else
            Debug.Log("ValueNotSet");
    }
}
