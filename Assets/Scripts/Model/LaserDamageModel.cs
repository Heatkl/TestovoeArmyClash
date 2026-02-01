namespace Game.Combat
{
    using Game.Model;
    using UnityEngine;

    public class LaserDamageModel
    {
        private readonly float _tickDamage = 10f;
        private readonly float _tickInterval = 0.1f;

        private float _timer;

        public void Tick(float dt, UnitModel target)
        {
            if (target == null || target.IsDead)
                return;

            _timer += dt;

            if (_timer >= _tickInterval)
            {
                _timer = 0f;
                target.TakeDamage(_tickDamage);
            }
        }

        public void ResetTimer()
        {
            _timer = 0f;
        }
    }
}
