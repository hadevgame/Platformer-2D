using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{
    Health health;
    Animator playerAnim;
    public int damage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            health = collision.GetComponent<Health>();
            playerAnim= collision.GetComponent<Animator>();
            DamagePlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health = null;
            playerAnim = null;
            
        }
    }

    void DamagePlayer()
    {
        if (health != null)
        {
            health.TakeDamage(damage);
            playerAnim.SetTrigger("TakeHit");
        }

    }
}
