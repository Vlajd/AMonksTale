using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelFade : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] spriteRenderer = new SpriteRenderer[4];
    private bool hasEntered = false;
    private Color color = new Color32(0, 0, 0, 1);

    void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.CompareTag("mainPlayer") || other.gameObject.CompareTag("npcPlayer")) {

            hasEntered = true;
        }
    }

    void Update () {

        if (hasEntered) {

            for (int i = 0; i < spriteRenderer.Length; i++) {

                spriteRenderer[i].color -= color;
                if (spriteRenderer[0].color.a == 0)
                    hasEntered = false;
                    Debug.Log("Alpha of Fade is 0");
            }
        }
    }
}
