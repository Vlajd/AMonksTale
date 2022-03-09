using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : PlayerCharacterController
{
    public override void MoveHorizontally(float value, bool sprint)
    {
        if (value == 0.0f){
            rigidBody.velocity = new Vector2(0.0f, rigidBody.velocity.y);
            return;
        }

        if (sprint) rigidBody.velocity = new Vector2(value * SprintSpeed, rigidBody.velocity.y);
        else rigidBody.velocity = new Vector2(value * WalkSpeed, rigidBody.velocity.y);
    
        if(value < 0.0f) this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public override void MoveVertically(float value, bool sprint)
    {
        if (value == 0.0f){
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0.0f);
            return;
        }

        if (sprint) rigidBody.velocity = new Vector2(rigidBody.velocity.x, value * SprintSpeed);
        else rigidBody.velocity = new Vector2(rigidBody.velocity.x, value * WalkSpeed);
    }

    public override void Jump()
    {
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere for PossessRadius at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, PossessRadius);
    }
}
