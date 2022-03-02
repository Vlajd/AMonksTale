using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : CharacterController
{
    public override void MoveHorizontally(float value, bool sprint)
    {
        if (value == 0.0f){
            _rigidBody.velocity = new Vector2(0.0f, _rigidBody.velocity.y);
            return;
        }

        if (sprint) _rigidBody.velocity = new Vector2(value * SprintSpeed, _rigidBody.velocity.y);
        else _rigidBody.velocity = new Vector2(value * WalkSpeed, _rigidBody.velocity.y);
    
        if(value < 0.0f) this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public override void MoveVertically(float value, bool sprint)
    {
        if (value == 0.0f){
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0.0f);
            return;
        }

        if (sprint) _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, value * SprintSpeed);
        else _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, value * WalkSpeed);
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
