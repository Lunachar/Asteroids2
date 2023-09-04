using UnityEngine;

namespace Asteroids2
{
    public class AsteroidFactory : EnemyFactory
    {
        public GameObject asteroidPrefab; // Prefab for creating asteroids
        private static bool _isAsteroidSpawned; // Flag to track if an asteroid is spawned

        // Override the base class method to create an asteroid
        public override Enemy CreateEnemy()
        {
            EnemyManager.IsEnemyOnScene(); // Notify the EnemyManager that an enemy is on the scene

            // Instantiate the asteroid object
            GameObject asteroidObject = Instantiate(asteroidPrefab);

            Debug.Log("Asteroid spawned.");

            // Get the Asteroid component of the created asteroid object
            Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();

            return asteroid; // Return the created asteroid
        }

        // Static method to check if an asteroid is spawned
        public static bool IsAsteroidSpawned()
        {
            return _isAsteroidSpawned;
        }

        // Static method to set the asteroid spawn flag
        public static void SetAsteroidSpawned(bool value)
        {
            _isAsteroidSpawned = value;
        }
    }
}