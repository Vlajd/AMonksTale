// THIS IS THE BASE CLASS FOR regularCharacter and ghostCharacter which they are inheriting from
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    public bool MainPlayer;
    public float WalkSpeed;
    public float SprintSpeed;
    public float PossessRadius;

    [HideInInspector] public Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = this.GetComponent<Rigidbody2D>();
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
