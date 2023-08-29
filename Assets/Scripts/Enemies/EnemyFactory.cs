using UnityEngine;

namespace Asteroids2
{
    public class EnemyFactory : MonoBehaviour, IEnemyFactory
    {
        private Transform[] spawnPoints;
        private GameObject enemyPrefab;
    
        public EnemyFactory(Transform[] spawnPoints, GameObject prefab)
        {
            this.spawnPoints = spawnPoints;
            enemyPrefab = prefab;
        }
    
        public Enemy CreateEnemy()
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];
            //enemyPrefab.transform.position = spawnPoint.position;

            GameObject enemyObject = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            return enemyObject.GetComponent<Enemy>();
        }
    }
}