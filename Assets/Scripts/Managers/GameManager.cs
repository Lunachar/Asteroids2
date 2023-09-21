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

        
        //private int _playerScore;
        //private int _playerHp;
        //private float _asteroidGamePlay;
        //private float _barrelGamePlay;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            gameState = GameState.MainMenu;
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
            //SceneManager.UnloadSceneAsync("MainMenuScene");
            StartCoroutine(LoadGameScene());
        }

        private void OpenSettings()
        {
            SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
            gameState = GameState.Pause;
        }

        private IEnumerator LoadGameScene()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
    
            yield return loadOperation;

            MusicManagerScript.Instance.PlayGameMusic();
            gameState = GameState.Game;
    
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("MainMenuScene");
            yield return unloadOperation;
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


    }
}