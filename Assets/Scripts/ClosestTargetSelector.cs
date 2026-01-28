using System.Collections.Generic;

namespace Game.Model
{
    public class ClosestTargetSelector : ITargetSelector
    {
        public UnitModel SelectTarget(UnitModel self, IReadOnlyList<UnitModel> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].IsAlive)
                    return enemies[i]; // упрощённо: первый живой
            }
            return null;
        }
    }
}
