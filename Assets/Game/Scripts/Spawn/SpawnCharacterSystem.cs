using DZM.Player;
using DZM.Weapon;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DZM.Spawn
{
    [BurstCompile]
    public partial struct SpawnCharacterSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SpawnCharacterResourcesComponentData>();

            // Auto-create singleton
            var randomSeed = (uint)System.DateTime.Now.Millisecond;
            var singletonEntity = state.EntityManager.CreateEntity();
            state.EntityManager.AddComponentData(singletonEntity, new SpawnSystemComponentData
            {
                Random = Random.CreateFromIndex(randomSeed),
            });
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            ref var systemComponentData = ref SystemAPI.GetSingletonRW<SpawnSystemComponentData>().ValueRW;
            var gameResources = SystemAPI.GetSingleton<SpawnCharacterResourcesComponentData>();
            HandleSpawnCharacter(ref state, ref systemComponentData, gameResources);
        }

        private void HandleSpawnCharacter(ref SystemState state, ref SpawnSystemComponentData systemComponentData,
            SpawnCharacterResourcesComponentData gameResources)
        {
            if (SystemAPI.QueryBuilder().WithAll<CharacterSpawnRequest>().Build().CalculateEntityCount() <= 0)
            {
                return;
            }

            var ecb = SystemAPI.GetSingletonRW<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .ValueRW.CreateCommandBuffer(state.WorldUnmanaged);

            var spawnPointsQuery = SystemAPI.QueryBuilder().WithAll<SpawnPointComponentData, LocalToWorld>().Build();
            var spawnPointLtWs = spawnPointsQuery.ToComponentDataArray<LocalToWorld>(Allocator.Temp);
            foreach (var (_, entity) in SystemAPI.Query<RefRW<CharacterSpawnRequest>>().WithEntityAccess())
            {
                // Set player data
                var playerEntity = ecb.Instantiate(gameResources.PlayerGhost);
                var player = SystemAPI.GetComponent<PlayerComponentData>(gameResources.PlayerGhost);
                ecb.SetComponent(playerEntity, player);
                
                // Find spawn point
                var randomSpawnPointIndex = systemComponentData.Random.NextInt(0, spawnPointLtWs.Length - 1);
                var randomSpawnPosition = spawnPointLtWs[randomSpawnPointIndex].Position;

                // Spawn character
                var characterEntity = ecb.Instantiate(gameResources.CharacterGhost);
                ecb.SetComponent(characterEntity, LocalTransform.FromPosition(randomSpawnPosition));

                player.Character = characterEntity;
                ecb.SetComponent(playerEntity, player);

                // Spawn & assign starting weapon
                var randomWeaponPrefab = GetRandomWeapon(systemComponentData, gameResources);
                var weaponEntity = ecb.Instantiate(randomWeaponPrefab);
                ecb.SetComponent(characterEntity, new ActiveWeaponComponentData { Entity = weaponEntity });

                ecb.DestroyEntity(entity);
            }

            spawnPointLtWs.Dispose();
        }

        private Entity GetRandomWeapon(SpawnSystemComponentData systemComponentData,
            SpawnCharacterResourcesComponentData gameResources) =>
            systemComponentData.Random.NextInt(0, 4) switch
            {
                0 => gameResources.MachineGunGhost,
                1 => gameResources.RailgunGhost,
                2 => gameResources.RocketLauncherGhost,
                3 => gameResources.ShotgunGhost,
                _ => default
            };
    }
}