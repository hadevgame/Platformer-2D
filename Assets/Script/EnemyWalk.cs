using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    Rigidbody2D rb;
    Health health;
    public float moveSpeed;
    public int damage = 4;
    public GameObject pointA;
    public GameObject pointB;
    private Transform curPoint;
    private int moveDirection = 1;
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        curPoint = pointA.transform;
    }

    
    void Update()
    {
       Vector2 distance = curPoint.position - transform.position;
        if (curPoint == pointA.transform)
            rb.velocity = new Vector2(-moveSpeed, 0);
        else
            rb.velocity = new Vector2(moveSpeed, 0);

        if(Vector2.Distance(transform.position,curPoint.position) <0.5f && curPoint == pointA.transform)
        {
            curPoint= pointB.transform;
            transform.localScale = new Vector3(-2, 2, 0);
        }
        if (Vector2.Distance(transform.position, curPoint.position) < 0.5f && curPoint == pointB.transform)
        {
            curPoint = pointA.transform;
            transform.localScale = new Vector3(2, 2, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health = collision.GetComponent<Health>();
            
            InvokeRepeating("DamagePlayer", 0f, 5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health = null;
            
            CancelInvoke("DamagePlayer");
        }
    }

    void DamagePlayer()
    {
        if (health != null)
            health.TakeDamage(damage);
    }


}
