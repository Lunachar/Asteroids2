using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids2
{
    public class GameManager:MonoBehaviour
    {
        private GameState gameState;

        private int _playerScore;
        private int _playerHp;
        private float _asteroidGamePlay;
        private float _barrelGamePlay;

        private void Start()
        {
            gameState = GameState.Start;
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Additive);

            StartCoroutine(SpawnAsteroids());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameState == GameState.Game)
                {
                    // show settings menu
                    SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
                    gameState = GameState.None;
                }
                else if (gameState == GameState.None)
                {
                    // back in the game
                    SceneManager.UnloadSceneAsync("SettingsScene");
                    gameState = GameState.Game;
                }
            }
        }

        private IEnumerator SpawnAsteroids()
        {
            yield return new WaitForSeconds(_asteroidGamePlay);

            while (gameState == GameState.Game)
            {
                //Asteroid asteroid = enemyFactory.Create(new Health(100));
            }
        }
    }
}