using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Systems
{
    [BurstCompile]
    [UpdateAfter(typeof(InputSystem))]
    public struct InputMovementSystem : ISystemBase
    {
        private EntityQuery _queryEntities;

        public void OnCreate(ref SystemState state)
        {
            // Cached access to a set of ComponentData based on a specific query
            _queryEntities = state.GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    typeof(Translation),
                    typeof(Rotation),
                    typeof(MovementComponent),
                }
            });
        }

        public void OnUpdate(ref SystemState state)
        {
            if (_queryEntities.CalculateEntityCount() == 0) 
            {
                UnityEngine.Debug.Log("0");
                return;
            }

            ComponentTypeHandle<Rotation> handleRotation = state.GetComponentTypeHandle<Rotation>();
            ComponentTypeHandle<Translation> handleTranslation = state.GetComponentTypeHandle<Translation>();
            ComponentTypeHandle<MovementComponent> handleMove = state.GetComponentTypeHandle<MovementComponent>();

            Jobs.MovementJob job = new Jobs.MovementJob
            {
                MovementType = handleMove,
                TranslationType = handleTranslation,
                RotationType = handleRotation,

                DeltaTime = state.Time.DeltaTime,
            };

            state.Dependency = job.ScheduleParallel(_queryEntities, 1, state.Dependency);
        }

        public void OnDestroy(ref SystemState state)
        {
        }
    }
}
