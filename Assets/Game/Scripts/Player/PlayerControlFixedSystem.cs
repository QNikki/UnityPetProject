using DZM.Base;
using DZM.Character;
using Unity.Burst;
using Unity.CharacterController;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DZM.Player
{
    [BurstCompile]
    [WithAll(typeof(Simulate))]
    public partial struct PlayerControlFixedJob : IJobEntity
    {
        [ReadOnly] public ComponentLookup<LocalTransform> LocalTransformLookup;

        public uint Tick;

        public ComponentLookup<CharControlComponentData> CharacterControlLookup;

        private void Execute(in PlayerInputComponentData inputComponentData, in PlayerComponentData playerComponent)
        {
            // Character
            if (!CharacterControlLookup.HasComponent(playerComponent.Character))
            {
                return;
            }

            var characterControl = CharacterControlLookup[playerComponent.Character];
            var characterRotation = LocalTransformLookup[playerComponent.Character].Rotation;

            // Move
            var charForward = math.mul(characterRotation, math.forward());
            var charRight = math.mul(characterRotation, math.right());
            characterControl.MoveVector = (inputComponentData.Move.y * charForward) + (inputComponentData.Move.x * charRight);
            characterControl.MoveVector = MathUtilities.ClampToMaxLength(characterControl.MoveVector, 1f);

            // Jump
            characterControl.Jump = inputComponentData.JumpPressed.IsSet(Tick);

            CharacterControlLookup[playerComponent.Character] = characterControl;
        }
    }
    
    
    [BurstCompile]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup), OrderFirst = true)]
    public partial struct PlayerControlFixedSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<FixedTickComponentData>();
            state.RequireForUpdate(SystemAPI.QueryBuilder().WithAll<PlayerComponentData, PlayerInputComponentData>().Build());
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var tick = SystemAPI.GetSingleton<FixedTickComponentData>().Tick;
            var job = new PlayerControlFixedJob()
            {
                Tick = tick,
                LocalTransformLookup = SystemAPI.GetComponentLookup<LocalTransform>(true),
                CharacterControlLookup = SystemAPI.GetComponentLookup<CharControlComponentData>(),
            };

            state.Dependency = job.Schedule(state.Dependency);
        }
    }
}