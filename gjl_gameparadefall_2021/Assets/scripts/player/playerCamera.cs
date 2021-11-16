using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private GameObject ghostCharacter;
    [SerializeField] private float smoothing;
    [SerializeField] private float lowestY;
    private Vector3 offset;
    private Vector3 wantedCamPos;

    void Start () {

        offset = transform.position - mainCharacter.transform.position;
        lowestY += transform.position.y;
    }

    // Fixed Update
    void FixedUpdate () {

        // check if main is controlled
        if (mainCharacter.GetComponent<playerMovement>().isControlled)
            wantedCamPos = mainCharacter.transform.position + offset;

        // check if ghost is controlled
        else if (ghostCharacter.GetComponent<playerGhostMovement>().isControlled || ghostCharacter.GetComponent<playerGhostParenter>().isNPCParent)
            wantedCamPos = ghostCharacter.transform.position + offset;

        transform.position = Vector3.Lerp(transform.position, wantedCamPos, smoothing * Time.deltaTime);

        // stop if reached minimal limit
        if (transform.position.y < lowestY) {

            transform.position = new Vector3(transform.position.x, lowestY, transform.position.z);
        }
    }
}
