using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids2
{
    public class GameManager : MonoBehaviour
    {
        private GameState _gameState;  // Current game state

        private int _playerScore;      // Player's score
        private int _playerHp;         // Player's health points
        private float _asteroidGamePlay; // Duration of asteroid gameplay
        private float _barrelGamePlay;   // Duration of barrel gameplay

        private void Start()
        {
            _gameState = GameState.Start;  // Set the initial game state to "Start"
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Additive);  // Load the main menu scene

            StartCoroutine(SpawnAsteroids()); // Start spawning asteroids
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_gameState == GameState.Game)
                {
                    // Player pressed Escape during gameplay
                    // Show settings menu by loading the SettingsScene
                    SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
                    _gameState = GameState.None; // Set game state to "None" (paused)
                }
                else if (_gameState == GameState.None)
                {
                    // Player pressed Escape while in the settings menu
                    // Return to the game by unloading the SettingsScene
                    SceneManager.UnloadSceneAsync("SettingsScene");
                    _gameState = GameState.Game; // Set game state back to "Game"
                }
            }
        }

        private IEnumerator SpawnAsteroids()
        {
            yield return new WaitForSeconds(_asteroidGamePlay);

            while (_gameState == GameState.Game)
            {
               
            }
        }
    }
}
