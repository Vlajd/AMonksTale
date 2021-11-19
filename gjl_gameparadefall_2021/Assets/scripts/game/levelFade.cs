using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelFade : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] spriteRenderer = new SpriteRenderer[4];
    [SerializeField] private GameObject ghost;
    [SerializeField] private GameObject[] npc = new GameObject[10];
    private bool hasEntered = false;
    private bool ghostEnter;
    private Color color = new Color32(0, 0, 0, 1);

    void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.CompareTag("mainPlayer")) {

            hasEntered = true;
        }

        if (other.gameObject.CompareTag("ghostPlayer") || other.gameObject.CompareTag("npcPlayer")) {

            ghostEnter = true;
        }
    }

    void OnTriggerExit2D (Collider2D other) {

        if (other.gameObject.CompareTag("ghostPlayer") || other.gameObject.CompareTag("npcPlayer")) {

            ghostEnter = false;
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

        if (ghostEnter && !hasEntered && ghost.GetComponent<playerGhostMovement>().isControlled && ghost.GetComponent<playerGhostMovement>().horizontalMove > 0) {

            ghost.GetComponent<playerGhostMovement>().horizontalMove = 0;
        }
        else if (ghostEnter && npc[ghost.GetComponent<playerGhostParenter>().NPCIndex].GetComponent<playerNPCMovement>().horizontalMove > 0 && !hasEntered && npc[ghost.GetComponent<playerGhostParenter>().NPCIndex].GetComponent<playerNPCMovement>().isControlled) {

            npc[ghost.GetComponent<playerGhostParenter>().NPCIndex].GetComponent<playerNPCMovement>().horizontalMove = 0;
        }
    }
}
