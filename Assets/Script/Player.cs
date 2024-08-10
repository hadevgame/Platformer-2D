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
    public float attackRange;
    public Transform attackPoint;
    public int damage;
    bool isGrounded = false;
    Animator animator;
    Health health;
    Animator enemyAnim;
   
    public float timeBtwAttack;
    private float timebtwattack;
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
                transform.localScale = new Vector3(1.7f, 1.7f, 0);
            else
                transform.localScale = new Vector3(-1.7f, 1.7f, 0);
        }

        if (Input.GetMouseButtonDown(0) && timebtwattack <=0)
        {
            Attack();
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
