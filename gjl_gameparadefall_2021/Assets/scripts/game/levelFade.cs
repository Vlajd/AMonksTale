using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelFade : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] spriteRenderer = new SpriteRenderer[4];
    private bool hasEntered = false;
    private Color color = new Color32(0, 0, 0, 2);

    void Start () {

        gameObject.SetActive(true);
    }

    void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.CompareTag("mainPlayer")) {

            hasEntered = true;
        }
    }

    void Update () {

        if (hasEntered) {

            for (int i = 0; i < spriteRenderer.Length; i++) {

                spriteRenderer[i].color -= color;
                if (spriteRenderer[0].color.a == 0)
                    hasEntered = false;
            }
        }
    }
}
