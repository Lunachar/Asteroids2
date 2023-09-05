using UnityEngine;

namespace Asteroids2
{
    public class BarrelFactory : EnemyFactory
    {
        public GameObject barrelPrefab; // Prefab for creating barrels
        private static bool _isBarrelSpawned; // Flag to track if a barrel is spawned

        // Override the base class method to create a barrel
        public override Enemy CreateEnemy()
        {
            EnemyManager.IsEnemyOnScene(); // Notify the EnemyManager that an enemy is on the scene

            // Instantiate the barrel object
            GameObject barrelObject = Instantiate(barrelPrefab);

            Debug.Log("Barrel spawned.");

            // Get the Barrel component of the created barrel object
            Barrel barrel = barrelObject.GetComponent<Barrel>();

            return barrel; // Return the created barrel
        }

        // Static method to check if a barrel is spawned
        public static bool IsAsteroidBarrelSpawned()
        {
            return _isBarrelSpawned;
        }

        // Static method to set the barrel spawn flag
        public static void SetBarrelSpawned(bool value)
        {
            _isBarrelSpawned = value;
        }
    }
}