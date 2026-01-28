using System.Collections.Generic;
using System.Linq;

namespace Game.Model
{
    public class ArmyModel
    {
        private readonly List<UnitModel> _units = new List<UnitModel>(32);

        public IReadOnlyList<UnitModel> Units => _units;

        public void AddUnit(UnitModel unit) => _units.Add(unit);

        public bool IsDefeated => _units.All(u => u.IsDead);

        public UnitModel GetRandomAliveUnit()
        {
            for (int i = 0; i < _units.Count; i++)
                if (_units[i].IsAlive)
                    return _units[i];
            return null;
        }
    }
}
