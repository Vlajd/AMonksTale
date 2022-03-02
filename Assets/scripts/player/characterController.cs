// THIS IS THE BASE CLASS FOR regularCharacter and ghostCharacter which they are inheriting from
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class characterController : MonoBehaviour
{
    public bool MainPlayer;
    public float WalkSpeed;
    public float SprintSpeed;
    [SerializeField] private string[] CollisionTags; 

    [HideInInspector] public bool _isGrounded = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollisionTags.Contains(collision.gameObject.tag))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CollisionTags.Contains(collision.gameObject.tag))
        {
            _isGrounded = false;
        }
    }
}
