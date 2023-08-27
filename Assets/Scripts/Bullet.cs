using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids2
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletDestroyTime = 3f;
        [SerializeField] private GameObject gameObject;

        private void Start()
         {
             Destroy(gameObject, bulletDestroyTime);
         }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Asteroid"))
            {
                Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
                if (asteroid != null)
                {
                    asteroid.TakeDamage(10);
                    Debug.Log("Damage 10");
                }
                
                Destroy(gameObject);
            }
        }
    }
}