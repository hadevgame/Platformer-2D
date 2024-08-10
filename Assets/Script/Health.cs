using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxhealth;
    public int curhealth;
    Animator animator;
   
    public HealthBar healthBar;
    private bool isDead = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        
        curhealth = maxhealth;
       /* if (healthBar != null)
        {
            healthBar.UpdateBar(curhealth, maxhealth);
        }*/
    }

    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        if (isDead )
            return;

        curhealth -= damage;

        if (curhealth <= 0)
        {
            curhealth = 0;
            
            animator.SetTrigger("dead");
            // Destroy(gameObject);
            isDead= true;
        }

        if (healthBar != null)
        {
            healthBar.UpdateBar(curhealth, maxhealth);
        }
    }

    public void TakeDamageEnemy(int damage) 
    {
        curhealth -= damage;
        if (curhealth <= 0)
        {
            curhealth = 0;  
            Destroy(gameObject);
           
        }

        if (healthBar != null)
        {
            healthBar.UpdateBar(curhealth, maxhealth);
            
        }
    }
}
