using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids2
{
    public class EnemyManager : MonoBehaviour
    {
        public Transform[] spawnPoints;
        public GameObject enemyPrefab;
        public EnemyFactory enemyFactory;
        public static EnemyManager Instance { get; private set; }

        private static bool _isEnemyOnScene = false;
        
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
            enemyFactory = gameObject.AddComponent<EnemyFactory>();
            enemyFactory.Initialize(spawnPoints, enemyPrefab);
        }

        private void Update()
        {
            if (!_isEnemyOnScene && ScoreManager.GetScore() <= 300)
            {
                AsteroidFactory asteroidFactory = enemyFactory as AsteroidFactory;
                if (asteroidFactory != null)
                {
                    asteroidFactory.asteroidPrefab = enemyPrefab;
                }

                int randomIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = this.spawnPoints[randomIndex];
                Enemy enemy = enemyFactory.CreateEnemy();
                enemy.transform.position = spawnPoint.position;

                _isEnemyOnScene = true;
            }
        }

        public static void IsEnemyOnScene()
        {
            _isEnemyOnScene = false;
        }
    }
}
