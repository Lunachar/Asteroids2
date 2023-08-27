using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids2
{
    public class AsteroidHealthUI : MonoBehaviour
    {
        public TextMeshProUGUI healthTextObject;
        public string healthText;
        
        private Health asteroidHealth;
        private Asteroid _asteroid;


        private void Start()
        {
            healthText = healthTextObject.text;
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
                healthText = _asteroid.CurrentHealth.ToString();
            }
            Debug.Log("Asteroid health is:" + healthText);
        }
    }
}
