﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids2
{
    // Define a delegate for the barrelDestroyed event
    delegate void BarreldDestroyedCallback();

    public class Barrel : Enemy, IMove
    {
        private Health _barrelHealth;     // Health component for the barrel
        private Transform _target;          // Target (e.g., player) for the barrel to follow
        private Rigidbody2D _rb;            // Rigidbody component for physics interactions
        private float _barrelDestroyTime; // Time when the barrel should be destroyed
        private float _startTime;           // Time when the barrel was created

        //public barrelHealthUI barrelHealthUI; // UI component for displaying barrel health
        public float barrelSpeed = 1f;         // Speed at which the barrel moves
        public override int ScoreValue => 100;   // Score value awarded when the barrel is destroyed

        private void Start()
        {
            _startTime = Time.time;
            _barrelHealth = new Health(100); // Initialize the barrel's health
            _rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
            SetTarget();                        // Set the target for the barrel
            FaceTarget();                      // Rotate the barrel to face the target
            Move();                            // Move the barrel towards the target
        }

        private void Update()
        {
            //barrelHealthUI.UpdateHealthText(); // Update the UI for barrel health
            
            float currentTime = Time.time;
            float elapsedTime = currentTime - _startTime;

            if (elapsedTime >= 6f)
            {
                if (gameObject != null)
                {
                    Destroy(); // Destroy the barrel when the specified time has passed
                }
            }
        }

        private void FaceTarget()
        {
            Vector2 direction = _target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rb.rotation = angle; // Rotate the barrel to face the target
        }

        public override void SetTarget()
        {
            _target = GameObject.FindWithTag("Player").GetComponent<Transform>(); // Find and set the player as the target
            //Debug.Log("1Target is:" + _target);
        }

        public void Move()
        {
            //Debug.Log("2Target is:" + _target);
            var barrelPosition = transform.position;
            var playerPosition = _target.position;
            var direction = playerPosition - barrelPosition;
            _rb.AddForce(direction.normalized * barrelSpeed, ForceMode2D.Impulse); // Move the barrel towards the player
        }

        public override void TakeDamage(int damageAmount)
        {
            //Debug.Log("take damage enemy:" + damageAmount);
            _barrelHealth.takeDamage(damageAmount); // Reduce barrel's health when it takes damage
            if (_barrelHealth.GetCurrentHealth() <= 0)
            {
                Die(); // Destroy the barrel when its health reaches zero
                BarrelFactory.SetBarrelSpawned(false); // Notify the barrel factory
                ScoreManager.AddScore(ScoreValue); // Add score when the barrel is destroyed
            }
        }

        void Destroy()
        {
            Destroy(gameObject); // Destroy the barrel
            EnemyManager.IsEnemyOnScene(); // Notify the EnemyManager that an enemy is no longer on the scene
        }

        public int CurrentHealth => _barrelHealth.GetCurrentHealth(); // Get the current health of the barrel
    }
}
