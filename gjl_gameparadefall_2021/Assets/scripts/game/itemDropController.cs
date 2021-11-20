using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDropController : MonoBehaviour
{
    [SerializeField] private GameObject puzzleController;
    [SerializeField] private GameObject item;
    [SerializeField] private float radius;
    [SerializeField] private GameObject[] a_items = new GameObject[5];
    [SerializeField] private GameObject player;
    private float objDist;
    public bool isRight;

    void Update () {

        // rightBre
        if (Vector3.Distance(item.transform.position, transform.position) < radius)
            isRight = true;
        else
            isRight = false;

        // everyBre
        if (!player.GetComponent<playerMovement>().isCarrying)
            placeInSlot();
    }

    void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void placeInSlot () {

        for (int i = 0; i < 5; i++) {

            objDist = Vector3.Distance(transform.position, a_items[i].transform.position);

            if (objDist < radius) {

                a_items[i].transform.position = transform.position;
            }
        }
    }
}
