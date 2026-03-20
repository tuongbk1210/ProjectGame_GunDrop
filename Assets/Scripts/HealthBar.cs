using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image fillBar;
    public TextMeshProUGUI valuetext;

    public void UpdateBar(int currentValue, int maxValue)
    {
        fillBar.fillAmount = (float)currentValue / (float)maxValue;
        if(valuetext != null)
        {
            valuetext.text = currentValue.ToString() + " / " + maxValue.ToString();
        }
    }
}
