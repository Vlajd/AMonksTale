using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    public Transform[] playerTarget;
    public GameObject playerBool;
    public float smoothing;
    private Vector3 offset;
    public float lowestY;
    Vector3 targetCamPos;
    float i;
    public float toggleValue;

    void Start () {

        offset = transform.position - playerTarget[0].position;

        lowestY = lowestY + transform.position.y;
    }

    void FixedUpdate () {
        
        if (playerBool.GetComponent<playerMovement>().isControlled == true) {
            targetCamPos = playerTarget[0].position + offset;
        }
        else {
            targetCamPos = playerTarget[1].position + offset;
        } 

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        if (transform.position.y < lowestY) {

            transform.position = new Vector3(transform.position.x, lowestY, transform.position.z);

            if (playerTarget[0].position.y < lowestY && playerTarget[1].position.y > lowestY) {

                playerBool.GetComponent<playerMovement>().hardToggle();
            }
        }
    }
}
