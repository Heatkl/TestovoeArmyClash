namespace Game.Factory
{
    using Game.Data;
    using Game.Model;
    using UnityEngine;
    using System.Collections.Generic;

    public class ArmyFactory
    {
        private readonly UnitBaseStatsSO _baseStats;

        public ArmyFactory(UnitBaseStatsSO baseStats)
        {
            _baseStats = baseStats;
        }

        public List<UnitSpawnData> CreateArmy(int count,
            FormModifierSO[] forms,
            ColorModifierSO[] colors,
            SizeModifierSO[] sizes)
        {
            var result = new List<UnitSpawnData>(count);

            for (int i = 0; i < count; i++)
            {
                var form = forms[Random.Range(0, forms.Length)];
                var color = colors[Random.Range(0, colors.Length)];
                var size = sizes[Random.Range(0, sizes.Length)];

                var stats = BuildStats(form, color, size);
                var model = new UnitModel(stats);

                result.Add(new UnitSpawnData
                {
                    Model = model,
                    Form = form,
                    Color = color,
                    Size = size
                });
            }

            return result;
        }

        private UnitStats BuildStats(StatModifierSO f, StatModifierSO c, StatModifierSO s)
        {
            float hp = _baseStats.baseHP + f.hpModifier + c.hpModifier + s.hpModifier;
            float atk = _baseStats.baseATK + f.atkModifier + c.atkModifier + s.atkModifier;
            float spd = _baseStats.baseSpeed + f.speedModifier + c.speedModifier + s.speedModifier;
            float cd = _baseStats.baseAttackSpeed + f.attackSpeedModifier + c.attackSpeedModifier + s.attackSpeedModifier;

            return new UnitStats(hp, atk, spd, cd);
        }
    }
}
