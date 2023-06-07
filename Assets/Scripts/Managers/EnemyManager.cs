using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids2;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject asteroidPrefab;
    
    public IEnemyFactory enemyFactory;

    private void Start()
    {
        enemyFactory = new AsteroidFactory();
        AsteroidFactory asteroidFactory = enemyFactory as AsteroidFactory;
        if (asteroidFactory != null)
        {
            asteroidFactory.asteroidPrefab = asteroidPrefab;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = this.spawnPoints[randomIndex];
        Health enemyHealth = new Health(100);
        Enemy enemy = enemyFactory.Create(enemyHealth);
        enemy.transform.position = spawnPoint.position;
    }
}
