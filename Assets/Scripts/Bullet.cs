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

        public IEnumerator RotateBullet(Rigidbody2D bullet)
        {
            while (bullet != null && bullet.gameObject.activeSelf)
            {
                bullet.MoveRotation(bullet.rotation + 200f * Time.deltaTime);
                yield return null;
            }
        }
    }
}