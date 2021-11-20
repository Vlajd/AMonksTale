using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundParallax : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float ogCamPos;
    [SerializeField] private float offset;

    void Start () {

        ogCamPos = cam.transform.position.x;
    }

    void Update () {

        float camX = cam.transform.position.x - ogCamPos;

        transform.position = Vector3.Lerp(transform.position, new Vector3(cam.transform.position.x - camX * parallaxEffect + offset, transform.position.y, transform.position.z), 3f);
    }
}
