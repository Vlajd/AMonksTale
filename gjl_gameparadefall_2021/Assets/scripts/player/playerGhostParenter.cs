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


    // Start
    void Start() {
        
        if (!isActive)
            activeRender.enabled = false;        
    }

    // Update
    void Update() {
        
        if (Input.GetKeyDown("e")) {
            
            mainCharacterDistance = Vector3.Distance(transform.position, mainCharacter.transform.position);

            // activate
            if (!isActive) {

                activeRender.enabled = true;
                ghostMovement.isControlled = true;
                mainCharacter.GetComponent<playerMovement>().isControlled = false;
                isActive = true;
            }
            // toggle
            else if (isActive && mainCharacterDistance > mergeDistance && ghostMovement.isControlled && !mainCharacter.GetComponent<playerMovement>().isControlled) {

                ghostMovement.isControlled = false;
                mainCharacter.GetComponent<playerMovement>().isControlled = true;
            }
            else if (isActive && mainCharacterDistance > mergeDistance && !ghostMovement.isControlled && mainCharacter.GetComponent<playerMovement>().isControlled) {

                ghostMovement.isControlled = true;
                mainCharacter.GetComponent<playerMovement>().isControlled = false;
            }
            // deactivate
            else if (isActive && mainCharacterDistance < mergeDistance) {

                activeRender.enabled = false;
                ghostMovement.isControlled = false;
                mainCharacter.GetComponent<playerMovement>().isControlled = true;
                isActive = false;
            }
        }
        // keep "parented"
        else if(!isActive) {

            transform.position = mainCharacter.transform.position;
        }
    }
}
