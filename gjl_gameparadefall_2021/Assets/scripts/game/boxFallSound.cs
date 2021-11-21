using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxFallSound : MonoBehaviour
{
    public cameraShake cameraShake;
    [SerializeField] private float camShakeDuration = 0.5f;
    [SerializeField] private float camShakeMagnitude = 0.4f;
    private bool hasPlayed = false;

    void OnTriggerEnter2D (Collider2D other) {

        if(other.tag == "box" && !hasPlayed) {

            gameObject.GetComponent<AudioSource>().Play();
            hasPlayed = true;
            StartCoroutine(cameraShake.Shake(camShakeDuration, camShakeMagnitude));
        }
    }
}
