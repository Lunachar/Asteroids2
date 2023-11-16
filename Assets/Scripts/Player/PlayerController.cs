using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerModel _playerModel; // Reference to the PlayerModel component
        private PlayerView _playerView;   // Reference to the PlayerView component

        private void Awake()
        {
            // Get references to the PlayerModel and PlayerView components on the same GameObject
            _playerModel = GetComponent<PlayerModel>();
            _playerView = GetComponent<PlayerView>();
        }

        void Update()
        {
            // Call movement and rotation functions from PlayerModel
            _playerModel.Move();
#if UNITY_STANDALONE_WIN && UNITY_EDITOR
            _playerModel.Rotate();
#endif
            

            // Update the position and rotation of the player in the PlayerView
            _playerView.UpdatePosition(_playerModel.Position);
#if UNITY_STANDALONE_WIN && UNITY_EDITOR
            _playerView.UpdateRotation(_playerModel.Rotation);
#endif

            // if (Input.GetButtonDown("Fire1"))
            // {
            //     _playerModel.Shoot();
            // }
        }

    }
}
