using Unity.CharacterController;
using Unity.Entities;
using UnityEngine;

namespace DZM.Character
{
    [DisallowMultipleComponent]
    public class CharAuthoring : MonoBehaviour
    {
        [field: SerializeField] public GameObject ViewObject { get; set; }

        [field: SerializeField] public AuthoringKinematicCharacterProperties CharacterProperties { get; set; } =
            AuthoringKinematicCharacterProperties.GetDefault();

        // ReSharper disable once InconsistentNaming
        public CharComponentData Character = CharComponentData.GetDefault();

        public class Baker : Baker<CharAuthoring>
        {
            public override void Bake(CharAuthoring charAuthoring)
            {
                KinematicCharacterUtilities.BakeCharacter(this, charAuthoring, charAuthoring.CharacterProperties);

                var entity = GetEntity(TransformUsageFlags.Dynamic);
                charAuthoring.Character.ViewEntity = GetEntity(charAuthoring.ViewObject, TransformUsageFlags.Dynamic);
                
                AddComponent(entity, charAuthoring.Character);
                AddComponent(entity, new CharControlComponentData());
            }
        }
    }
}