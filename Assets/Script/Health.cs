using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxhealth;
    public int curhealth;
    Animator animator;
    public GameOver gameover;
    Animator bossAnim;
    public HealthBar healthBar;
    private bool isDead = false ;

    public GameObject goldPrefab;
    public GameObject heartPrefab;
    
    public int maxGold = 5; 
    public float goldSpeed = 1f;
    void Start()
    {
        animator = GetComponent<Animator>();
        bossAnim = GetComponent<Animator>();
        curhealth = maxhealth;
       
    }

    public int GetHealth() 
    {
        return curhealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead )
            return;

        curhealth -= damage;

        if (curhealth <= 0 && !isDead)
        {
            curhealth = 0;
            isDead = true;
            animator.SetTrigger("dead");
            gameover.DisplayGameOver();
            
        }

        if (healthBar != null)
        {
            healthBar.UpdateBar(curhealth, maxhealth);
        }
    }

    public void TakeDamageEnemy(int damage) 
    {
        curhealth -= damage;
        float random = Random.value;
        if (curhealth <= 0)
        {
            curhealth = 0;
            Destroy(gameObject);
            if(random < 0.5f) 
            {
                DropGold();
            }
            else 
            {
                DropHeart();
            }
        }

        if (healthBar != null)
        {
            healthBar.UpdateBar(curhealth, maxhealth);
            
        }
    }

    public void TakeDamageBoss(int damage)
    {
        curhealth -= damage;
        float random = Random.value;
        if (curhealth <= 0)
        {
            curhealth = 0;
            
            bossAnim.SetTrigger("death");
            
            //Destroy(gameObject);
            if (random < 0.5f)
            {
                DropGold();
            }
            else
            {
                DropHeart();
            }
        }

        if (healthBar != null)
        {
            healthBar.UpdateBar(curhealth, maxhealth);

        }
    }

    public void UpdateHealth(int value) 
    {
        if(curhealth + value > maxhealth) 
        {
            curhealth = maxhealth;
            healthBar.UpdateBar(curhealth, maxhealth);

        }
        else 
        {
            curhealth += value;
            healthBar.UpdateBar(curhealth, maxhealth);
        }
        
    }

    public void UpdateMaxHealth() 
    {
        maxhealth += 5;
        healthBar.UpdateBar(curhealth, maxhealth);
    }

    public void DropGold()
    {
        Vector3 goldPosition1 = transform.position;
        GameObject gold1 = Instantiate(goldPrefab, goldPosition1, Quaternion.identity);
        Rigidbody goldRb1 = gold1.GetComponent<Rigidbody>();
        goldRb1.velocity = new Vector3(0, -1f, 0);

    }

    public void DropHeart()
    {
        Vector3 heartPosition = transform.position;
        GameObject heart = Instantiate(heartPrefab, heartPosition, Quaternion.identity);

    }
}

