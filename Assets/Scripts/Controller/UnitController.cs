namespace Game.Controller
{
    using Game.Model;
    using Game.View;
    using System.Collections.Generic;
    using UnityEngine;

    public class UnitController
    {
        private readonly UnitModel _model;
        private readonly UnitView _view;
        private readonly ITargetSelector _targetSelector;

        private float _attackTimer;
        private float separationStrength = 100f;

        public UnitView View => _view;
        public UnitModel Model => _model;

        public UnitController(UnitModel model, UnitView view, ITargetSelector selector)
        {
            _model = model;
            _view = view;
            _targetSelector = selector;

            _model.OnDeath += HandleDeath;
        }

        public void Tick(float dt,
                 IReadOnlyList<UnitModel> enemies,
                 Dictionary<UnitModel, UnitController> lookup,
                 List<UnitController> allies)
        {
            if (_model.IsDead) return;

            UpdateTarget(enemies);

            if (_model.Target == null) return;

            var targetController = lookup[_model.Target];

            HandleMovementAndAttack(dt, targetController);

            ApplySeparation(allies);
        }


        private void UpdateTarget(IReadOnlyList<UnitModel> enemies)
        {
            if (_model.Target == null || _model.Target.IsDead)
                _model.SetTarget(_targetSelector.SelectTarget(_model, enemies));
        }

        private void HandleMovementAndAttack(float dt, UnitController target)
        {
            _view.LookAtTarget();
            float distance = _view.DistanceTo(target.View);
            float attackDistance = _view.Radius + target.View.Radius;

            if (distance > attackDistance)
            {
                // Двигаемся, пока не соприкоснёмся коллайдерами
                _view.MoveTowards(target.View.transform.position, _model.Stats.Speed);
                return;
            }

            // Мы на дистанции атаки
            _attackTimer += dt;
            if (_attackTimer >= _model.Stats.AttackCooldown)
            {
                _attackTimer = 0f;
                target.Model.TakeDamage(_model.Stats.ATK, _model);

            }
        }

        private void ApplySeparation(List<UnitController> allies)
        {
            Vector3 correction = Vector3.zero;

            for (int i = 0; i < allies.Count; i++)
            {
                var other = allies[i];
                if (other == this || other.Model.IsDead) continue;

                Vector3 delta = _view.transform.position - other.View.transform.position;
                float distance = delta.magnitude;
                float minDist = _view.Radius + other.View.Radius;

                if (distance < 0.0001f) continue;

                if (distance < minDist)
                {
                    float penetration = minDist - distance;
                    correction += delta.normalized * penetration;
                }
            }

            if (correction != Vector3.zero)
                _view.AddOffset(correction * separationStrength * Time.deltaTime);
        }




        private void HandleDeath(UnitModel obj)
        {
            obj.OnDeath -= HandleDeath;
            _view.PlayDeath();
        }
    }
}
