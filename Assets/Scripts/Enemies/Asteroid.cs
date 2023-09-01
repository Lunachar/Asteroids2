using System;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids2
{
    public class Asteroid : Enemy, IMove
    {
        private Health _asteroidHealth;
        private Transform _target;
        private Rigidbody2D _rb;
        private float _asteroidDestroyTime;
        private float _startTime;
        
        public AsteroidHealthUI asteroidHealthUI;
        public float asteroidSpeed = 1f;
        public override int ScoreValue => 100;
        //[Serializable] public class AsteroidDestroyedEvent : UnityEvent<Asteroid> { }

        //[SerializeField] private Asteroid asteroid;
        //[SerializeField] private EnemyManager enemyManager;

        public delegate void AsteroidDestroyedCallback();

        public static event AsteroidDestroyedCallback AsteroidDestroyed;

        public event AsteroidDestroyedCallback AsDes;
            


        private void Start()
        {
            _startTime = Time.time;
            _asteroidHealth = new Health(100);
            // Destroy(gameObject, _asteroidDestroyTime);
            // asteroid.onDestroy.AddListener(enemyManager.IsEnemyOnScene());
            // Destroy();
            _rb = GetComponent<Rigidbody2D>();
            SetTarget();
            FaceTarget();
            Move();
            //asteroidHealthUI.SetAsteroidHealth(asteroidHealth);
        }

        private void Update()
        {
            asteroidHealthUI.UpdateHealthText();
            
            float currentTime = Time.time;
            float elapsedTime = currentTime - _startTime;

            if (elapsedTime >= 6f)
            {
                Destroy(gameObject);
                if (AsDes != null)
                {
                    AsDes();
                }
            }
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
                AsteroidFactory.SetAsteroidSpawned(false);
                ScoreManager.AddScore(ScoreValue);
            }
        }

        // public void Destroy()
        // {
        //     Destroy(gameObject);
        //     onDestroy.Invoke(this);
        //     onDestroy.RemoveAllListeners();
        //     EnemyManager.Instance.IsEnemyOnScene();
        // }


        public int CurrentHealth => _asteroidHealth.GetCurrentHealth();
    }
}