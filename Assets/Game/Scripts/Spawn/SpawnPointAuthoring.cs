using Unity.Entities;
using UnityEngine;

namespace DZM.Spawn
{
    public class SpawnPointAuthoring : MonoBehaviour
    {
        public class Baker : Baker<SpawnPointAuthoring>
        {
            public override void Bake(SpawnPointAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new SpawnPointComponentData());
            }
        }
    }
}