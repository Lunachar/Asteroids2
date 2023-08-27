using UnityEngine;

namespace Asteroids2
{
    // public class EnemyFactory : IEnemyFactory
    // {
    //     private Transform[] spawnPoints;
    //
    //     public EnemyFactory(Transform[] spawnPoints)
    //     {
    //         this.spawnPoints = spawnPoints;
    //     }
    //
    //     public Enemy Create(Health hp)
    //     {
    //         int randomIndex = Random.Range(0, spawnPoints.Length);
    //         Transform spawnPoint = spawnPoints[randomIndex];
    //
    //         Asteroid enemy = new Asteroid();
    //         //enemy.DependencyInjectHealth(hp);
    //
    //         enemy.transform.position = spawnPoint.position;
    //
    //         return enemy;
    //     }
    // }
}