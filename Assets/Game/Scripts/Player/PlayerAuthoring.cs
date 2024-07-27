using Unity.Entities;
using UnityEngine;

namespace DZM.Player
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [field: SerializeField] private GameObject ControlledCharacter { get; set; }

        [field: SerializeField] private float DefaultSensitivity { get; set; }

        private class PlayerBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new PlayerComponentData
                {
                    Character = GetEntity(authoring.ControlledCharacter, TransformUsageFlags.Dynamic),
                    LookSensitivity = authoring.DefaultSensitivity,
                });

                AddComponent<PlayerInputComponentData>(entity);
            }
        }
    }
}