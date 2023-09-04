using UnityEngine;

namespace Asteroids2
{
    public class EnemyFactory : MonoBehaviour, IEnemyFactory
    {
        private Transform[] _spawnPoints;   // Array of spawn points for enemies
        private GameObject _enemyPrefab;   // Prefab for creating enemies

        // Initialize the EnemyFactory with spawn points and enemy prefab
        public void Initialize(Transform[] spawnPoints, GameObject prefab)
        {
            _spawnPoints = spawnPoints;    // Assign the spawn points
            _enemyPrefab = prefab;        // Assign the enemy prefab
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
    }
}