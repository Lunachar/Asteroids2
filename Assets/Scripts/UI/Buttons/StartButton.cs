using System;
using Asteroids2;
using UnityEngine.UI;

namespace UI.Buttons
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class StartButton : MonoBehaviour
    {
        public Button button;
        public GameManager gameManager;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            button.onClick.AddListener(gameManager.StartGame);
        }
    }
}