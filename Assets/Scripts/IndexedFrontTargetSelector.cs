namespace Game.Model
{
    using System.Collections.Generic;

    public class IndexedFrontTargetSelector : ITargetSelector
    {
        private readonly IReadOnlyList<UnitModel> _ownArmy;
        private readonly IReadOnlyList<UnitModel> _enemyArmy;

        public IndexedFrontTargetSelector(IReadOnlyList<UnitModel> ownArmy, IReadOnlyList<UnitModel> enemyArmy)
        {
            _ownArmy = ownArmy;
            _enemyArmy = enemyArmy;
        }

        public UnitModel SelectTarget(UnitModel self, IReadOnlyList<UnitModel> enemies)
        {
            if (self.Attacker != null) {
                if(self.Attacker.IsAlive)
                    return self.Attacker;
                else
                    self.ClearAttacker();
            }

            int myIndex = GetIndex(self, _ownArmy);
            if (myIndex < 0) return null;

            int enemyCount = _enemyArmy.Count;

            if (myIndex < enemyCount && _enemyArmy[myIndex].IsAlive)
                return _enemyArmy[myIndex];

            for (int offset = 1; offset < enemyCount; offset++)
            {
                int lower = myIndex - offset;
                int upper = myIndex + offset;

                bool lowerValid = lower >= 0;
                bool upperValid = upper < enemyCount;

                if (lowerValid && _enemyArmy[lower].IsAlive)
                    return _enemyArmy[lower];

                if (upperValid && _enemyArmy[upper].IsAlive)
                    return _enemyArmy[upper];

                if (!lowerValid && !upperValid)
                    break;
            }

            return null;
        }


        private int GetIndex(UnitModel unit, IReadOnlyList<UnitModel> list)
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i] == unit)
                    return i;
            return -1;
        }
    }
}
