using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Asteroids2
{
    public class EnemyFactory : MonoBehaviour, IEnemyFactory
    {
        private Transform[] _spawnPoints; // Array of spawn points for enemies
        private GameObject _enemyPrefab; // Prefab for creating enemies
        public static EnemyFactory Instance { get; private set; } // Singleton instance of the EnemyFactory

        public event Action OnFinallyEvent;

        // Initialize the EnemyFactory with spawn points and enemy prefab
        public void Initialize(Transform[] spawnPoints, GameObject prefab)
        {
            _spawnPoints = spawnPoints; // Assign the spawn points
            _enemyPrefab = prefab; // Assign the enemy prefab
        }

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
            Instance.OnFinallyEvent += HandleFinallyEvent;
            
        }

        private void Update()
        {
            bool spawnAsteroids = ScoreManager.GetScore() < 200;
            bool spawnBarrels = ScoreManager.GetScore() >= 200 && ScoreManager.GetScore() < 400;
            bool spawnBoss = ScoreManager.GetScore() >= 400 && ScoreManager.GetScore() <= 500;

            if (spawnAsteroids && !EnemyManager._isEnemyOnScene)
            {
                _enemyPrefab = EnemyManager.Instance.asteroidPrefab;
                CreateEnemy();
                EnemyManager.IsEnemyOnScene(true);
            }
            else if (spawnBarrels && !EnemyManager._isEnemyOnScene)
            {
                _enemyPrefab = EnemyManager.Instance.barrelPrefab;
                CreateEnemy();
                EnemyManager.IsEnemyOnScene(true);
            }
            else if (spawnBoss && !EnemyManager._isEnemyOnScene)
            {
                _enemyPrefab = EnemyManager.Instance.bossPrefab;
                StartCoroutine(ShowAndHide(FinallyDisplay.TextObject, 4f));
            }
        }


        // Create a new enemy and return it
        public virtual Enemy CreateEnemy()
        {
            // Randomly select a spawn point from the array
            int randomIndex = Random.Range(0, _spawnPoints.Length);
            Transform spawnPoint = _spawnPoints[randomIndex];

            // Instantiate the enemy object at the chosen spawn point with no rotation
            GameObject enemyObject = Instantiate(_enemyPrefab, spawnPoint.position, Quaternion.identity);

            // Return the Enemy component of the created enemy object
            return enemyObject.GetComponent<Enemy>();
        }

        IEnumerator ShowAndHide(GameObject text, float delay)
        {
            if (text != null)
            {
                text.GetComponent<Text>().enabled = true;
                yield return new WaitForSeconds(delay);
                text.GetComponent<Text>().enabled = false;

                OnFinallyEvent?.Invoke();
            }
        }

        void HandleFinallyEvent()
        {
            if (!EnemyManager._isEnemyOnScene)
            {
               CreateEnemy();
               EnemyManager.IsEnemyOnScene(true);
            }
        }
    }
}