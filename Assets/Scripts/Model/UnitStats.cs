namespace Game.Model
{
    public class UnitStats
    {
        public float MaxHP { get; }
        public float CurrentHP { get; private set; }
        public float ATK { get; }
        public float Speed { get; }
        public float AttackCooldown { get; }

        public bool IsDead => CurrentHP <= 0;

        public UnitStats(float hp, float atk, float speed, float atkCd)
        {
            MaxHP = hp;
            CurrentHP = hp;
            ATK = atk;
            Speed = speed;
            AttackCooldown = atkCd;
        }

        public void ApplyDamage(float dmg)
        {
            if (IsDead) return;
            CurrentHP -= dmg;
            if (CurrentHP < 0) CurrentHP = 0;
        }
    }
}
