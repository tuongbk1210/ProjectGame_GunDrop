using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI valuetext;
    public int score = 0;

    public void addScore(int value)
    {
        score += value;
        UpdateScoreUI();
    }
     
    void UpdateScoreUI()
    {
        if(valuetext != null)
        {
            valuetext.text = score.ToString();
        }
    }
}
