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
            s_npc = GameObject.FindGameObjectsWithTag("npcPlayer"); 
    } 
  
    void OnTriggerEnter2D (Collider2D other) {

        if (other.tag == "mainPlayer") {
            s_player[0].GetComponent<playerMovement>().walkSpeed -= speedReduction;
            s_player[0].GetComponent<playerMovement>().sprintSpeed -= speedReduction * 2;
        }   
        else if (other.tag == "npcPlayer") {
            
            for (int i = 0; i < ghost.GetComponent<playerGhostParenter>().m_NPC.Length; i++) {
                s_player[i].GetComponent<playerMovement>().walkSpeed -= speedReduction;
                s_player[i].GetComponent<playerMovement>().sprintSpeed -= speedReduction * 2;
            }
        }
    }

    void OnTriggerExit2D (Collider2D other) {

        if (other.tag == "mainPlayer") {
            s_player[0].GetComponent<playerMovement>().walkSpeed += speedReduction;
            s_player[0].GetComponent<playerMovement>().sprintSpeed += speedReduction * 2;
        }
        else if (other.tag == "npcPlayer") {
            
            for (int i = 0; i < ghost.GetComponent<playerGhostParenter>().m_NPC.Length; i++) {
                s_player[i].GetComponent<playerMovement>().walkSpeed += speedReduction;
                s_player[i].GetComponent<playerMovement>().sprintSpeed += speedReduction * 2;
            }
        }
    }
}
