namespace Asteroids2
{
    public interface IEnemyFactory
    {
        Enemy Create(Health hp);
    }
}