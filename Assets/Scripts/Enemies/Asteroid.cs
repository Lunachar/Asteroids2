using System;
using UnityEngine;

namespace Asteroids2
{
    public class Asteroid : Enemy, IMove
    {
        private Transform target;
        private Rigidbody2D _rb;
        public float asteroidDestroyTime;
        public AsteroidHealthUI asteroidHealthUI;
        private Health asteroidHealth;
        
        

        private void Start()
        {
            Destroy(gameObject, asteroidDestroyTime);
            _rb = GetComponent<Rigidbody2D>();
            SetTarget();
            FaceTarget();
            Move();
            asteroidHealthUI.SetAsteroidHealth(asteroidHealth);
        }

        private void Update()
        {
            asteroidHealthUI.UpdateHealthText();
            Debug.Log("Asteroid health is:" + asteroidHealth);
        }

        private void FaceTarget()
        {
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rb.rotation = angle;
        }

        public override void SetTarget()
        {
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            Debug.Log("1Target is:" + target);
        }

        public void Move()
        {
            Debug.Log("2Target is:" + target);
            var asteroidPosition = transform.position;
            var playerPosition = target.position;
            var direction = playerPosition - asteroidPosition;
            _rb.AddForce(direction, ForceMode2D.Impulse);
        }
    }
}