using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemController : MonoBehaviour
{
    public int ItemIndex;
    [SerializeField] private GameObject player;
    [SerializeField] private float lerpValue = 3f;
    public bool playerPickUp = false;
    
    void Update () {

        if (playerPickUp && player.GetComponent<playerMovement>().isCarrying)
            gameObject.transform.position = Vector3.Lerp(transform.position, player.transform.position, lerpValue);
    }
}
