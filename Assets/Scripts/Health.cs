using UnityEngine;

namespace Asteroids2
{
    public class Health
    {
        private int _currentHealth; // Current health value

        // Constructor to initialize the health with an initial value
        public Health(int initialHealth)
        {
            _currentHealth = initialHealth;
        }

        // Method to subtract damage from the current health
        public void takeDamage(int damageAmount)
        {
            Debug.Log("take damage health" + damageAmount);
            _currentHealth -= damageAmount; // Reduce current health by the specified damage amount
            if (_currentHealth < 0)
            {
                _currentHealth = 0; // Ensure that health doesn't go below zero
            }
        }

        // Method to get the current health value
        public int GetCurrentHealth()
        {
            return _currentHealth;
        }
    }
}