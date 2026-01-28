namespace Game.Data
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Unit/Base Stats")]
    public class UnitBaseStatsSO : ScriptableObject
    {
        public float baseHP = 100;
        public float baseATK = 10;
        public float baseSpeed = 10;
        public float baseAttackSpeed = 1;
    }
}
