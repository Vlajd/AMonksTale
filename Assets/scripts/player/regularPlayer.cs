using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regularPlayer : characterController
{
    [SerializeField] private float JumpForce;

    public override void MoveHorizontally(float value, bool sprint)
    {
        if (value == 0.0f) return;

        if (sprint) _rigidBody.velocity = new Vector2(value * SprintSpeed, _rigidBody.velocity.y);
        else _rigidBody.velocity = new Vector2(value * WalkSpeed, _rigidBody.velocity.y);
    
        if(value < 0.0f) this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public override void MoveVertically(float value, bool sprint)
    {
    }

    public override void Jump()
    {
        if (_isGrounded) _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpForce);
    }
}
