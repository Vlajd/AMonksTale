using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnNPCMove : MonoBehaviour
{
    [SerializeField] private GameObject NPC;

    void Update() {
        
        if (NPC.GetComponent<playerNPCMovement>().horizontalMove > 0)
            Destroy(gameObject);
    }
}
