using UnityEngine;

namespace Asteroids2
{
    public class AsteroidFactory : IEnemyFactory
    {
        [SerializeField] public GameObject asteroidPrefab;
        public Enemy Create(Health hp)
        {
            GameObject asteroidObject = Object.Instantiate(asteroidPrefab);
            Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
            //asteroid.Health = hp;
            return asteroid;
        }
        
    }
}