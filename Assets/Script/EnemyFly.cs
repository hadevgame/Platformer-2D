﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class EnemyFly : MonoBehaviour
{
    public Seeker seeker;
    Path path;
    Coroutine moveCoroutine;
    bool reachDes = false;
    public float moveSpeed;
    public float nextWPDistance = 2f;
    public float maxMoveDistance = 5f;
    public bool isChase= false;
    private Vector3 startPos;
    private bool movetostart;
    Health health;
   
    Animator animator;
    public int damage = 5;

    void Start()
     {
         startPos = transform.position;
         InvokeRepeating("CaculatePath", 0f, 0.2f);
         reachDes = true;
         animator = GetComponent<Animator>();
     }

     void Update()
     {

     }

    void CaculatePath()
    {
        Vector2 target = FindTarget();
        float distanceToTarget = Vector2.Distance(transform.position, target);
         if (distanceToTarget <= maxMoveDistance && seeker.IsDone())
             seeker.StartPath(transform.position, target, OnPathComplete);

       float distancemove = Vector2.Distance(transform.position, startPos);
        if (distancemove > maxMoveDistance)
        {
            CancelInvoke("Calculatepath");
            movetostart = true;
            seeker.CancelCurrentPathRequest();
            seeker.StartPath(transform.position, startPos, OnPathComplete);
        }
    }
    
    void OnPathComplete(Path p)
     {
         if (p.error) return;
         path = p;
         MoveToTarget();
       
    }

     Vector2 FindTarget()
     {
         Vector3 playerPos = FindObjectOfType<Player>().transform.position;
         Vector3 rotate = playerPos - transform.position;
         if (rotate.x < 0 && rotate.y > 0 || rotate.x < 0 && rotate.y < 0)
             transform.localScale = new Vector3(1.8f, 1.6f, 0);
         else
             transform.localScale = new Vector3(-1.8f, 1.6f, 0);


         return playerPos;

     }

     void MoveToTarget()
     {
         if (moveCoroutine != null) StopCoroutine(moveCoroutine);
         moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
     }

    IEnumerator MoveToTargetCoroutine()
      {
          int currentWP = 0;
          reachDes = false;
          while (currentWP < path.vectorPath.Count)
          {
              Vector2 directtion = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized; //tính hướng di chuyển từ vị trí hiện tại tới điểm trên đường đi
              Vector2 force = directtion * moveSpeed * Time.deltaTime;
              transform.position += (Vector3)force;

              float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
              if (distance < nextWPDistance)
                  currentWP++;
              yield return null;
          }
          reachDes = true;
          if(movetostart ==true) InvokeRepeating("CaculatePath", 0f, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health = collision.GetComponent<Health>();
            animator.SetBool("attack", true);
            InvokeRepeating("DamagePlayer", 0f, 2f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health = null;
            animator.SetBool("attack", false);
            CancelInvoke("DamagePlayer");
        }
    }

    void DamagePlayer()
    {
        if (health != null)
            health.TakeDamage(damage);
    }
}