using Unity.Entities;
using UnityEngine;

namespace DZM.Camera
{
    [DisallowMultipleComponent]
    public class PlayerCameraAuthoring : MonoBehaviour
    {
        [field: SerializeField] private float FOV { get; set; } = 75f;

        public class Baker : Baker<PlayerCameraAuthoring>
        {
            public override void Bake(PlayerCameraAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerCameraComponentData(authoring.FOV));
            }
        }
    }
}