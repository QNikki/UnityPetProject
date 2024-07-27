using DZM.Character;
using Unity.Burst;
using Unity.Entities;

namespace DZM.Player
{
    [BurstCompile]
    [WithAll(typeof(Simulate))]
    public partial struct PlayerControlVariableJob : IJobEntity
    {
        public ComponentLookup<CharControlComponentData> CharacterControlLookup;

        private void Execute(in PlayerInputComponentData inputComponentData, in PlayerComponentData playerComponent)
        {
            // Character
            if (!CharacterControlLookup.HasComponent(playerComponent.Character))
            {
                return;
            }

            // Look
            var characterControl = CharacterControlLookup[playerComponent.Character];
            characterControl.LookYawPitchDegrees.x = inputComponentData.LookDelta.x;
            characterControl.LookYawPitchDegrees.y = inputComponentData.LookDelta.y;
            CharacterControlLookup[playerComponent.Character] = characterControl;
        }
    }
    
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(FixedStepSimulationSystemGroup))]
    public partial struct PlayerControlVariableSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate(SystemAPI.QueryBuilder().WithAll<PlayerComponentData, PlayerInputComponentData>().Build());
        }

        public void OnUpdate(ref SystemState state)
        {
            var job = new PlayerControlVariableJob()
            {
                CharacterControlLookup = SystemAPI.GetComponentLookup<CharControlComponentData>(false),
            };

            state.Dependency = job.Schedule(state.Dependency);
        }
    }
}