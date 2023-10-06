﻿using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UI.Buttons;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = System.Object;

namespace Asteroids2
{
    public class GameManager:MonoBehaviour
    {
        public HighScoreManager highScoreManager;
        private GameState gameState;
        private Scene _currentGameScene;
        private EventSystem _eventSystem;
        private AudioListener[] _audioListeners;

        public Button startButton;
        public Button settingsButton;

        private float _musicLenght;
        internal bool boosDieFlag;
        internal bool playerDieFlag;

        private float _timeToCompletGame;
        private float _startGameTime;
        private float _endGameTime;
        internal float _elapsedGameTime;


        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _elapsedGameTime = 0;
            gameState = GameState.MainMenu;
            StartCoroutine(LoadMainMenu());
            settingsButton.onClick.AddListener(OpenSettings);
        }

        private IEnumerator LoadMainMenu()
        {
            yield return new WaitUntil(() => SceneManager.GetSceneByName("MainMenuScene").isLoaded);
            
            MusicManagerScript.Instance.PlayMainMenuMusic();

            gameState = GameState.MainMenu;
        }

        internal IEnumerator StartMainMenu()
        {
            _currentGameScene = SceneManager.GetActiveScene();
            Debug.LogError($"here 1");
            _eventSystem = FindObjectOfType<EventSystem>();
            SceneManager.LoadScene("MainMenuScene");
            yield return StartCoroutine(LoadMainMenu());
            SceneManager.UnloadSceneAsync(_currentGameScene);
            Debug.LogError($"here 2");
        }

        public void StartGame()
        {
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
            _startGameTime = Time.realtimeSinceStartup;
    
            yield return loadOperation;

            MusicManagerScript.Instance.PlayGameMusic();
            gameState = GameState.Game;
    
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("MainMenuScene");
            yield return unloadOperation;
        }

        private void Update()
        {
            OnlyOneAudioListenerIsEnabled();
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
            if (boosDieFlag)
            {
               WinGame();
               boosDieFlag = false;
            }

            if (playerDieFlag)
            {
                LoseGame();
                playerDieFlag = false;
            }
        }
        private void WinGame()
        {
            _endGameTime = Time.realtimeSinceStartup;
            _elapsedGameTime = (_endGameTime - _startGameTime);
            Debug.LogError($"!!!!!!!! {_elapsedGameTime} !!!!!!");

            MusicManagerScript.Instance.PlayWinMusic();
            StartCoroutine(LoadWinScene());
        }

        private IEnumerator LoadWinScene()
        {
            _musicLenght = MusicManagerScript.Instance.winMusic.length;
            Debug.LogError($"::::: {_musicLenght} :::::");
            yield return new WaitForSeconds(_musicLenght - 3f);
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("WinScene", LoadSceneMode.Additive);
            yield return loadOperation;
            gameState = GameState.Win;
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("GameScene");
            yield return unloadOperation;
        }
        private void LoseGame()
        {
            PlayerModel playerModel = GameObject.Find("Player").GetComponent<PlayerModel>();
            Destroy(playerModel.gameObject);
            MusicManagerScript.Instance.PlayLoseMusic();
            Debug.LogError($"-= LOSE MUSIC =-");
            StartCoroutine(LoadLoseScene());
        }

        private IEnumerator LoadLoseScene()
        {
            _musicLenght = MusicManagerScript.Instance.loseMusic.length;
            Debug.LogError($"::::: {_musicLenght} :::::");
            yield return new WaitForSeconds(_musicLenght);
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("LoseScene", LoadSceneMode.Additive);
            yield return loadOperation;
            gameState = GameState.Lose;
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("GameScene");
            yield return unloadOperation;
        }

        private void OnlyOneAudioListenerIsEnabled()
        {
            _audioListeners = FindObjectsOfType<AudioListener>();

            if (_audioListeners.Length > 1)
            {
                for (int i = 1; i < _audioListeners.Length; i++)
                {
                    _audioListeners[i].enabled = false;
                }
            }

            if (_audioListeners.Length > 0)
            {
                _audioListeners[0].enabled = true;
            }
        }
    }
}