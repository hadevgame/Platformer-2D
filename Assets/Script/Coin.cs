using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    private CoinController controller;
    private bool hasCollided = false;
    void Start()
    {
        controller = CoinController.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasCollided) 
        {
            controller.UpdateCoin(value);
            hasCollided = true;
            Destroy(gameObject);
            
        }
    }
}
