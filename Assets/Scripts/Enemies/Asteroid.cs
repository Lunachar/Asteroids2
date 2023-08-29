using System;
using UnityEngine;

namespace Asteroids2
{
    public class Asteroid : Enemy, IMove
    {
        private Health _asteroidHealth;
        private Transform _target;
        private Rigidbody2D _rb;
        public float asteroidDestroyTime;
        public AsteroidHealthUI asteroidHealthUI;

        public float asteroidSpeed = 1f;

        public override int ScoreValue => 100;


        private void Start()
        {
            _asteroidHealth = new Health(100);
            Destroy(gameObject, asteroidDestroyTime);
            _rb = GetComponent<Rigidbody2D>();
            SetTarget();
            FaceTarget();
            Move();
            //asteroidHealthUI.SetAsteroidHealth(asteroidHealth);
        }

        private void Update()
        {
            asteroidHealthUI.UpdateHealthText();
            
        }

        private void FaceTarget()
        {
            Vector2 direction = _target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rb.rotation = angle;
        }

        public override void SetTarget()
        {
            _target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            Debug.Log("1Target is:" + _target);
        }

        public void Move()
        {
            Debug.Log("2Target is:" + _target);
            var asteroidPosition = transform.position;
            var playerPosition = _target.position;
            var direction = playerPosition - asteroidPosition;
            _rb.AddForce(direction.normalized * asteroidSpeed, ForceMode2D.Impulse);
        }
        
        public override void TakeDamage(int damageAmount)
        {
            Debug.Log("take damage enemy:" + damageAmount);
            _asteroidHealth.takeDamage(damageAmount);
            if (_asteroidHealth.GetCurrentHealth() <= 0)
            {
                Die();
                ScoreManager.AddScore(ScoreValue);
            }
        }


        public int CurrentHealth => _asteroidHealth.GetCurrentHealth();
    }
}