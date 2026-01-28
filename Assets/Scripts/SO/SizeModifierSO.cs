namespace Game.Data
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Unit/Modifier/Size")]
    public class SizeModifierSO : StatModifierSO
    {
        public Vector3 sizeScale = Vector3.one; 
    }
}
