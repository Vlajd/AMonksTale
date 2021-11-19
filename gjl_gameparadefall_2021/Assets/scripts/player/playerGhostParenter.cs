using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGhostParenter : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isActive = false;
    [SerializeField] private SpriteRenderer activeRender;
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private playerGhostMovement ghostMovement;
    [SerializeField] private float mergeDistance = 1f;
    [SerializeField] private GameObject gameManager;
    private float mainCharacterDistance;
    public int m_NPCCount = 3;
    public GameObject[] m_NPC = new GameObject[5];
    private float m_NPCdist;
    public bool isNPCParent = false;
    public int NPCIndex;


    // Start
    void Start() {

        if (!isActive)
            activeRender.enabled = false;
    }

    // Update
    void Update() {
        
        if (Input.GetKeyDown("r")) {
            
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

            cancelHorizontalMovement();
        }

        if (Input.GetKeyDown("q")) {

            CheckDistanceToPlayer();

            // distance to NPCs
            if (isActive && mainCharacterDistance > mergeDistance && !mainCharacter.GetComponent<playerMovement>().isControlled) {

                for (int i = 0; i < m_NPC.Length; i++) {

                    m_NPCdist = Vector3.Distance(transform.position, m_NPC[i].transform.position);

                    if (m_NPCdist < 1f) {

                        NPCIndex = i;
                        deactivateFromNPC();
                    }
                }
            }
            // deactivate
            else if (isActive && !isNPCParent && mainCharacterDistance < mergeDistance && !m_NPC[NPCIndex].GetComponent<playerNPCMovement>().isControlled) {

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

            cancelHorizontalMovement();
        }

        // keep "parented"
        if(!isActive && !isNPCParent) {

            transform.position = mainCharacter.transform.position;
        }
        else if (!isActive && isNPCParent) {

            transform.position = m_NPC[NPCIndex].transform.position;
        }

        // unparent when npc below camera limit
        if (!isActive && isNPCParent && m_NPC[NPCIndex].transform.position.y < gameManager.GetComponent<gameManager>().minY) {

            gameManager.GetComponent<gameManager>().pressRestartButton();
        }

        // error correction

        if (isActive)
            activeRender.enabled = true;
    }


    // "parent" and "unparent"
    void activateFromMain () {

        // animation
        animator.SetInteger("mergeInt", 1);
        animator.SetBool("isMerging", true);

        activeRender.enabled = true;
        ghostMovement.isControlled = true;
        mainCharacter.GetComponent<playerMovement>().isControlled = false;
        mainCharacter.GetComponent<playerMovement>().horizontalMove = 0;
        isActive = true;
    }

    public void activateFromNPC () {

        // animation
        animator.SetInteger("mergeInt", 1);
        animator.SetBool("isMerging", true);

        activeRender.enabled = true;
        ghostMovement.isControlled = true;
        m_NPC[NPCIndex].GetComponent<playerNPCMovement>().isControlled = false;
        m_NPC[NPCIndex].GetComponent<playerNPCMovement>().horizontalMove = 0;
        isActive = true;
        isNPCParent = false;
    }

    void deactivateFromMain () {

        // animation
        animator.SetInteger("mergeInt", 2);
        animator.SetBool("isMerging", true);

        ghostMovement.isControlled = false;
        mainCharacter.GetComponent<playerMovement>().isControlled = true;
        isActive = false;
        isNPCParent = false;
    }

    void deactivateFromNPC () {

        // animation
        animator.SetInteger("mergeInt", 2);
        animator.SetBool("isMerging", true);

        ghostMovement.isControlled = false;
        m_NPC[NPCIndex].GetComponent<playerNPCMovement>().isControlled = true;
        isActive = false;
        isNPCParent = true;
    }

    void CheckDistanceToPlayer () {

        // distance to player
        mainCharacterDistance = Vector3.Distance(transform.position, mainCharacter.transform.position);
    }

    void cancelHorizontalMovement () {

        mainCharacter.GetComponent<playerMovement>().horizontalMove = 0;
        m_NPC[NPCIndex].GetComponent<playerNPCMovement>().horizontalMove = 0;
        ghostMovement.GetComponent<playerGhostMovement>().horizontalMove = 0;
        ghostMovement.GetComponent<playerGhostMovement>().verticalMove = 0;
    }

    public void animationDone () {

        animator.SetBool("isMerging", false);
    }

    public void animationDoneMerging () {

        animator.SetBool("isMerging", false);
        activeRender.enabled = false;
    }
}
