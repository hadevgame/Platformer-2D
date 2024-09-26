using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    Animator anim;
    Animator animPlayer;
    Health health;
    public int damage;
    bool isAnim = false;
    public Vector2 attackrange = new Vector2(1f,1f);
    public Transform attackPoint;

    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("DoAnimation", 0, 3f);
        
    }

    public void DoAnimation() 
    {
        anim.SetTrigger("fire");
        Collider2D[] hitplayer = Physics2D.OverlapBoxAll(attackPoint.position, attackrange,0f);
        foreach (Collider2D player in hitplayer)
        {
            if (player.CompareTag("Player"))
            {
                health = player.GetComponent<Health>();
                animPlayer = player.GetComponent<Animator>();
                //InvokeRepeating("DamagePlayer", 0f, 2f);
                DamagePlayer();

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireCube(attackPoint.position, new Vector2(attackrange.x,attackrange.y));
    }

    public void DamagePlayer() 
    {
        if (health != null)
        {
            health.TakeDamage(damage);
            animPlayer.SetTrigger("TakeHit");
        }
    }

 
}
