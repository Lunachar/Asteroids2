using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text text;         // Reference to the UI text element for displaying the score
    public int initialScore = 0;  // Initial score value
    private static int score = 0;  // Static variable to store the player's score

    private void Start()
    {
        score = initialScore;
    }

    public void Update()
    {
        // Update the UI text element to display the current score
        text.text = $"Score: " + GetScore();
    }

    // Add points to the player's score
    public static void AddScore(int points)
    {
        score += points;
    }

    // Get the current player's score
    public static int GetScore()
    {
        return score;
    }
}