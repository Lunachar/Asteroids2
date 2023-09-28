using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Asteroids2
{
    public class Barrel : Enemy, IMove, IRotation
    {
        private Health _barrelHealth;     // Health component for the barrel
        private Transform _target;          // Target (e.g., player) for the barrel to follow
        private Rigidbody2D _rb;            // Rigidbody component for physics interactions
        private float _barrelDestroyTime; // Time when the barrel should be destroyed
        private float _startTime;           // Time when the barrel was created
        private Vector3 _initialPosition;     // For storing the start vertical position
        private int _barrelHp;

        public Text _text;
        //public barrelHealthUI barrelHealthUI; // UI component for displaying barrel health
        public float barrelSpeed = 15f;           // Speed at which the barrel moves
        public float barrelRotationSpeed = 80f;  // Speed at which the barrel rotates

        public override int ScoreValue => 100;   // Score value awarded when the barrel is destroyed
        Vector3 position;
        private void Start()
        {
            _initialPosition = transform.position;
            _startTime = Time.time;
            _barrelHealth = new Health(200); // Initialize the barrel's health
            _rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
            SetTarget();                        // Set the target for the barrel
            Rotate();
        }

        private void Update()
        {
            _barrelHp = CurrentBarrelHealth;
            _text.text = _barrelHp.ToString();
            float currentTime = Time.time;
            float elapsedTime = currentTime - _startTime;

            if (elapsedTime >= 8f)
            {
                if (gameObject != null)
                {
                    Destroy(); // Destroy the barrel when the specified time has passed
                }
            }
            //barrelHealthUI.UpdateHealthText(); // Update the UI for barrel health
           Move();
           

        }

        // private void FaceTarget()
        // {
        //     Vector2 direction = _target.position - transform.position;
        //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //     _rb.rotation = angle; // Rotate the barrel to face the target
        // }

        public override void SetTarget()
        {
            _target = GameObject.FindWithTag("Player").GetComponent<Transform>(); // Find and set the player as the target
            //Debug.Log("1Target is:" + _target);
        }

        public void Move()
        {
            Vector3 directionToPlayer = (_target.position - _initialPosition).normalized;
            transform.position += directionToPlayer * barrelSpeed * Time.deltaTime;
        }

  

        public override void TakeDamage(int damageAmount)
        {
            //Debug.Log("take damage enemy:" + damageAmount);
            _barrelHealth.takeDamage(damageAmount); // Reduce barrel's health when it takes damage
            if (_barrelHealth.GetCurrentHealth() <= 0)
            {
                Die(); // Destroy the barrel when its health reaches zero
                //BarrelFactory.SetBarrelSpawned(false); // Notify the barrel factory
                DisplayUIManager.AddScore(ScoreValue); // Add score when the barrel is destroyed
            }
        }

        void Destroy()
        {
            Destroy(gameObject); // Destroy the barrel
            EnemyManager.IsEnemyOnScene(false); // Notify the EnemyManager that an enemy is no longer on the scene
        }

        public int CurrentBarrelHealth => _barrelHealth.GetCurrentHealth(); // Get the current health of the barrel
        public void Rotate()
        {
            _rb.angularVelocity = barrelRotationSpeed;
        }
    }
}
