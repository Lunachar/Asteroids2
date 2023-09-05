using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids2
{
    public class EnemyManager : MonoBehaviour
    {
        public Transform[] spawnPoints;        // Array of spawn points for enemies
        public GameObject enemyPrefab;         // Prefab for enemy objects
        public EnemyFactory enemyFactory;      // Reference to the enemy factory
        public static EnemyManager Instance { get; private set; }  // Singleton instance of the EnemyManager

        private static bool _isEnemyOnScene = false;  // Flag to track if an enemy is currently on the scene

        private void Awake()
        {
            // Singleton pattern: Ensure there is only one instance of EnemyManager
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // Initialize the enemy factory with spawn points and enemy prefab
            enemyFactory = gameObject.AddComponent<EnemyFactory>();
            enemyFactory.Initialize(spawnPoints, enemyPrefab);
        }

        private void Update()
        {
            if (!_isEnemyOnScene & ScoreManager.GetScore() < 400)
            {
                // Check if it's time to spawn a new enemy based on the score
                AsteroidFactory asteroidFactory = enemyFactory as AsteroidFactory;
                if (asteroidFactory != null)
                {
                    asteroidFactory.asteroidPrefab = enemyPrefab;
                }

                // Randomly select a spawn point
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = this.spawnPoints[randomIndex];

                // Create a new enemy and set its position to the chosen spawn point
                Enemy enemy = enemyFactory.CreateEnemy();
                enemy.transform.position = spawnPoint.position;

                _isEnemyOnScene = true; // Mark that an enemy is on the scene
            }
            else if (!_isEnemyOnScene & (ScoreManager.GetScore() >=400 & ScoreManager.GetScore() < 1200))
            {
                Debug.Log("Barrels!");
                // Check if it's time to spawn a new enemy based on the score
                BarrelFactory barrelFactory = enemyFactory as BarrelFactory;
                if (barrelFactory != null)
                {
                    barrelFactory.barrelPrefab = enemyPrefab;
                }

                // Randomly select a spawn point
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = this.spawnPoints[randomIndex];

                // Create a new enemy and set its position to the chosen spawn point
                Enemy enemy = enemyFactory.CreateEnemy();
                enemy.transform.position = spawnPoint.position;

                _isEnemyOnScene = true; // Mark that an enemy is on the scene
            }
        }

        // Reset the flag to indicate no enemy is on the scene
        public static void IsEnemyOnScene()
        {
            _isEnemyOnScene = false;
        }
    }
}
