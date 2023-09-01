using UnityEngine;

namespace Asteroids2
{
    public class EnemyFactory : MonoBehaviour, IEnemyFactory
    {
        private Transform[] _spawnPoints;
        private GameObject _enemyPrefab;
    
        public void Initialize(Transform[] spawnPoints, GameObject prefab)
        {
            _spawnPoints = spawnPoints;
            _enemyPrefab = prefab;
        }
    
        public virtual Enemy CreateEnemy()
        {
            int randomIndex = Random.Range(0, _spawnPoints.Length);
            Transform spawnPoint = _spawnPoints[randomIndex];
            GameObject enemyObject = Instantiate(_enemyPrefab, spawnPoint.position, Quaternion.identity);
            
            return enemyObject.GetComponent<Enemy>();
        }
    }
}