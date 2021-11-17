using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGhostParenter : MonoBehaviour
{

    private bool isActive = false;
    [SerializeField] private SpriteRenderer activeRender;
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private playerGhostMovement ghostMovement;
    [SerializeField] private float mergeDistance = 1f;
    private float mainCharacterDistance;
    [SerializeField] private GameObject[] m_NPC = new GameObject[3];
    private float m_NPCdist;
    public bool isNPCParent = false;
    private int NPCIndex;


    // Start
    void Start() {
        
        if (!isActive)
            activeRender.enabled = false;
    }

    // Update
    void Update() {
        
        if (Input.GetKeyDown("e")) {
            
            CheckDistanceToPlayer();

            // toggle
            if (isActive && !isNPCParent && mainCharacterDistance > mergeDistance && ghostMovement.isControlled && !mainCharacter.GetComponent<playerMovement>().isControlled) {

                ghostMovement.isControlled = false;
                mainCharacter.GetComponent<playerMovement>().isControlled = true;
            }
            else if (isActive && !isNPCParent && mainCharacterDistance > mergeDistance && !ghostMovement.isControlled && mainCharacter.GetComponent<playerMovement>().isControlled) {

                ghostMovement.isControlled = true;
                mainCharacter.GetComponent<playerMovement>().isControlled = false;
            }
            // toggle when he is NPC
            else if (!isActive && isNPCParent && m_NPC[NPCIndex].GetComponent<playerNPCMovement>().isControlled && !mainCharacter.GetComponent<playerMovement>().isControlled) {

                m_NPC[NPCIndex].GetComponent<playerNPCMovement>().isControlled = false;
                mainCharacter.GetComponent<playerMovement>().isControlled = true;
            }
            else if (!isActive && isNPCParent && !m_NPC[NPCIndex].GetComponent<playerNPCMovement>().isControlled && mainCharacter.GetComponent<playerMovement>().isControlled) {

                m_NPC[NPCIndex].GetComponent<playerNPCMovement>().isControlled = true;
                mainCharacter.GetComponent<playerMovement>().isControlled = false;
            }
        }

        if (Input.GetKeyDown("q")) {

            CheckDistanceToPlayer();

            // distance to NPCs
            if (isActive && mainCharacterDistance > mergeDistance) {

                for (int i = 0; i < m_NPC.Length; i++) {

                    m_NPCdist = Vector3.Distance(transform.position, m_NPC[i].transform.position);

                    if (m_NPCdist < 1f) {

                        NPCIndex = i;
                        deactivateFromNPC();
                    }
                }
            }
            // deactivate
            else if (isActive && !isNPCParent && mainCharacterDistance < mergeDistance) {

                deactivateFromMain();
            }
            // activate from NPC
            else if (!isActive && isNPCParent && !mainCharacter.GetComponent<playerMovement>().isControlled) {

                activateFromNPC();
            }
            // activate
            else if (!isActive && !isNPCParent) {

                activateFromMain();
            }
        }

        // keep "parented"
        if(!isActive && !isNPCParent) {

            transform.position = mainCharacter.transform.position;
        }
        else if (!isActive && isNPCParent) {

            transform.position = m_NPC[NPCIndex].transform.position;
        }
    }


    // "parent" and "unparent"
    void activateFromMain () {

        activeRender.enabled = true;
        ghostMovement.isControlled = true;
        mainCharacter.GetComponent<playerMovement>().isControlled = false;
        mainCharacter.GetComponent<playerMovement>().horizontalMove = 0;
        isActive = true;
    }

    public void activateFromNPC () {

        activeRender.enabled = true;
        ghostMovement.isControlled = true;
        m_NPC[NPCIndex].GetComponent<playerNPCMovement>().isControlled = false;
        m_NPC[NPCIndex].GetComponent<playerNPCMovement>().horizontalMove = 0;
        isActive = true;
        isNPCParent = false;
    }

    void deactivateFromMain () {

        activeRender.enabled = false;
        ghostMovement.isControlled = false;
        mainCharacter.GetComponent<playerMovement>().isControlled = true;
        isActive = false;
        isNPCParent = false;
    }

    void deactivateFromNPC () {

        activeRender.enabled = false;
        ghostMovement.isControlled = false;
        m_NPC[NPCIndex].GetComponent<playerNPCMovement>().isControlled = true;
        isActive = false;
        isNPCParent = true;
    }

    void CheckDistanceToPlayer () {

        // distance to player
        mainCharacterDistance = Vector3.Distance(transform.position, mainCharacter.transform.position);
    }
}
