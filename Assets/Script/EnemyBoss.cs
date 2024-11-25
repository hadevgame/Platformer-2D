using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Pathfinding;

public class EnemyBoss : MonoBehaviour
{
    Rigidbody2D rb;
    Health health;
    public float moveSpeed;
    Animator playerAnim;
    public Animator bossAnim;
    private Vector3 startPos;
    public GameObject imghealthbar;
    public float timeBtwAttack = 2f;
    private float timebtwattack;
    private int healthboss;
    private int countHealth = 0;
    
    void Start()
    {
        health= GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        bossAnim= GetComponent<Animator>();
        
        startPos = transform.position;
        InvokeRepeating("CheckAttack", 0f, 3f);
        
    }

    
    void Update()
    {
        DisplayName();
        CheckHealth();
         
    }

    void CheckHealth() 
    {
        if (health != null)
        {
            healthboss = health.GetHealth();
            if (healthboss <= 50 && countHealth == 0)
            {
                countHealth = 1;
                
                bossAnim.SetTrigger("flyup");
                StartCoroutine(HealOverTime());
               
            }
            if (healthboss == 0)
                imghealthbar.SetActive(false);
            else return;
        }
        else Debug.Log("health null");
        
    }

    IEnumerator HealOverTime()
    {
        for (int i = 0; i < 3; i++)
        {
            health.UpdateHealth(10);
            yield return new WaitForSeconds(2f);
        }
        
        bossAnim.SetTrigger("flydown");
    }
    void DisplayName() 
    {
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        float distanceToTarget = Vector2.Distance(transform.position, playerPos);
        Vector3 rotate = playerPos - transform.position;
        
        
        if (distanceToTarget < 3f) 
        {
            imghealthbar.SetActive(true);
            if (rotate.x < 0)
                transform.localScale = new Vector3(-1f, 1f, 0);
            else
                transform.localScale = new Vector3(1f, 1f, 0);
        }
        else
        {
            imghealthbar.SetActive(false);
        }
    }

    void CheckAttack()
    {
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        float distanceToTarget = Vector2.Distance(transform.position, playerPos);
       
        if (distanceToTarget < 3f)
        {
            InvokeRepeating("Attack", 0f, 3f);
        }
        else
        {
            CancelInvoke("Attack");
        }
    }

    void Attack() 
    {
        float random = Random.Range(0, 10);
        if(random < 5f) 
        {
            bossAnim.SetTrigger("attack2");

        }
        else 
        {
            
            bossAnim.SetTrigger("attack1");
        }
    }
    

    
}
