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
    public void UpdateCoin (int amount) 
    {
        coins += amount;
        CoinsDisplay.text = coins.ToString();
    }
}
