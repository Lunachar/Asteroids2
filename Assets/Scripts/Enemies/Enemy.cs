using System.Collections;
using System.Collections.Generic;
using Asteroids2;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public static IEnemyFactory Factory;
    public Health Health { get; protected set; }
    public abstract int ScoreValue { get; }

    public abstract void SetTarget();

    public void TakeDamage(int damageAmount)
    {
        Health.TakeDamage(damageAmount);
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
