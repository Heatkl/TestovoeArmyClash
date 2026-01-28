namespace Game.Data
{
    using Game.View;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Unit/Modifier/Form")]
    public class FormModifierSO : StatModifierSO
    {
        public UnitView prefab;   //Префабы форм
    }
}
