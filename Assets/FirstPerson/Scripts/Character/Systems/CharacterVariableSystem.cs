using Unity.Burst;
using Unity.CharacterController;
using Unity.Entities;
using Unity.Physics;

namespace FP.Core.Character
{
    [BurstCompile]
    [UpdateInGroup(typeof(KinematicCharacterVariableUpdateGroup))]
    public partial struct CharacterVariableSystem : ISystem
    {
        private EntityQuery _characterQuery;
        private CharacterUpdateContext _context;
        private KinematicCharacterUpdateContext _baseContext;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _characterQuery = KinematicCharacterUtilities.GetBaseCharacterQueryBuilder()
                .WithAll<CharacterData, CharacterControlData>()
                .Build(ref state);

            _context = new CharacterUpdateContext();
            _context.OnSystemCreate(ref state);
            _baseContext = new KinematicCharacterUpdateContext();
            _baseContext.OnSystemCreate(ref state);

            state.RequireForUpdate(_characterQuery);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            _context.OnSystemUpdate(ref state);
            _baseContext.OnSystemUpdate(ref state, SystemAPI.Time, SystemAPI.GetSingleton<PhysicsWorldSingleton>());

            var job = new UpdateJob
            {
                Context = _context,
                BaseContext = _baseContext,
            };
            job.ScheduleParallel();
        }
    }
}