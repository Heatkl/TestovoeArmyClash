namespace Game.Battle
{
    using Game.Model;
    using Game.Controller;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    public class BattleController : MonoBehaviour
    {
        public static event Action OnBattleFinished;

        private List<UnitController> _controllersA;
        private List<UnitController> _controllersB;
        private Dictionary<UnitModel, UnitController> _lookup;

        private ArmyModel _armyA;
        private ArmyModel _armyB;

        private bool _isRunning;

        public void Init(
            ArmyModel a,
            ArmyModel b,
            List<UnitController> controllersA,
            List<UnitController> controllersB,
            Dictionary<UnitModel, UnitController> lookup)
        {
            _armyA = a;
            _armyB = b;
            _controllersA = controllersA;
            _controllersB = controllersB;
            _lookup = lookup;

            _isRunning = true;
        }

        private void Update()
        {
            if (!_isRunning) return;
            RunBattleTick(Time.deltaTime);
        }

        private void RunBattleTick(float dt)
        {
            UpdateArmy(_controllersA, _armyB);
            UpdateArmy(_controllersB, _armyA);

            CheckBattleEnd();
        }

        private void UpdateArmy(List<UnitController> controllers, ArmyModel enemyArmy)
        {
            var enemies = enemyArmy.Units;

            for (int i = 0; i < controllers.Count; i++)
            {
                controllers[i].Tick(Time.deltaTime, enemies, _lookup, controllers);
            }
        }


        private void CheckBattleEnd()
        {
            if (_armyA.IsDefeated || _armyB.IsDefeated)
            {
                _isRunning = false;
                Debug.Log("Battle Finished");
                OnBattleFinished?.Invoke();
            }
        }
    }
}
