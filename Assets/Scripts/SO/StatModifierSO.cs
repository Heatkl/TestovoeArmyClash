namespace Game.Data
{
    using UnityEngine;

    public abstract class StatModifierSO : ScriptableObject
    {
        public float hpModifier;
        public float atkModifier;
        public float speedModifier;
        public float attackSpeedModifier;
    }
}