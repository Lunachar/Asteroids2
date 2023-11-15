using System;
using System.Collections;
using Asteroids2;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerModel : MonoBehaviour, IMove, IRotation, IShoot
    {
        private Rigidbody2D _rb;
        private Collider2D _col;
        public static event Action OnScreenEdgeCollision;
        public static event Action PlayerGunShoot;
        public static event Action PlayerCollide;

        public Vector2 Position => _rb.position; // Player's current position
        public float Rotation => _rb.rotation; // Player's current rotation angle

        [SerializeField] private float _speed = 5f; // Movement speed of the player
        //[SerializeField] private float _rotationSpeed = 20f;  // Rotation speed of the player

        [SerializeField] private Transform gun1; // Reference to the first gun
        [SerializeField] private Transform gun2; // Reference to the second gun
        [SerializeField] private GameObject bulletPrefab; // Prefab of the bullet
        [SerializeField] private Rigidbody2D bulletRb; // Rigidbody of the bullet
        [SerializeField] private float bulletSpeed = 2f; // Speed of the bullets
        private bool _shootSide; // Tracks which gun to use for shooting
        private Transform _bulletSpawnPoint; // Reference to the spawn point of bullets

        private float _horizontalInput; // Input for horizontal movement
        private float _verticalInput; // Input for vertical movement

        private bool _isShooting = false; // Tracks if the player is currently shooting
        private float timeBetweenShoots = 0.2f; // Time delay between consecutive shots

        public Health PlayerHealth;
        public GameManager gameManager;
        public GameObject explosionPrefab;
        private ToggleGOSwitchComponents _tsc;

        public Joystick Joystick;
        
        private float _musicLenght;

        private int _dieCounter;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            PlayerHealth = new Health(100);
            _tsc = gameObject.AddComponent<ToggleGOSwitchComponents>();
            gameManager = GameObject.Find("ManagersDDOL").GetComponent<GameManager>();
        }

        private void Update()
        {
            CheckScreenEdgeCollision();
            if (PlayerHealth.GetCurrentHealth() <= 0 && _dieCounter == 0)
            {
                gameManager.playerDieFlag = true;
                _dieCounter = 1;
                _tsc.Switch(gameObject);
            }
            
#if UNITY_STANDALONE_WIN && UNITY_EDITOR
            _verticalInput = Input.GetAxis("Vertical"); // Get vertical input (e.g., W, S, Up, Down)
            _horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input (e.g., A, D, Left, Right)
            
            if (Input.GetMouseButton(0))
            {
                if (!_isShooting)
                {
                    _isShooting = true;
                    StartCoroutine(ContinuousShooting());
                }
            }
            else
            {
                _isShooting = false;
            }
#endif
            
#if UNITY_ANDROID
            _verticalInput = Joystick.Vertical;
            _horizontalInput = Joystick.Horizontal;
#endif

        }




        private IEnumerator ContinuousShooting()
        {
            while (_isShooting)
            {
                Shoot(); // Trigger shooting
                yield return new WaitForSeconds(timeBetweenShoots); // Delay between shots
            }
        }

        public void Move()
        {
            // Calculate movement direction based on input and apply force to the player
            Vector2 movement = new Vector2(_horizontalInput, _verticalInput);
            _rb.AddForce(movement * _speed, ForceMode2D.Impulse);

            if (Camera.main == null) return;

            var position2 = transform.position;
            CalculateScreenBounds(out var minX, out var maxX, out var minY, out var maxY);
            float clampedX = Mathf.Clamp(position2.x, minX, maxX);
            float clampedY = Mathf.Clamp(position2.y, minY, maxY);

            Vector3 screenBounds = new Vector2(clampedX, clampedY);
            Vector2 direction = screenBounds - position2;

            _rb.AddForce(direction * _speed, ForceMode2D.Impulse);
        }

        public void Rotate()
        {
            // Rotate the player to face the mouse cursor
            _rb.rotation = GetRotationAngle();
        }

        public float GetRotationAngle()
        {
            // Calculate the angle to rotate the player towards the mouse cursor
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var playerPosition = transform.position;
            var direction = mousePosition - playerPosition;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return angle;
        }

        public void Shoot()
        {
            // Determine which gun to use for shooting
            switch (_shootSide)
            {
                case true:
                    _bulletSpawnPoint = gun1;
                    _shootSide = false;
                    break;
                case false:
                    _bulletSpawnPoint = gun2;
                    _shootSide = true;
                    break;
            }

            // Create a bullet instance and shoot it
            var bullet = Instantiate(bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            PlayerGunShoot?.Invoke();
            bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce((_bulletSpawnPoint.up * (bulletSpeed + _speed)), ForceMode2D.Impulse);
        }

        private void CalculateScreenBounds(out float minX, out float maxX, out float minY, out float maxY)
        {
            minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        }

        private void CheckScreenEdgeCollision()
        {
            Vector2 position = _rb.position;

            CalculateScreenBounds(out var minX, out var maxX, out var minY, out var maxY);
            if (position.x <= minX || position.x >= maxX || position.y <= minY || position.y >= maxY)
            {
                OnScreenEdgeCollision?.Invoke();
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                PlayerCollide?.Invoke();
                Enemy enemy = col.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(5);
                }
                PlayerHealth.takeDamage(10);

                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
        }
        
        internal void OnFireButtonClicked()
        {
                if (!_isShooting)
                {
                    _isShooting = true;
                    StartCoroutine(ContinuousShooting());
                }
            
            else
            {
                _isShooting = false;
            }
        }
    }
}