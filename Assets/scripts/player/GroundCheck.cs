using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private string[] CollisionTags;

    [HideInInspector] public bool IsGrounded;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollisionTags.Contains(collision.gameObject.tag))
        {
            IsGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CollisionTags.Contains(collision.gameObject.tag))
        {
            IsGrounded = false;
        }
    }
}
