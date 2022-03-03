using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [HideInInspector] public Animator DoorAnimator;

    private void Awake()
    {
        DoorAnimator = this.GetComponent<Animator>();
        if (DoorAnimator == null)
        {
            Debug.LogWarning("No Animator Component On Prefab \"Door\"");
        }
    }
}
