using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int scoreValue = 0;
    private int scoreIntialValue = 0;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        UpdateScore();
    }

    public void IncreaseScore(int increaseScore)
    {
        scoreValue += increaseScore;

        if(scoreValue <= 0)
        {
            scoreValue = 0;
        }
        UpdateScore();
    }
    private void UpdateScore()
    {
        scoreText.text = "Score : " + scoreValue;
    }

    public void ResetScore()
    {
        scoreText.text = "Score : " + scoreIntialValue;
    }
}
