using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelFade : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] spriteRenderer = new SpriteRenderer[4];
    private bool hasEntered = false;
    private Color color = new Color32(0, 0, 0, 2);
    [SerializeField] private GameObject ghost;

    void Start () {

        gameObject.SetActive(true);
    }

    void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.CompareTag("mainPlayer")) {

            hasEntered = true;
            ghost.GetComponent<Collider2D>().isTrigger = true;
        }
        /* else if (other.gameObject.CompareTag("npcPlayer") && !hasEntered) {

            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
        else if (other.gameObject.CompareTag("ghostPlayer") && !other.gameObject.CompareTag("mainPlayer") && !hasEntered) {

            gameObject.GetComponent<Collider2D>().isTrigger = false;
            ghost.GetComponent<Collider2D>().isTrigger = false;
        } */
    }

    void OnTriggerExit2D (Collider2D other) {

        if (other.gameObject.CompareTag("npcPlayer")) {

            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        else if (other.gameObject.CompareTag("ghostPlayer")) {

            gameObject.GetComponent<Collider2D>().isTrigger = true;
            ghost.GetComponent<Collider2D>().isTrigger = true;
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
