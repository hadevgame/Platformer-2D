using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrap : MonoBehaviour
{
    Rigidbody2D rb;
    public int force;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("push");
            rb = collision.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.left * force, ForceMode2D.Impulse);
            
        }
        
    }
}
