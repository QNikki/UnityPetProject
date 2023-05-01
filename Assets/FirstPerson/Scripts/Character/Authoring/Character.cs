using UnityEngine;
using Unity.Entities;
using Unity.Physics.Authoring;
using Unity.CharacterController;
using UnityEngine.Serialization;

namespace FP.Core.Character
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PhysicsShapeAuthoring))]
    public class Character : MonoBehaviour
    {
        public AuthoringKinematicCharacterProperties CharacterProperties =
            AuthoringKinematicCharacterProperties.GetDefault();

        [FormerlySerializedAs("Character")] 
        public CharacterData CharacterData = CharacterData.GetDefault;

        public class Baker : Baker<Character>
        {
            public override void Bake(Character authoring)
            {
                KinematicCharacterUtilities.BakeCharacter(this, authoring, authoring.CharacterProperties);
                AddComponent(GetEntity(TransformUsageFlags.None), authoring.CharacterData);
                AddComponent(GetEntity(TransformUsageFlags.None), new CharacterControlData());
            }
        }
    }
}