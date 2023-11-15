using System;
using Asteroids2;
using UnityEngine.UI;
using UnityEngine;

namespace UI.Buttons
{
    public class ExitGameButton : MonoBehaviour
    {
            public Button button;
            public GameManager gameManager;

            private void Start()
            {
                gameManager = FindObjectOfType<GameManager>();
                button.onClick.AddListener(gameManager.OnExitButtonClicked);
            }
    }
}