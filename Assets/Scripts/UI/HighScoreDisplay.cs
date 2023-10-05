﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids2.UI
{
    public class HighScoreDisplay: MonoBehaviour
    {
        public TMP_Text highScoreText;
        public TMP_Text currentTimeText;
        public TMP_InputField playerNameInput;
        public Button saveButton;
        public Button backToGame;
        public HighScoreManager highScoreManager;
        public GameManager gameManager;
        private float _gameTime;
        
        private bool _isRebuilded;
        private bool _isNameEntered;
        private bool _isSaveButtonPressed;

        string _text = "High Scores: \n";

        private void Start()
        {
            highScoreManager = GetComponent<HighScoreManager>();
            gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
            _gameTime = gameManager._elapsedGameTime;
            currentTimeText.text = $"your time is: {gameManager._elapsedGameTime.ToString()}";
            
            highScoreManager.LoadHighScores();
            Debug.LogError($"Start: {highScoreManager.IsHighScore(_gameTime).ToString()}");
            
            
            if (!highScoreManager.IsHighScore(_gameTime))
            {
                playerNameInput.interactable = false;
                saveButton.interactable = false;
            }
            saveButton.onClick.AddListener(OnSaveButtonPressed);
            backToGame.onClick.AddListener(BackToGame);
            playerNameInput.onEndEdit.AddListener(OnPlayerNameEntered);
            StartCoroutine(HighScorePresent());
        }

        private void Update()
        {

        }

        private void OnPlayerNameEntered(string playerName) => _isNameEntered = true;

        private void OnSaveButtonPressed() => _isSaveButtonPressed = true;

        private IEnumerator HighScorePresent()
        {
            yield return new WaitForSeconds(1f);
            highScoreText.text = RebuildHighScoreText();
            yield return new WaitUntil(() => _isNameEntered || _isSaveButtonPressed);
            SaveHighScore();
            yield return new WaitForSeconds(0.5f);
            highScoreText.text = RebuildHighScoreText();
        }

        private void SaveHighScore()
        {
            highScoreManager.AddHighScore(playerNameInput.text, _gameTime);

            playerNameInput.interactable = false;
            saveButton.interactable = false;
        }

        private string RebuildHighScoreText()
        {
            _text = "High Scores: \n";
            int i = 1;
            foreach (HighScoreManager.HighScoreEntry entry in highScoreManager.highScores)
            {
                _text += $"{i}. {entry.playerName}: {entry.gameTime}\n";
                i++;
            }
            return _text;
        }

        private void BackToGame()
        {
            StartCoroutine(gameManager.StartMainMenu());
        }
    }
}