using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text text;
    private static int score = 0;

    public void Update()
    {
        text.text = $"Score: " + GetScore();
    }


    public static void AddScore(int points)
    {
        score += points;
    }

    public static int GetScore()
    {
        return score;
    }
}
