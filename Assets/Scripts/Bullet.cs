using System;
using System.Collections;
using UnityEngine;

namespace Asteroids2
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletDestroyTime = 3f;
        [SerializeField] private int damageAmount = 10;

        private void Start()
        {
            Destroy(gameObject, bulletDestroyTime);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("OnTriggerEnter +");
            if (col.gameObject.CompareTag("Asteroid"))
            {
                Asteroid asteroid = col.gameObject.GetComponent<Asteroid>();
                if (asteroid != null)
                {
                    asteroid.TakeDamage(damageAmount);
                    Debug.Log("Damage 10");
                }
                else
                {
                    Debug.Log("Asteroid not found!");
                }
                
                Destroy(gameObject);
            }
        }
    }
}