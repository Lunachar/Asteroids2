namespace Asteroids2
{
    public class Health
    {
        private int currentHealth;
        private int maxHealth;

        public Health(int maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            
        }
    }
}