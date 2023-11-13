using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids2.Utilities
{
    public class Singleton <T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy((gameObject));
            }
        }
    }
}