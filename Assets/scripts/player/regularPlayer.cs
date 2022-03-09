using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RegularPlayer : PlayerCharacterController
{
    [SerializeField] private float JumpForce;
    [SerializeField] private float JumpDistanceToGround = 0.01f;
    [SerializeField] private string[] GroundTags;

    public override void MoveHorizontally(float value, bool sprint)
    {
        if (value == 0.0f) return;

        if (sprint) rigidBody.velocity = new Vector2(value * SprintSpeed, rigidBody.velocity.y);
        else rigidBody.velocity = new Vector2(value * WalkSpeed, rigidBody.velocity.y);
    
        if(value < 0.0f) this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public override void MoveVertically(float value, bool sprint)
    {
    }

    public override void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(cCollider.bounds.center - new Vector3(0.0f, cCollider.bounds.extents.y + 0.01f, 0.0f), Vector2.down, JumpDistanceToGround);

        #if UnityEngine
            Debug.DrawRay(cCollider.bounds.center - new Vector3(0.0f, cCollider.bounds.extents.y + 0.01f, 0.0f), Vector2.down, Color.yellow, 0.2f);
        #endif
        
        if (hit.collider == null) return;

        if (GroundTags.Contains(hit.collider.tag)) rigidBody.velocity = new Vector2(rigidBody.velocity.x, JumpForce);
    }
}
