using Unity.Burst;
using Unity.Entities;
using UnityEngine;


namespace FP.Core.Character
{
    [BurstCompile]
    [UpdateBefore(typeof(FixedStepSimulationSystemGroup))]
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    public partial struct VariableStepControlSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate(SystemAPI.QueryBuilder().WithAll<PlayerData, InputData>().Build());
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (playerInputs, player) in SystemAPI.Query<InputData, PlayerData>().WithAll<Simulate>())
            {
                if (!SystemAPI.HasComponent<CameraControlData>(player.Camera)) 
                    continue;
                
                var cameraControl = SystemAPI.GetComponent<CameraControlData>(player.Camera);
                cameraControl.FollowedEntity = player.Character;
                cameraControl.Look = playerInputs.CameraLook;
                cameraControl.Zoom = playerInputs.CameraZoom;
                SystemAPI.SetComponent(player.Camera, cameraControl);
            }
        }
    }
}