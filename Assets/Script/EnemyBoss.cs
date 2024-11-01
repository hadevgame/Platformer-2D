using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class EnemyBoss : MonoBehaviour
{
    Rigidbody2D rb;
    Health health;
    public float moveSpeed;
    public int damage = 4;
    public GameObject pointA;
    public GameObject pointB;
    private Transform curPoint;
    Animator playerAnim;
    public Animator bossAnim;
    private Vector3 startPos;
    public GameObject imghealthbar;
    public float timeBtwAttack = 2f;
    private float timebtwattack;

    
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        bossAnim= GetComponent<Animator>();
        curPoint = pointA.transform;
        //bossAnim.SetTrigger("walk");
        startPos = transform.position;
        InvokeRepeating("CheckAttack", 0f, 3f);

    }

    
    void Update()
    {
        DisplayName();
       
        /* Vector2 distance = curPoint.position - transform.position;
         if (curPoint == pointA.transform) 
         {
             rb.velocity = new Vector2(-moveSpeed, 0);

         }
         else 
         {
             rb.velocity = new Vector2(moveSpeed, 0);

         }


         if (Vector2.Distance(transform.position, curPoint.position) < 0.5f && curPoint == pointA.transform)
         {
             curPoint = pointB.transform;
             transform.localScale = new Vector3(1, 1, 0);

         }
         if (Vector2.Distance(transform.position, curPoint.position) < 0.5f && curPoint == pointB.transform)
         {
             curPoint = pointA.transform;
             transform.localScale = new Vector3(-1, 1, 0);

         }*/
    }

    void DisplayName() 
    {
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        float distanceToTarget = Vector2.Distance(transform.position, playerPos);
        Vector3 rotate = playerPos - transform.position;
        if (rotate.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 0);
        else
            transform.localScale = new Vector3(1f, 1f, 0);
        if (distanceToTarget < 3f) 
        {
            imghealthbar.SetActive(true);
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
        float random = Random.Range(0, 1);
        if(random < 0.5f) 
        {
            bossAnim.SetTrigger("attack1");

        }
        else 
        {
            bossAnim.SetTrigger("attack2");
        }
    }
    
}
