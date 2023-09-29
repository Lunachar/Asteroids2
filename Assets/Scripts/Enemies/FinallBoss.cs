using System;
using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids2
{
    [RequireComponent(typeof(LineRenderer))]
    public class FinallBoss : Enemy, IMove, IRotation, IShoot
    {
        public Transform laserOrign;
        public float gunRange = 50f;
        public float fireRate = 1f;
        public float laserDuration = 0.05f;
        public TextMeshProUGUI bossHp;

        private LineRenderer laserLine;
        private float fireTimer;
        private Health _bossHealth;
        private Transform _target;
        private Rigidbody2D _rb;
        private float _startTime;
        private Vector3 _initialPosition;
        private int _bossHp;

        private bool _isMoving = true;
        private static bool _isShooting = false;

        [SerializeField]private float _moveSpeed = 2.0f;
        private float _rotationSpeed = 0.5f;
        private float _shootCooldown = 10f;
        private float _nextShootTime = 1;
        private int _shootNumber;

        public GameManager gameManager;
        private float _musicLenght;
        private bool hasWon;
        


        public override int ScoreValue => 500;

        private void Awake()
        {
            laserLine = GetComponent<LineRenderer>();
            gameManager = GetComponent<GameManager>();
        }

        void Start()
        {
            _startTime = Time.time;
            _bossHealth = new Health(500);
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            _initialPosition = transform.position;

            SetTarget(); // Set the target for the Boss

            Move();
            Shoot();

            Rotate();

            _bossHp = _bossHealth.GetCurrentHealth();
            bossHp.text = _bossHp.ToString();

        }



        public override void SetTarget()
        {
            _target = GameObject.FindWithTag("Player")
                .GetComponent<Transform>(); // Find and set the player as the target
        }

        private void FaceTarget()
        {
            Vector2 direction = _target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rb.rotation = angle; // Rotate the boss to face the target
        }


        public override void TakeDamage(int damageAmount)
        {
            _bossHealth.takeDamage(damageAmount); // Reduce Boss's health when it takes damage
            if (_bossHealth.GetCurrentHealth() <= 0)
            {
                gameManager.boosDieFlag = true;
                Debug.LogError($"{gameManager.boosDieFlag.ToString()}");
                Die(); // Destroy the Bos when its health reaches zero
                DisplayUIManager.AddScore(ScoreValue); // Add score when the Boss is destroyed
            }
        }

        public void Move()
        {
            if (Vector3.Distance(transform.position, _target.position) > 7f)
            {
                Vector3 directionToPlayer = (_target.position - _initialPosition).normalized;
                transform.position += directionToPlayer * _moveSpeed * Time.deltaTime;
                Debug.Log($"STOP SHOT. BEGIN TO MOVE");
                _isShooting = false;
                _shootNumber = 0;
            }
            else
            {
                Debug.Log($"STOP MOVE. BEGIN TO SHOT");
                _isShooting = true;

                //_nextShootTime = 0;
                //StartCoroutine(waiter());
            }
        }


        public void Rotate()
        {
            Vector3 directonToTarget = (_target.position - transform.position).normalized;

            Vector2 directionXY = new Vector2(directonToTarget.x, directonToTarget.y);

            float angle = Vector2.SignedAngle(Vector2.up, directionXY);

            Quaternion desiredRotation = Quaternion.Euler(0, 0, angle);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * _rotationSpeed);
        }

        public void Shoot()
        {
            // Vector3 rayOrigin = laserOrign.position;

            if (_isShooting && _shootNumber < 6 /* && Time.time > _nextShootTime*/)
            {

                fireTimer += Time.deltaTime;
                Debug.Log($"fireTimer: {fireTimer.ToString()}; fireRate: {fireRate.ToString()}");
                if (fireTimer > fireRate)
                {
                    Debug.Log($"fireTimer > fireRate");
                    fireTimer = 0;
                    laserLine.SetPosition(0, laserOrign.position);
                    

                    Vector2 direction = _target.position - gameObject.transform.position;
                    Vector2 rayOrigin = laserOrign.position;
                    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, gunRange);

                    if (hit.collider.gameObject == _target.gameObject)
                    {
                        laserLine.SetPosition(1, hit.point*2);
                        _target.GetComponent<PlayerModel>().PlayerHealth.takeDamage(30);
                        Debug.LogError($"takeDamage {_target.GetComponent<PlayerModel>().PlayerHealth.GetCurrentHealth().ToString()}");
                        
                    }
                    else
                    {
                        Debug.Log($"MISSING");
                        laserLine.SetPosition(1, rayOrigin + (direction * gunRange));
                    }
            
                    StartCoroutine(ShootLaser());
                }


                //_nextShootTime = Time.time + _shootCooldown;
            }
        }

        IEnumerator ShootLaser()
        {
            Debug.Log($"ShootLaser");
            laserLine.enabled = true;
            yield return new WaitForSeconds(laserDuration);
            _shootNumber++;
            laserLine.enabled = false;
        }

        IEnumerator waiter()
        {
            yield return new WaitForSeconds(2f);
        }

        private void OnDrawGizmos()
        {
            if (_target != null)
            {
                Gizmos.color = Color.red;
                Vector3 lineStart = transform.position;
                Vector3 lineEnd = _target.position;
                Gizmos.DrawLine(transform.position, _target.position);

                float distance = Vector3.Distance(lineStart, lineEnd);
                Debug.Log($"DISTANCE IS: {distance}");
            }
        }
    }
}