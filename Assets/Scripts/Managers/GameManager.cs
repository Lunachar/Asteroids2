using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids2.UI;
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
        private GameObject _player;
        private GameState gameState;
        private Scene _currentGameScene;
        private AudioListener[] _audioListeners;
        private EventSystem[] _eventSystems;
        private PauseManager _pauseManager;
        private forPauseClass _forPauseClasses;

        public Button settingsButton;
        private bool _settingsButtonClicked;

        public Button backButton;
        private bool _backButtonClicked;

        private float _musicLenght;
        internal bool boosDieFlag;
        internal bool playerDieFlag;
        
        //private float _timeToCompletGame;
        private float _startGameTime;
        private float _endGameTime;


        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            // _elapsedGameTime = 0;
            _pauseManager = gameObject.AddComponent<PauseManager>();
            _forPauseClasses = gameObject.AddComponent<forPauseClass>();
            gameState = GameState.MainMenu;
            StartCoroutine(LoadMainMenu());
        }

        private void Update()
        {
            OnlyOneAudioListenerIsEnabled();
            OnlyOneEventSystemIsEnabled();
            
            if (Input.GetKeyDown(KeyCode.Escape) || _settingsButtonClicked || _backButtonClicked)
            {
                if (gameState == GameState.Game || gameState == GameState.MainMenu)
                {
                    // show settings menu
                    _forPauseClasses.TogglePauseSet();
                    _pauseManager.TogglePause();
                    gameState = GameState.None;
                    _settingsButtonClicked = false;
                }
                else if (gameState == GameState.None )
                {
                    // back in the game
                    _forPauseClasses.TogglePauseSet();
                    _pauseManager.TogglePause();
                    gameState = GameState.Game;
                    _backButtonClicked = false;
                }
            }

            if (_player == null)
            {
                _player = GameObject.FindWithTag("Player");
            }
            if (boosDieFlag && gameState == GameState.Game)
            {
                WinGame();
            }

            if (playerDieFlag && gameState == GameState.Game)
            {
                LoseGame();
            }
            _settingsButtonClicked = false;
        }

        // private void OnEnable()
        // {
        //     SceneManager.sceneLoaded += OnSceneLoaded;
        // }
        //
        // private void OnDisable()
        // {
        //     SceneManager.sceneLoaded -= OnSceneLoaded;
        // }

        private IEnumerator LoadMainMenu()
        {
            yield return new WaitUntil(() => SceneManager.GetSceneByName("MainMenuScene").isLoaded);
            
            MusicManagerScript.Instance.PlayMainMenuMusic();

            gameState = GameState.MainMenu;
        }

        internal IEnumerator StartMainMenu()
        {
            
            boosDieFlag = false;
            playerDieFlag = false;
            _currentGameScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene("MainMenuScene");
            yield return StartCoroutine(LoadMainMenu());
            settingsButton = GameObject.Find("ButtonSettings").GetComponent<Button>();
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            SceneManager.UnloadSceneAsync(_currentGameScene);
        }

        internal void StartGame()
        {
            StartCoroutine(LoadGameScene());
        }

        internal void OnSettingsButtonClicked()
        {
                _settingsButtonClicked = true;
        }
        internal void OnBackButtonClicked()
        {
            _backButtonClicked = true;
        }

        private IEnumerator LoadGameScene()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
            EnemyManager.IsEnemyOnScene(false);
            _forPauseClasses.StartGameUnpause();
            _pauseManager.StartGameStartTime();
            PauseManager.gameTimer = 0f;
            _startGameTime = Time.realtimeSinceStartup;
    
            yield return loadOperation;

            MusicManagerScript.Instance.PlayGameMusic();
            gameState = GameState.Game;
    
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("MainMenuScene");
            yield return unloadOperation;
            boosDieFlag = false;
            playerDieFlag = false;
        }

        private void WinGame()
        {
            //_endGameTime = Time.realtimeSinceStartup;
            // _elapsedGameTime = _pauseManager.gameTimer /*(_endGameTime - _startGameTime)*/;
            // Debug.LogError($"!!!!!!!! {_elapsedGameTime} !!!!!!");
            gameState = GameState.Win;
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
            yield return new WaitForSeconds(2f);
        }
        private void LoseGame()
        {
            gameState = GameState.Lose;
            //PlayerModel playerModel = _player.GetComponent<PlayerModel>();
            //Destroy(playerModel.gameObject);
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

        private void OnlyOneEventSystemIsEnabled()
        {
            _eventSystems = FindObjectsOfType<EventSystem>();
        
            if (_eventSystems.Length > 1)
            {
                for (int i = 1; i < _eventSystems.Length; i++)
                {
                    _eventSystems[i].enabled = false;
                }
            }
        
            if (_eventSystems.Length > 0)
            {
                _eventSystems[0].enabled = true;
            }
        }
        //
        // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        // {
        //     OnlyOneEventSystemIsEnabled();
        // }

        internal float ElapsedTime()
        {
            return Time.unscaledTime - _startGameTime;
        }
    }
}