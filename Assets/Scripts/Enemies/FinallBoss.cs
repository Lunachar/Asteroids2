using Asteroids2;
using UnityEngine;

public class FinallBoss : Enemy, IMove, IRotation, IShoot
{
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    public override int ScoreValue { get; }
    public override void SetTarget()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int damageAmount)
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }

    public void Rotate()
    {
        _rb.angularVelocity = 20f;
    }

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }
}
