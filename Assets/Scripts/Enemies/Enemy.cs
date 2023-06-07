using System.Collections;
using System.Collections.Generic;
using Asteroids2;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public static IEnemyFactory Factory;
    public Health Health { get; protected set; }

    public abstract void SetTarget();

    public void DependencyInjectHealth(Health hp)
    {
        Health = hp;
    }
}
