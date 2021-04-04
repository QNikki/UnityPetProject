using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Components;
using Unity.Burst;
using Unity.Collections;

namespace Systems
{
    public class InputMovementSystem : SystemBase
    {
        private EntityQuery _queryEntities;

        protected override void OnCreate()
        {
            // Cached access to a set of ComponentData based on a specific query
            _queryEntities = GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    typeof(Translation),
                    typeof(Components.InputMovement),
                    typeof(Components.InputAbility),
                    typeof(Components.VelocityComponent),
                    typeof(Rotation),
                }
            });
        }

        protected override void OnUpdate()
        {
            if (_queryEntities.CalculateEntityCount() == 0) 
            {
                return;
            }

            NativeArray<ArchetypeChunk> chunks = _queryEntities.CreateArchetypeChunkArray(Allocator.TempJob);

        }

        protected override void OnDestroy()
        {
        }
    }
}
