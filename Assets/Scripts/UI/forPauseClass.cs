using System;
using UnityEngine;

namespace Asteroids2
{
    public class forPauseClass : MonoBehaviour
    {
        private GameObject _player;
        private GameObject _ui;
        private GameObject _enemy;
        private GameObject _currentCanvas;
        private Canvas _settingsCanvas;

        public GameObject[] GameObjectsToPause;


        private void Update()
        {
            if (GameObject.Find("Player") != null) _player = GameObject.Find("Player");

            if (GameObject.FindWithTag("Enemy") != null)
            {
                _enemy = GameObject.FindWithTag("Enemy");
            }
        
            if (GameObject.Find("UI") != null) _ui = GameObject.Find("UI");

            if (GameObject.FindWithTag("UI") != null) _currentCanvas = GameObject.FindWithTag("UI");

            if(GameObject.Find("CanvasSettings") != null) _settingsCanvas = GameObject.Find("CanvasSettings").GetComponent<Canvas>();
            
            GameObjectsToPause = new[] {_player, _ui, _enemy, _currentCanvas};
        }

        public void TogglePauseSet()
        {
            foreach (var VARIABLE in GameObjectsToPause)
            {
                if (VARIABLE != null)
                {
                    VARIABLE.SetActive(!VARIABLE.activeSelf);
                }
            }

            if (_settingsCanvas != null)
            {
                _settingsCanvas.enabled = !_settingsCanvas.enabled;
            }
        }

        public void StartGameUnpause()
        {
            foreach (var VARIABLE in GameObjectsToPause)
            {
                if (VARIABLE != null)
                {
                    VARIABLE.SetActive(true);
                }
            }
        }
    }
}