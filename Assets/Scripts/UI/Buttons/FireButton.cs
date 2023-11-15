using System;
using Asteroids2;
using Player;
using UnityEngine.UI;
using UnityEngine;

namespace UI.Buttons
{
    public class FireButton : MonoBehaviour
    {
        public Button button;
        public PlayerModel PlayerModel;

        private void Start()
        {
#if UNITY_STANDALONE_WIN && UNITY_EDITOR
            gameObject.SetActive(false);
#endif
            
#if UNITY_ANDROID
gameObject.SetActive(true);
#endif
            
            PlayerModel = FindObjectOfType<PlayerModel>();
            button.onClick.AddListener(PlayerModel.OnFireButtonClicked);
        }
        private void OnFireButtonClicked()
        {
            PlayerModel.OnFireButtonClicked();
        }
    }
}