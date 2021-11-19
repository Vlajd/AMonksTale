using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPuzzleController : MonoBehaviour
{
    [SerializeField] private GameObject[] droppers = new GameObject[5];

    void Update () {

        if (droppers[0].GetComponent<itemDropController>().isRight &&
            droppers[1].GetComponent<itemDropController>().isRight &&
            droppers[2].GetComponent<itemDropController>().isRight &&
            droppers[3].GetComponent<itemDropController>().isRight &&
            droppers[4].GetComponent<itemDropController>().isRight)
            gameObject.GetComponent<Collider2D>().enabled = false;
        else
            gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
