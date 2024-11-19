using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class Golem : MonoBehaviour
{
    public Seeker seeker;
    Path path;
    Coroutine moveCoroutine;
    bool reachDes = false;
    public float moveSpeed;
    public float nextWPDistance = 2f;
    public float maxMoveDistance = 5f;
    public bool isChase = false;
    private Vector3 startPos;
    private bool movetostart;

    private bool isattack = false;
    Health health;
    Animator animator;
    Animator playerAnim;
    public int damage = 5;
    float lastAttackTime = 0f;
    void Start()
    {
        startPos = transform.position;
        InvokeRepeating("CaculatePath", 0f, 0.2f);
        reachDes = true;
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if(isattack == false) 
        {
            CheckAttack();
        }
        if ( Time.time - lastAttackTime >= 5f)
        {
            SetIsAttack();
        }
    }

    void CaculatePath()
    {
        if (movetostart == false) 
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
                /*target = FindStartPos();
                distanceToTarget = Vector2.Distance(transform.position, target);
                if (seeker.IsDone())*/
                seeker.StartPath(transform.position, startPos, OnPathComplete);
            }
        }
        else 
        {
            Vector2 target = FindStartPos();
            float distanceToTarget = Vector2.Distance(transform.position, target);
            if (distanceToTarget <= maxMoveDistance && seeker.IsDone())
                seeker.StartPath(transform.position, target, OnPathComplete);
            
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
            transform.localScale = new Vector3(-1f, 1f, 0);
        else
            transform.localScale = new Vector3(1f, 1f, 0);


        return playerPos;

    }
    Vector2 FindStartPos()
    {
        Vector3 playerPos = startPos;
        Vector3 rotate = playerPos - transform.position;
        if (rotate.x < 0 && rotate.y > 0 || rotate.x < 0 && rotate.y < 0)
            transform.localScale = new Vector3(-1f, 1f, 0);
        else
            transform.localScale = new Vector3(1f, 1f, 0);


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
            Vector2 force = new Vector2( directtion.x,0) * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;
            animator.SetBool("move", true);

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < nextWPDistance)
                currentWP++;
            yield return null;
        }
        reachDes = true;
       
        if (movetostart == true) 
        {
            movetostart = false;
            animator.SetBool("move", false);
            InvokeRepeating("CaculatePath", 0f, 0.2f);
        }
           
    }

    void CheckAttack()
    {
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        float distanceToTarget = Vector2.Distance(transform.position, playerPos);

        if (distanceToTarget < 1f)
        {
            //InvokeRepeating("Attack", 0f, 5f);
            Attack();
            isattack = true;
            lastAttackTime = Time.time;
        }
        else
        {
            
        }
    }

    void SetIsAttack() 
    {
        isattack = false;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            InvokeRepeating("CheckAttack", 0f, 5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            CancelInvoke("CheckAttack");
        }
    }*/
    void Attack() 
    {
        animator.SetTrigger("attack");
        //DamagePlayer();
    }

}
