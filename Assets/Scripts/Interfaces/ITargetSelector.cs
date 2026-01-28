using System.Collections.Generic;

namespace Game.Model
{
    public interface ITargetSelector
    {
        UnitModel SelectTarget(UnitModel self, IReadOnlyList<UnitModel> enemies);
    }
}
