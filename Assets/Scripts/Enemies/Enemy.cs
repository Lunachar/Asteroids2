using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids2;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Health Health { get; set; }
    public abstract int ScoreValue { get; }

    public abstract void SetTarget();

    public abstract void TakeDamage(int damageAmount);


    public void Die()
    {
        Destroy(gameObject);
        EnemyManager.Instance.IsEnemyOnScene();
    }
}
