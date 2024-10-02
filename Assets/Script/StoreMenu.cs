using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : MonoBehaviour
{
    
    public Player player;
    private CoinController controller;
    Health playerHealth;
    

    private void Start()
    {
        controller = CoinController.instance;
        
    }
    public void UpdateDamage() 
    {
       if(player != null && controller != null) 
       {
            int curcoins = controller.GetCoins();
            if (controller.CheckCoins(curcoins) == true)
            {
                player.damage += 1;
                controller.BuyItem(2);
            }
            else 
            {
                Debug.Log("Not enough coin");
            }

        }
    }

    public void UpdateMaxHealth() 
    {
        playerHealth = player.GetComponent<Health>();
        int curcoins = controller.GetCoins();
        if (player != null && controller != null)
        {
            if (controller.CheckCoins(curcoins) == true)
            {
                playerHealth.UpdateMaxHealth();
                controller.BuyItem(2);
            }
            else
            {
                Debug.Log("Not enough coin");
            }
        }
    }

    public void UpdateSpeed() 
    {
        if (player != null && controller != null)
        {
            int curcoins = controller.GetCoins();
            if (controller.CheckCoins(curcoins) == true)
            {
                player.rollboost += 1;
                controller.BuyItem(2);
            }
            else
            {
                Debug.Log("Not enough coin");
            }

        }
    }
}
