using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDropController : MonoBehaviour
{
    [SerializeField] private GameObject puzzleController;
    [SerializeField] private GameObject item;
    [SerializeField] private float radius;
    public bool isRight;

    void Update () {

        if (Vector3.Distance(item.transform.position, transform.position) < radius)
            isRight = true;
        else
            isRight = false;
    }

    void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
