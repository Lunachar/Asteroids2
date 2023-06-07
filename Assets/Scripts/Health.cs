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