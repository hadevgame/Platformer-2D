using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{
    public static CoinController instance;
    private int coins = 0;
    public TextMeshProUGUI CoinsDisplay;

    public void Awake()
    {
        if (!instance) 
        {
            instance= this;
        }
    }

    public int GetCoins() 
    {
        return coins;
    }
    public void UpdateCoin (int amount) 
    {
        coins += amount;
        CoinsDisplay.text = coins.ToString();
    }

    public void BuyItem(int amount) 
    {
        coins -= amount;
        CoinsDisplay.text = coins.ToString();
    }

    public bool CheckCoins(int coins) 
    {
        if (coins == 0 || coins < 2)
        {
            return false;
        }
        else return true;
    }
}
