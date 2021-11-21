using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxController : MonoBehaviour
{
    [SerializeField] private float speedReduction = 10f;
    private GameObject[] s_player;
    private GameObject[] s_npc;
    [SerializeField] private GameObject ghost;

    void Start () {

        if (s_player == null)
            s_player = GameObject.FindGameObjectsWithTag("mainPlayer");

        if (s_npc == null)
            s_npc = new GameObject[ghost.GetComponent<playerGhostParenter>().m_NPCCount];
            s_npc = GameObject.FindGameObjectsWithTag("npcPlayer"); 
    } 
  
    void OnTriggerEnter2D (Collider2D other) {

        if (other.tag == "mainPlayer") {
            s_player[0].GetComponent<playerMovement>().walkSpeed -= speedReduction;
            s_player[0].GetComponent<playerMovement>().sprintSpeed -= speedReduction * 2;
            s_player[0].GetComponent<playerMovement>().boxPush = true;
            s_player[0].GetComponent<Animator>().SetBool("isBoxPushed", true);
        }   
        else if (other.tag == "npcPlayer") {
        
            for (int i = 0; i < ghost.GetComponent<playerGhostParenter>().m_NPCCount; i++) {
                s_npc[i].GetComponent<playerNPCMovement>().walkSpeed -= speedReduction;
                s_npc[i].GetComponent<playerNPCMovement>().sprintSpeed -= speedReduction * 2;
                s_npc[i].GetComponent<playerNPCMovement>().boxPush = true;
                s_npc[i].GetComponent<Animator>().SetBool("isBoxPushed", true);

            }
        }
    }

    void OnTriggerExit2D (Collider2D other) {

        if (other.tag == "mainPlayer") {
            s_player[0].GetComponent<playerMovement>().walkSpeed += speedReduction;
            s_player[0].GetComponent<playerMovement>().sprintSpeed += speedReduction * 2;
            s_player[0].GetComponent<playerMovement>().boxPush = false;
            s_player[0].GetComponent<Animator>().SetBool("isBoxPushed", false);
        }
        else if (other.tag == "npcPlayer") {
        
            for (int i = 0; i < ghost.GetComponent<playerGhostParenter>().m_NPCCount; i++) {
                s_npc[i].GetComponent<playerNPCMovement>().walkSpeed += speedReduction;
                s_npc[i].GetComponent<playerNPCMovement>().sprintSpeed += speedReduction * 2;
                s_npc[i].GetComponent<playerNPCMovement>().boxPush = false;
                s_npc[i].GetComponent<Animator>().SetBool("isBoxPushed", false);

            }
        }
    }
}
