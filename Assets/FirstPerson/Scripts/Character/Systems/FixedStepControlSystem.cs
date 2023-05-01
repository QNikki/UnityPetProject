using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.CharacterController;
using UnityEngine;

namespace FP.Core.Character
{
    [BurstCompile]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup), OrderFirst = true)]
    public partial struct FixedStepControlSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<FixedTickSystem.Singleton>();
            state.RequireForUpdate(SystemAPI.QueryBuilder().WithAll<PlayerData, InputData>().Build());
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            uint fixedTick = SystemAPI.GetSingleton<FixedTickSystem.Singleton>().Tick;
            foreach (var (playerInputs, player) in SystemAPI.Query<RefRW<InputData>, PlayerData>()
                         .WithAll<Simulate>())
            {
                var characterControl = SystemAPI.GetComponent<CharacterControlData>(player.Character);
                var playerRotation = SystemAPI.GetComponent<LocalTransform>(player.Character).Rotation;
                float3 characterUp = MathUtilities.GetUpFromRotation(playerRotation);
                quaternion cameraRotation = SystemAPI.GetComponent<LocalTransform>(player.Camera).Rotation;
                float3 cameraForward = MathUtilities.GetForwardFromRotation(cameraRotation);
                float3 cameraForwardOnUpPlane = math.normalizesafe(cameraForward, characterUp);
                float3 cameraRight = MathUtilities.GetRightFromRotation(cameraRotation);

                // Move
                characterControl.MoveVector = playerInputs.ValueRW.Move.y * cameraForwardOnUpPlane +
                                              playerInputs.ValueRW.Move.x * cameraRight;
                
                characterControl.MoveVector = MathUtilities.ClampToMaxLength(characterControl.MoveVector, 1f);

                // Jump
                // We detect a jump event if the jump counter has changed since the last fixed update.
                // This is part of a strategy for proper handling of button press events that are consumed during the fixed update group
                characterControl.Jump = playerInputs.ValueRW.JumpPressed.IsSet(fixedTick);
                SystemAPI.SetComponent(player.Character, characterControl);
            }
        }
    }
}