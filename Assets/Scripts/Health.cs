namespace Asteroids2
{
    public class Health
    {
        private int _currentHealth;
        

        public Health(int initialHealth)
        {
            _currentHealth = initialHealth;
        }

        public void TakeDamage(int damageAmount)
        {
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