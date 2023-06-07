using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerModel _playerModel;
        private PlayerView _playerView;

        private void Awake()
        {
            _playerModel = GetComponent<PlayerModel>();
            _playerView = GetComponent<PlayerView>();
        }

        void Update()
        {
            _playerModel.Move();
            _playerModel.Rotate();
            

            _playerView.UpdatePosition(_playerModel.Position);
            _playerView.UpdateRotation(_playerModel.Rotation);

            if (Input.GetButtonDown("Fire1"))
            {
                _playerModel.Shoot();
            }
        }

    }
}
