using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Asteroids2;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DisplayUIManager : MonoBehaviour
{
    public Text scroreText;         // Reference to the UI text element for displaying the score
    public Text playerHPText;
    public Text time;
    public GameManager gameManager;
    
    public PlayerModel _playerModel;
    
    public int initialScore = 0;  // Initial score value
    private static int score = 0;  // Static variable to store the player's score
    private static int _playerHp;
    private PauseManager _pauseManager;

    private void Start()
    {
        _pauseManager = GameObject.Find("ManagersDDOL").GetComponent<PauseManager>();
        gameManager = GameObject.Find("ManagersDDOL").GetComponent<GameManager>();
        score = initialScore;
    }

    public void Update()
    {
        _playerHp = _playerModel.PlayerHealth.GetCurrentHealth();
        
        // Update the UI text element to display the current score
        scroreText.text = $"Score: " + GetScore();
        playerHPText.text = $"HP: {_playerHp}";
        time.text = _pauseManager.gameTimer.ToString();
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