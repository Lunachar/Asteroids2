using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids2;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Health Health { get; set; }
    public abstract int ScoreValue { get; }

    
    // private void Start()
    // {
    //     Health = new Health(100);
    // }

    public abstract void SetTarget();

    public abstract void TakeDamage(int damageAmount);

    public void Die()
    {
        Destroy(gameObject);
    }
}
