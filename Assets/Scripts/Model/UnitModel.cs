namespace Game.Model
{
    using System;

    public class UnitModel : IDamageable, ITargetable
    {
        public UnitStats Stats { get; }
        public UnitModel Target { get; private set; }
        public UnitModel Attacker { get; private set; }

        public bool IsDead => Stats.IsDead;
        public bool IsAlive => !Stats.IsDead;

        public event Action<UnitModel> OnDeath;

        public UnitModel(UnitStats stats)
        {
            Stats = stats;
        }

        public void SetTarget(UnitModel target) => Target = target;
        public void ClearAttacker() => Attacker = null;

        public void TakeDamage(float amount, UnitModel attacker = null)
        {
            if (IsDead) return;

            if (attacker != null && attacker.IsAlive)
                Attacker = attacker;

            Stats.ApplyDamage(amount);

            if (Stats.IsDead)
                OnDeath?.Invoke(this);
        }

    }
}
