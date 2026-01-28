using Game.Data;
using Game.Model;
using UnityEngine;

public static class UnitFactory
{
    public static UnitModel CreateUnit(UnitBaseStatsSO baseStats,
        StatModifierSO form, StatModifierSO color, StatModifierSO size)
    {
        float hp = baseStats.baseHP + form.hpModifier + color.hpModifier + size.hpModifier;
        float atk = baseStats.baseATK + form.atkModifier + color.atkModifier + size.atkModifier;
        float speed = baseStats.baseSpeed + form.speedModifier + color.speedModifier + size.speedModifier;
        float atkSpd = baseStats.baseAttackSpeed + form.attackSpeedModifier + color.attackSpeedModifier + size.attackSpeedModifier;

        var stats = new UnitStats(hp, atk, speed, atkSpd);
        return new UnitModel(stats);
    }
}
