using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    public Vector3 moveinput;

    public float movespeed = 3f;

    public float jump = 5f;

    public float rollboost ;

    public float RollTime = 0.25f;

    bool checkroll = false;

    private float checkTime;

    public float attackRange;

    public Transform attackPoint;

    public int damage;

    bool isGrounded = false;

    Animator animator;

    Health health;

    Animator enemyAnim;

    public GameOver gameover;

    public float timeBtwAttack;

    private float timebtwattack;
    //private bool isAttacking = false;
    private int clickcount = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        
    }

    void Update()
    {
        timebtwattack -= Time.deltaTime;
        moveinput.x = Input.GetAxis("Horizontal");
       
        transform.position += moveinput * movespeed * Time.deltaTime;
        animator.SetFloat("speed", moveinput.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            isGrounded = false;
            animator.SetBool("jump", !isGrounded);
            rb.AddForce(new Vector2(0f,jump), ForceMode2D.Impulse );
           // rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if (moveinput.x != 0)
        {
            if (moveinput.x > 0)
                transform.localScale = new Vector3(1f, 1f, 0);
            else
                transform.localScale = new Vector3(-1f, 1f, 0);
        }

        if (Input.GetMouseButtonDown(0) && timebtwattack <=0 )
        {
            if (isGrounded) 
            {
                clickcount++;
                if (clickcount % 2 != 0)
                {
                    Attack();

                }
                else
                {
                    Attack2();

                }
            }
            else
            {
                AirAttack();
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && checkTime <= 0)
        {
            animator.SetBool("roll", true);
            movespeed += rollboost;
            checkTime = RollTime;
            checkroll = true;
            
        }

        if (checkTime <= 0 && checkroll == true)
        {
            animator.SetBool("roll", false);
            movespeed -= rollboost;
            checkroll = false;
            
        }
        else
        {
            checkTime -= Time.deltaTime;
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground") && !isGrounded)
        {
            isGrounded = true;
            animator.SetBool("jump", !isGrounded);
        }

        if (collision.CompareTag("water"))
        {
            Destroy(gameObject);
            gameover.DisplayGameOver();
        }
    }

    void Attack() 
    {
        timebtwattack = timeBtwAttack;
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies) 
        {
            if (enemy.CompareTag("Enemy")) 
            {
                health = enemy.GetComponent<Health>();
                enemyAnim = enemy.GetComponent<Animator>();
                DamageEnemy();
            }
        }
    }

    void Attack2()
    {
        timebtwattack = timeBtwAttack;
        animator.SetTrigger("Attack2");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                health = enemy.GetComponent<Health>();
                enemyAnim = enemy.GetComponent<Animator>();
                DamageEnemy();
            }
        }
    }

    void AirAttack()
    {
        timebtwattack = timeBtwAttack;
        animator.SetTrigger("AirAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                health = enemy.GetComponent<Health>();
                enemyAnim = enemy.GetComponent<Animator>();
                DamageEnemy();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void DamageEnemy()
    {
        if (health != null)
        {
            health.TakeDamageEnemy(damage);
            enemyAnim.SetTrigger("hit");
        }        
    }
}
