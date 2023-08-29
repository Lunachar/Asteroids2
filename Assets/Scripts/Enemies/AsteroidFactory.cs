using UnityEngine;

namespace Asteroids2
{
    public class AsteroidFactory : IEnemyFactory
    {
        public GameObject asteroidPrefab;
        public Enemy CreateEnemy()
        {
            GameObject asteroidObject = Object.Instantiate(asteroidPrefab);
            Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
            return asteroid;
        }
        
    }
}