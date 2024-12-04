using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage;
    Health playerHealth;
    Animator playerAnim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            playerAnim = collision.GetComponent<Animator>();
            playerHealth = playerAnim.GetComponent<Health>();
            DamagePlayer();
        }
        if (collision.CompareTag("ground") || collision.CompareTag("water")) 
        {
            Destroy(this.gameObject);
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerAnim = null;
            playerHealth = null;
           
        }
    }*/

    void DamagePlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            playerAnim.SetTrigger("TakeHit");
            Destroy(this.gameObject);
        }

    }
}
