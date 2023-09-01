using UnityEngine;

namespace Asteroids2
{
    public class AsteroidFactory : EnemyFactory
    {
        public GameObject asteroidPrefab;
        private static bool _isAsteroidSpawned;
        public override Enemy CreateEnemy()
        {
            EnemyManager.Instance.IsEnemyOnScene();
            GameObject asteroidObject = Instantiate(asteroidPrefab);
            
            Debug.Log("Asteroid spawned.");
            Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
            return asteroid;
        }

        public static bool IsAsteroidSpawned()
        {
            return _isAsteroidSpawned;
        }

        public static void SetAsteroidSpawned(bool value)
        {
            _isAsteroidSpawned = value;
        }
    }
}