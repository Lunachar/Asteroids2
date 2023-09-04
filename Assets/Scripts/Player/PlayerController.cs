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
            _playerModel.Rotate();

            // Update the position and rotation of the player in the PlayerView
            _playerView.UpdatePosition(_playerModel.Position);
            _playerView.UpdateRotation(_playerModel.Rotation);

            // if (Input.GetButtonDown("Fire1"))
            // {
            //     _playerModel.Shoot();
            // }
        }

    }
}
