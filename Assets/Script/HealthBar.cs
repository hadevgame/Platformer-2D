using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI healthText;


    public void UpdateBar(int curhealth, int maxhealth)
    {
        healthText.text = curhealth.ToString() + "/" + maxhealth.ToString();
        fillBar.fillAmount = (float)curhealth / (float)maxhealth;

    }
}
