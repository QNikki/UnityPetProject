using UnityEngine;

namespace BG.Core
{
    [CreateAssetMenu(fileName = "Modifier_", menuName = "Biome Generations/ Modifier")]
    public class ModifierConfig: ScriptableObject
    {
        [field: SerializeField] public HeightmapModifierType Type { get; set; } = default;

        [field: SerializeReference]
        public BaseHeightMapModifier Value { get; private set; } = ModifierType.GetModifier(default);

        private void OnValidate()
        {
            if (Type != Value.Type)
            {
                Value = ModifierType.GetModifier(Type);
            }
        }
    }
}