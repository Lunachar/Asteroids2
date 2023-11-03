using System;
using System.Collections;
using UnityEngine;

namespace Asteroids2
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletDestroyTime = 3f; // Time after which the bullet is destroyed
        [SerializeField] private int damageAmount = 10;       // Amount of damage the bullet deals
        public GameObject explosionPrefab;


        private void Start()
        {
            // Destroy the bullet game object after a specified time
            Destroy(gameObject, bulletDestroyTime);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            //Debug.Log("OnTriggerEnter +");
            if (col.gameObject.CompareTag("Enemy"))
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Enemy enemy = col.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount);
                    //Debug.Log("Damage 10");
                }
                else
                {
                    Debug.Log("Enemy not found!");
                }

                // Destroy the bullet game object on collision with an asteroid
                Destroy(gameObject);
            }
        }
    }
}