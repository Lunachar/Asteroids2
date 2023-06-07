using System;
using Asteroids2;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerModel : MonoBehaviour, IMove, IRotation, IShoot
    {
        private Rigidbody2D _rb;
        public Vector2 Position => _rb.position;
        public float Rotation => _rb.rotation;

        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _rotationSpeed = 20f;

        [SerializeField] private Transform gun1;
        [SerializeField] private Transform gun2;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Rigidbody2D bulletRb;
        [SerializeField] private float bulletSpeed = 1f;
        private bool _shootSide;
        private Transform _bulletSpawnPoint;

        private float _rotationSmothness = 90f;

        private float _horizontalInput;
        private float _verticalInput;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _verticalInput = Input.GetAxis("Vertical");
            _horizontalInput = Input.GetAxis("Horizontal");
        }

        public void Move()
        {
            Vector2 movement = new Vector2(_horizontalInput, _verticalInput);
            _rb.AddForce(movement * _speed, ForceMode2D.Impulse);

            if (Camera.main == null) return;
            float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            float minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            float maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

            var position2 = transform.position;
            float clampedX = Mathf.Clamp(position2.x, minX, maxX);
            float clampedY = Mathf.Clamp(position2.y, minY, maxY);
            
            Vector3 screenBounds = new Vector2(clampedX, clampedY);
            Vector2 direction = screenBounds - position2;

            _rb.AddForce(direction * _speed, ForceMode2D.Impulse);
        }

        public void Rotate()
        {
            _rb.rotation = GetRotationAngle();
        }
        public float GetRotationAngle()
        {
            
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var playerPosition = transform.position;
            //var direction = Zombie.position - playerPosition;
            var direction = mousePosition - playerPosition;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return angle;
        }


        public void Shoot()
        {
                if (_shootSide)
                {
                    _bulletSpawnPoint = gun1;
                    _shootSide = false;
                }
                else
                {
                    _bulletSpawnPoint = gun2;
                    _shootSide = true;
                }

                var bullet = Instantiate(bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
                bulletRb = bullet.GetComponent<Rigidbody2D>();
                bulletRb.AddForce(_bulletSpawnPoint.up * bulletSpeed, ForceMode2D.Impulse);

                StartCoroutine(gameObject.AddComponent<Bullet>().RotateBullet(bulletRb));
        }
    }
}