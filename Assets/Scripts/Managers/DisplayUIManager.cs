using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids2;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DisplayUIManager : MonoBehaviour
{
    public Text scroreText;         // Reference to the UI text element for displaying the score
    public Text playerHPText;
    // public Text asteroidHPText;
    // //public Text barrelHPText;
    // public Text BossHpText;
    
    public PlayerModel _playerModel;
    // public Asteroid _asteroid;
    // public Barrel _barrel;
    
    public int initialScore = 0;  // Initial score value
    private static int score = 0;  // Static variable to store the player's score
    private static int _playerHp;
    // public static int _asteroidHp;
    // private static int _barrelHp;


    private void Start()
    {
        score = initialScore;
    }

    public void Update()
    {
        _playerHp = _playerModel.PlayerHealth.GetCurrentHealth();
        //_asteroidHp = _asteroid.Health.GetCurrentHealth();
        
        // Update the UI text element to display the current score
        scroreText.text = $"Score: " + GetScore();
        playerHPText.text = $"HP: {_playerHp}";
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