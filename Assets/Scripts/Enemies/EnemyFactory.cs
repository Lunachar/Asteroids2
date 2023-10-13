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
        internal GameObject _enemyPrefab; // Prefab for creating enemies
        public static EnemyFactory Instance { get; private set; } // Singleton instance of the EnemyFactory

        private GameManager _gameManager;

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
            _gameManager = GameObject.Find("ManagersDDOL").GetComponent<GameManager>();

        }

        private void Update()
        {
            bool spawnAsteroids = DisplayUIManager.GetScore() < 200;
            bool spawnBarrels = DisplayUIManager.GetScore() >= 200 && DisplayUIManager.GetScore() < 400;
            bool spawnBoss = DisplayUIManager.GetScore() >= 400 && DisplayUIManager.GetScore() <= 500;

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
            else if (spawnBoss && !EnemyManager._isEnemyOnScene && !_gameManager.boosDieFlag)
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