using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids2
{
    public class AsteroidHealthUI : MonoBehaviour
    {
        public GameObject healthTextObject;
        private Health asteroidHealth;

        private Text healthText;

        private void Start()
        {
            healthText = healthTextObject.GetComponent<Text>();
        }

        public void SetAsteroidHealth(Health health)
        {
            asteroidHealth = health;
            UpdateHealthText();
        }

        public void UpdateHealthText()
        {
            if (healthText != null && asteroidHealth != null)
            {
                healthText.text = asteroidHealth.GetCurrentHealth().ToString();
            }
        }
    }
}
