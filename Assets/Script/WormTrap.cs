using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormTrap : MonoBehaviour
{
    public float attackrange;
    public Transform attackPoint;
    Animator anim;
    Animator playerAnim;
    Health health;
    public int damage;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            anim.SetTrigger("attack");
            Collider2D[] hitplayer = Physics2D.OverlapCircleAll(attackPoint.position, attackrange);
            foreach (Collider2D player in hitplayer)
            {
                if (player.CompareTag("Player"))
                {
                   
                   health = player.GetComponent<Health>();
                   playerAnim = player.GetComponent<Animator>();
                   //InvokeRepeating("DamagePlayer", 0f, 2f);
                   DamagePlayer();
                    
                }
            }
        }
    }

   /* private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health = null;
            playerAnim = null;
            CancelInvoke("DamagePlayer");
        }
    }*/

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackrange);
    }



    public void DamagePlayer() 
    {
        if (health != null)
        {
            health.TakeDamageEnemy(damage);
            playerAnim.SetTrigger("TakeHit");
        }
    }
}
