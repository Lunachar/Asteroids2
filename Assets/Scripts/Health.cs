using UnityEngine;

namespace Asteroids2
{
    public class Health
    {
        private int _currentHealth;
        

        public Health(int initialHealth)
        {
            _currentHealth = initialHealth;
        }

        public void takeDamage(int damageAmount)
        {
            Debug.Log("take damage health" +damageAmount);
            _currentHealth -= damageAmount;
            if (_currentHealth < 0)
            {
                _currentHealth = 0;
            }
        }

        public int GetCurrentHealth()
        {
            return _currentHealth;
        }
    }
}