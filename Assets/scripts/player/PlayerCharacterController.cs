// THIS IS THE BASE CLASS FOR regularCharacter and ghostCharacter which they are inheriting from
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCharacterController : MonoBehaviour
{
    public bool MainPlayer;
    public float WalkSpeed;
    public float SprintSpeed;
    public float PossessRadius;

    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public Collider2D cCollider;

    private void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        cCollider = this.GetComponent<Collider2D>();

        if(rigidBody == null) Debug.LogWarning("No Rigidbody Found On", this);
        if(cCollider == null) Debug.LogWarning("No cCollider Found On", this);
    }

    // Implement in regularCharacter.cs and ghostCharacter.cs
    public abstract void MoveHorizontally(float value, bool sprint);
    public abstract void MoveVertically(float value, bool sprint);
    public abstract void Jump();

    public void SwitchDir(float value)
    {
        if(value < 0.0f) this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
