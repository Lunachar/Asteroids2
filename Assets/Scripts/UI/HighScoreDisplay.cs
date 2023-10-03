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
        private bool _isHighScore;

        string _text = "High Scores: \n";

        private void Start()
        {
            playerNameInput.onEndEdit.AddListener(UpdateHighScoreText);
            highScoreManager = GetComponent<HighScoreManager>();
            gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
            currentTimeText.text = $"your time is: {gameManager._elapsedGameTime.ToString()}";

            _isHighScore = highScoreManager.AddHighScore(playerNameInput.text, _gameTime);



            

            saveButton.onClick.AddListener(SaveHighScore);
        }

        // private void Update()
        // {
        //     highScoreText.text = highScores.ToString();
        // }

        private void SaveHighScore()
        {
            //string playerName = playerNameInput.text;
            _gameTime = gameManager._elapsedGameTime;

            if (_isHighScore)
            {
                RebuildHighScoreText();
            }
            //highScoreManager.AddHighScore(playerName, gameTime);
            playerNameInput.interactable = false;
            saveButton.interactable = false;
        }

        private void RebuildHighScoreText()
        {
            _text = "Hihg Scores: \n";
            
            foreach (HighScoreManager.HighScoreEntry entry in highScoreManager.highScores)
            {
                _text += $"{entry.playerName}: {entry.gameTime}\n";
            }

            highScoreText.text = _text;
        }

        private void UpdateHighScoreText(string newPlayerName)
        {
            _text += $"{newPlayerName}: {gameManager._elapsedGameTime}\n";
            highScoreText.text = _text;
        }
    }
}