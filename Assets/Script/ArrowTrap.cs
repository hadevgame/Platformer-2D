using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject arrow;
    public Transform firePos;
    private float arrowForce;
    public int arrowSpeed;
    void Start()
    {
        InvokeRepeating("FireArrow", 0f, 3f);
    }

    void Update()
    {
        
    }

    void FireArrow() 
    {
        var arrowInstan = Instantiate(arrow,firePos.transform.position,Quaternion.identity);
        Rigidbody2D rb = arrowInstan.GetComponent<Rigidbody2D>();
        //rb.AddForce(Vector2.left * arrowForce, ForceMode2D.Impulse);
        rb.velocity = new Vector2(-arrowSpeed, 0);
    }

    
}
