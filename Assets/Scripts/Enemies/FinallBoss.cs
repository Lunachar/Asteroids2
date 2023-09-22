using System.Collections;
using UnityEngine;

namespace Asteroids2
{
    public class FinallBoss : Enemy, IMove, IRotation, IShoot
    {
    private Health _bossHealth;
    private Transform _target;
    private Rigidbody2D _rb;
    private float _startTime;
    private Vector3 _initialPosition;

    private RaycastGun _raycastGun;
    private bool _isMoving = true;
    private bool _isShooting = false;
    private float _moveSpeed = 2.0f;
    private float _rotationSpeed = 0.5f;
    private float _shootCooldown = 10f;
    private float _nextShootTime;


    public override int ScoreValue => 500;

    void Start()
    {
        _startTime = Time.time;
        _bossHealth = new Health(500);
        _rb = GetComponent<Rigidbody2D>();
        SetTarget();                        // Set the target for the Boss

    }

    void Update()
    {
        _initialPosition = transform.position;
        if (_isMoving)
        {
            Move();
        }

        if (_isShooting)
        {
            Shoot();
        }

        Rotate();
    }
    
    public override void SetTarget()
    {
        _target = GameObject.FindWithTag("Player").GetComponent<Transform>(); // Find and set the player as the target
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
            Die(); // Destroy the Bos when its health reaches zero
            ScoreManager.AddScore(ScoreValue); // Add score when the Boss is destroyed
        }
    }

    public void Move()
    {
        if (! _isMoving)
        {
            return;
        }
        
        Vector3 directionToPlayer = (_target.position - _initialPosition).normalized;
        transform.position += directionToPlayer * _moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, _target.position) <= 5f)
        {
            _isMoving = false;
            _isShooting = true;
        }
        else
        {
            _isMoving = true;
            _isShooting = false;
        }
    }


    public void Rotate()
    {
        Vector3 directonToTarget = (_target.position - transform.position).normalized;

        Vector2 directionXY = new Vector2(directonToTarget.x, directonToTarget.y);

        float angle = Vector2.SignedAngle(Vector2.right, directionXY);
        
        Quaternion desiredRotation = Quaternion.Euler(0, 0, angle);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * _rotationSpeed);
    }

    public void Shoot()
    {
        if (! _isShooting)
        {
            return;
        }
        
        if (Time.time > _nextShootTime)
        {
            _raycastGun.StartShooting();
            _nextShootTime = Time.time + _shootCooldown;
        }
    }
}
}
