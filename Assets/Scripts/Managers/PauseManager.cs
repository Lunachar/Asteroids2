using System;
using UnityEngine;

namespace Asteroids2
{
    public class PauseManager : MonoBehaviour
    {
        private static bool _isPaused = false;
        public static float gameTimer = 0f;
        private GameManager _gameManager;
        private bool flagKeper;

        public static bool IsPaused
        {
            get { return _isPaused; }
        }

        public void TogglePause()
        {
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0 : 1;
        }

        public void StartGameStartTime()
        {
            Time.timeScale = 1f;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _gameManager = GameObject.Find("ManagersDDOL").GetComponent<GameManager>();
        }

        private void Update()
        {
            flagKeper = _gameManager.boosDieFlag;
            Debug.Log($"1::: {_isPaused} ::: 2: {flagKeper}");
            if (!_isPaused && !flagKeper && !_gameManager.playerDieFlag)
            {
                    gameTimer += Time.deltaTime;
                    Debug.Log($"TIME: {gameTimer.ToString()}");
            }
        }
    }
}