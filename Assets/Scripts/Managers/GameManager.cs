using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UI.Buttons;
using UnityEngine.UI;

namespace Asteroids2
{
    public class GameManager:MonoBehaviour
    {
        private GameState gameState;

        public Button startButton;
        public Button settingsButton;

        
        private int _playerScore;
        private int _playerHp;
        private float _asteroidGamePlay;
        private float _barrelGamePlay;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            gameState = GameState.MainMenu;
            //SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Additive);
            StartCoroutine(LoadMainMenu());
            
            startButton.onClick.AddListener(StartGame);
            settingsButton.onClick.AddListener(OpenSettings);
        }

        private IEnumerator LoadMainMenu()
        {
            yield return new WaitUntil(() => SceneManager.GetSceneByName("MainMenuScene").isLoaded);
            
            MusicManagerScript.Instance.PlayMainMenuMusic();

            gameState = GameState.MainMenu;
        }

        private void StartGame()
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
            StartCoroutine(LoadGameScene());
        }

        private void OpenSettings()
        {
            SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
            gameState = GameState.Pause;
        }

        private IEnumerator LoadGameScene()
        {
            yield return new WaitUntil(() => SceneManager.GetSceneByName("GameScene").isLoaded);
            
            MusicManagerScript.Instance.PlayGameMusic();

            SceneManager.UnloadSceneAsync("MainMenuScene");

            gameState = GameState.Game;
        }

        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.Escape))
        //     {
        //         if (gameState == GameState.Game)
        //         {
        //             // show settings menu
        //             SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
        //             gameState = GameState.None;
        //         }
        //         else if (gameState == GameState.None)
        //         {
        //             // back in the game
        //             SceneManager.UnloadSceneAsync("SettingsScene");
        //             gameState = GameState.Game;
        //         }
        //     }
        // }


    }
}