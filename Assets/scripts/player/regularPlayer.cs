using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RegularPlayer : CharacterController
{
    [SerializeField] private float JumpForce;

    private GroundCheck _groundCheck;

    private void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GroundCheck tempGroundCheck = this.transform.GetChild(i).GetComponent<GroundCheck>();
            if(tempGroundCheck != null)
            {
             _groundCheck = tempGroundCheck;
             break;
            }
        }
    }

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
        if (_groundCheck.IsGrounded) _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpForce);
    }
}
