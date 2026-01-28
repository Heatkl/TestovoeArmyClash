namespace Game.Data
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Unit/Modifier/Color")]
    public class ColorModifierSO : StatModifierSO
    {
        public Color unitColor = Color.white;
    }
}
