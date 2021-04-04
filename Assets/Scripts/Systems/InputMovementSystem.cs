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
    public partial class InputMovementSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            // Grab our DeltaTime out of the system so it is usable by the ForEach lambda
            var deltaTime = Time.DeltaTime;

            // For simple systems that only run a single job and don't need to access the JobHandle themselves,
            // it can be omitted from the Entities.ForEach() call. The job will implicitly use the system's
            // Dependency handle as its input dependency, and update the system's Dependency property to contain the scheduled
            // job's handle.
            Entities
                .WithName("MovePlayer")// ForEach name is helpful for debugging
                .ForEach((
                    ref Translation translation, // "ref" keyword makes this parameter ReadWrite
                    in MovementComponent movement) =>
                {
                    translation.Value += new float3(
                        movement.InputMovement.Movement.x,
                        0.0f,
                         movement.InputMovement.Movement.y) * 3 * deltaTime;
                }).ScheduleParallel(); // Schedule the ForEach with the job system to run
        }
    }
}
