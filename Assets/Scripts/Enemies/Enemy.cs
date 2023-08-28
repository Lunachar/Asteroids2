using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids2;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Health Health { get; set; }
    public abstract int ScoreValue { get; }

    
    private void Start()
    {
        Health = new Health(100);
    }

    public abstract void SetTarget();

    public void TakeDamage(int damageAmount)
    {
        Debug.Log("take damage enemy:" + damageAmount);
        Health.takeDamage(damageAmount);
        if (Health.GetCurrentHealth() <= 0)
        {
            Die();
            ScoreManager.AddScore(ScoreValue);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
