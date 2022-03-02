using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private string[] CollisionTags; 

    [HideInInspector] public bool _isGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollisionTags.Contains(collision.gameObject.tag))
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CollisionTags.Contains(collision.gameObject.tag))
        {
            _isGrounded = false;
        }
    }
}
