using System;
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
        public HighScoreManager highScoreManager;
        public GameManager gameManager;
        private float _gameTime;
        private bool _isRebuilded;

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
            
            
            saveButton.onClick.AddListener(SaveHighScore);
            playerNameInput.onEndEdit.AddListener(UpdateHighScoreText);
            UpdateHighScoreText("");
        }

        private void Update()
        {
            if (!_isRebuilded)
            {
                highScoreText.text = _text;
            }
        }

        private void SaveHighScore()
        {
            highScoreManager.AddHighScore(playerNameInput.text, _gameTime);
            _text = RebuildHighScoreText();
            _isRebuilded = true;
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

        private void UpdateHighScoreText(string newPlayerName)
        {
            if (highScoreManager.IsHighScore(_gameTime))
            {
                highScoreText.text = RebuildHighScoreText();
            }
        }
    }
}