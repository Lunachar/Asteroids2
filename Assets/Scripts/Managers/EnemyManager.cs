using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids2;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    
    public IEnemyFactory enemyFactory;

    private void Start()
    {
        enemyFactory = new EnemyFactory(spawnPoints, enemyPrefab);
        
        AsteroidFactory asteroidFactory = enemyFactory as AsteroidFactory;
        if (asteroidFactory != null)
        {
            asteroidFactory.asteroidPrefab = enemyPrefab;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = this.spawnPoints[randomIndex];
        Enemy enemy = enemyFactory.CreateEnemy();
        enemy.transform.position = spawnPoint.position;
    }
}
