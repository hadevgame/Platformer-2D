using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject arrow;
    public Transform firePos;
    private float arrowForce;
    public int arrowSpeed;
    public int timeFire;
    public bool isleft;
    public bool isright;
    void Start()
    {
        InvokeRepeating("FireArrow", 0f, timeFire);
    }

    void Update()
    {
        
    }

    void FireArrow() 
    {
        if (isleft == true && isright == false)
        {
            var arrowInstan = Instantiate(arrow, firePos.transform.position, Quaternion.identity);
            Rigidbody2D rb = arrowInstan.GetComponent<Rigidbody2D>();
            //rb.AddForce(Vector2.left * arrowForce, ForceMode2D.Impulse);
            rb.velocity = new Vector2(-arrowSpeed, 0);
        }
        if (isright == true && isleft == false)
        {
            var arrowInstan = Instantiate(arrow, firePos.transform.position, Quaternion.identity);
            Rigidbody2D rb = arrowInstan.GetComponent<Rigidbody2D>();
            //rb.AddForce(Vector2.left * arrowForce, ForceMode2D.Impulse);
            arrowInstan.transform.rotation = Quaternion.Euler(0, 0, 180);
            rb.velocity = new Vector2(arrowSpeed, 0);
        }
        if(isleft == false && isright == false) 
        {
            var arrowInstan = Instantiate(arrow, firePos.transform.position, Quaternion.identity);
            Rigidbody2D rb = arrowInstan.GetComponent<Rigidbody2D>();
            //rb.AddForce(Vector2.left * arrowForce, ForceMode2D.Impulse);
            arrowInstan.transform.rotation = Quaternion.Euler(0, 0, 90);
            rb.velocity = new Vector2(0, -arrowSpeed);
        }
    }

    
}
