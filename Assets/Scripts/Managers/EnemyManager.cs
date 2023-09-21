using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids2
{
    public class EnemyManager : MonoBehaviour
    {
        public Transform[] spawnPoints; // Array of spawn points for enemies
        public GameObject asteroidPrefab; // Prefab for enemy
        public GameObject barrelPrefab; // Prefab for enemy
        public GameObject bossPrefab;
        public EnemyFactory enemyFactory; // Reference to the enemy factory
        public static EnemyManager Instance { get; private set; } // Singleton instance of the EnemyManager

        private GameObject _enemyPrefab; // Prefab for enemy objects

        internal static bool _isEnemyOnScene;  // Flag to track if an enemy is currently on the scene

        

        private void Awake()
        {
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
            _enemyPrefab = asteroidPrefab;
            // Initialize the enemy factory with spawn points and enemy prefab
            enemyFactory = gameObject.AddComponent<EnemyFactory>();
            enemyFactory.Initialize(spawnPoints, _enemyPrefab);
        }

        private void Update()
        {
            // Debug.Log($"Score: {ScoreManager.GetScore()}");
            // Debug.Log($"spawnAsteroids:  {spawnAsteroids}");
            // Debug.Log($"_isEnemyOnScene:  {_isEnemyOnScene.ToString()}");
        }

        // Reset the flag to indicate no enemy is on the scene
        public static void IsEnemyOnScene(bool value)
        {
            _isEnemyOnScene = value;
        }
    }
}
