using System;
using UnityEngine;

namespace Asteroids2
{
    public class PauseManager : MonoBehaviour
    {
        private static bool _isPaused = false;
        public float gameTimer = 0f;
        private GameManager _gameManager;

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
            if (!_isPaused && !_gameManager.boosDieFlag && !_gameManager.playerDieFlag)
            {
                gameTimer += Time.deltaTime;
            }
        }
    }
}