using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelFade : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] spriteRenderer = new SpriteRenderer[4];
    private bool hasEntered = false;
    private Color color = new Color32(0, 0, 0, 2);
    [SerializeField] private GameObject ghost;
    [SerializeField] private GameObject npc;
    private bool ghostHasEntered = false;
    private bool npcHasEntered = false;

    void Start () {

        gameObject.SetActive(true);
    }

    void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.CompareTag("mainPlayer")) {

            hasEntered = true;
            ghost.GetComponent<Collider2D>().isTrigger = true;
        }
        else if (other == npc.GetComponent<Collider2D>() && !hasEntered) {

            npcHasEntered = true;
            Debug.Log("NPC has entered");
            npc.GetComponent<playerNPCMovement>().hasFadeEntered = true;
        }
        else if (other == ghost.GetComponent<Collider2D>() && !hasEntered) {

            ghostHasEntered = true;
            Debug.Log("Ghost has entered");
        }
    }

    void OnTriggerExit2D (Collider2D other) {

        if (other == npc.GetComponent<Collider2D>() && !hasEntered) {

            npcHasEntered = false;
            Debug.Log("NPC has exited");
            npc.GetComponent<playerNPCMovement>().hasFadeEntered = false;
        }
        else if (other == ghost.GetComponent<Collider2D>() && !hasEntered) {

            ghostHasEntered = false;
            Debug.Log("Ghost has exited");
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
        
        if (npcHasEntered && !hasEntered)
            npc.GetComponent<playerNPCMovement>().horizontalMove = npc.GetComponent<playerNPCMovement>().sprintSpeed * -1f;

        if (ghostHasEntered && ghost.GetComponent<playerGhostMovement>().isControlled && !hasEntered)
            ghost.GetComponent<playerGhostMovement>().horizontalMove = ghost.GetComponent<playerGhostMovement>().sprintSpeed * -1f;
    }
}
