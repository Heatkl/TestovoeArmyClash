namespace Game.Model
{
    public interface IDamageable
    {
        void TakeDamage(float amount, UnitModel attacker);
        bool IsDead { get; }
    }
}
