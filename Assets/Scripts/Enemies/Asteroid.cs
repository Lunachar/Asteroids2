using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids2
{
    public class Asteroid : Enemy, IMove
    {
        private Health _asteroidHealth;     // Health component for the asteroid
        private Transform _target;          // Target (e.g., player) for the asteroid to follow
        private Rigidbody2D _rb;            // Rigidbody component for physics interactions
        private float _asteroidDestroyTime; // Time when the asteroid should be destroyed
        private float _startTime;           // Time when the asteroid was created
        private int _asteroidHp;
        
        public TextMeshProUGUI asteroidHp;
        public float asteroidSpeed = 1f;         // Speed at which the asteroid moves
        public override int ScoreValue => 100;   // Score value awarded when the asteroid is destroyed


        private void Start()
        {
            _startTime = Time.time;
            _asteroidHealth = new Health(100); // Initialize the asteroid's health
            _rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
            SetTarget();                        // Set the target for the asteroid
            FaceTarget();                      // Rotate the asteroid to face the target
            Move();                            // Move the asteroid towards the target
        }

        private void Update()
        {
            _asteroidHp = CurrentAsteroidHealth;
            asteroidHp.text = _asteroidHp.ToString();
            float currentTime = Time.time;
            float elapsedTime = currentTime - _startTime;

            if (elapsedTime >= 6f)
            {
                if (gameObject != null)
                {
                    Destroy(); // Destroy the asteroid when the specified time has passed
                }
            }
        }

        private void FaceTarget()
        {
            Vector2 direction = _target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rb.rotation = angle; // Rotate the asteroid to face the target
        }

        public override void SetTarget()
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                _target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            } // Find and set the player as the target
            //Debug.Log("1Target is:" + _target);
        }

        public void Move()
        {
            //Debug.Log("2Target is:" + _target);
            var asteroidPosition = transform.position;
            var playerPosition = _target.position;
            var direction = playerPosition - asteroidPosition;
            _rb.AddForce(direction.normalized * asteroidSpeed, ForceMode2D.Impulse); // Move the asteroid towards the player
        }

        public override void TakeDamage(int damageAmount)
        {
            //Debug.Log("take damage enemy:" + damageAmount);
            _asteroidHealth.takeDamage(damageAmount); // Reduce asteroid's health when it takes damage
            if (_asteroidHealth.GetCurrentHealth() <= 0)
            {
                Die(); // Destroy the asteroid when its health reaches zero
                //AsteroidFactory.SetAsteroidSpawned(false); // Notify the asteroid factory
                DisplayUIManager.AddScore(ScoreValue); // Add score when the asteroid is destroyed
            }
        }

        private void Destroy()
        {
            Destroy(gameObject); // Destroy the asteroid
            EnemyManager.IsEnemyOnScene(false); // Notify the EnemyManager that an enemy is no longer on the scene
        }

        public int CurrentAsteroidHealth => _asteroidHealth.GetCurrentHealth(); // Get the current health of the asteroid
    }
}
