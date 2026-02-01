using UnityEngine;
using Game.Data;
using Game.Factory;
using Game.Model;
using Game.Controller;
using Game.View;
using Game.Battle;
using System.Collections.Generic;

public class BattleBootstrap : MonoBehaviour
{
    public UnitBaseStatsSO baseStats;
    public FormModifierSO[] forms;
    public ColorModifierSO[] colors;
    public SizeModifierSO[] sizes;

    public BattleController battleController;

    private readonly Dictionary<UnitModel, UnitController> _lookup = new();

    void Start()
    {
        var factory = new ArmyFactory(baseStats);

        var armyAData = factory.CreateArmy(10, forms, colors, sizes);
        var armyBData = factory.CreateArmy(10, forms, colors, sizes);

        var armyA = new ArmyModel();
        var armyB = new ArmyModel();

        var selectorA = new IndexedFrontTargetSelector(armyA.Units, armyB.Units);
        var selectorB = new IndexedFrontTargetSelector(armyB.Units, armyA.Units);

        var controllersA = SpawnArmy(armyAData, armyA, new Vector3(-8, 0, 0), selectorA);
        var controllersB = SpawnArmy(armyBData, armyB, new Vector3(8, 0, 0), selectorB);


        battleController.Init(armyA, armyB, controllersA, controllersB, _lookup);
    }

    List<UnitController> SpawnArmy(List<UnitSpawnData> data, ArmyModel army, Vector3 startPos, ITargetSelector selector)
    {
        var list = new List<UnitController>();

        float spacing = 2.5f;
        int rowCount = data.Count;

        for (int i = 0; i < rowCount; i++)
        {
            float offsetZ = (i - rowCount / 2f) * spacing;
            var pos = startPos + new Vector3(0, 0, offsetZ);

            if (data[i].Form.prefab == null)
            {
                Debug.LogError($"Missing prefab in {data[i].Form.name}");
                continue;
            }

            var view = Instantiate(data[i].Form.prefab, pos, Quaternion.identity);
            view.Bind(data[i].Model);
            view.ApplyVisual(data[i].Color.unitColor, data[i].Size.sizeScale);

            var controller = new UnitController(data[i].Model, view, selector);

            army.AddUnit(data[i].Model);
            _lookup[data[i].Model] = controller;
            list.Add(controller);
        }

        return list;
    }

}
