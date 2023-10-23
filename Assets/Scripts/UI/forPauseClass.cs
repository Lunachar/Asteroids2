using System;
using UnityEngine;

namespace Asteroids2
{
    public class forPauseClass : MonoBehaviour
    {
        private GameObject _player;
        private GameObject _ui;
        private GameObject _enemy;

        public GameObject[] GameObjectsToPause;


        private void Update()
        {
            if (GameObject.Find("Player") != null) _player = GameObject.Find("Player");

            if (GameObject.FindWithTag("Enemy") != null) _enemy = GameObject.FindWithTag("Enemy");
        
            if (GameObject.Find("UI") != null) _ui = GameObject.Find("UI");

            GameObjectsToPause = new[] {_player, _ui, _enemy};
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