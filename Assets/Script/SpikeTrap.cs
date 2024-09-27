using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    Health health;
    Animator anim;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health = collision.GetComponent<Health>();
            anim = collision.GetComponent<Animator>();
            InvokeRepeating("DamagePlayer", 0, 3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health = null;
            anim = null;
        }
    }

    public void DamagePlayer() 
    {
        if(health!= null) 
        {
            health.TakeDamage(damage);
            anim.SetTrigger("TakeHit");
        }
    }
}
