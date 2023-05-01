using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace FP.Core.Character
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct PlayerSpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        { }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // Game init
            if (!SystemAPI.HasSingleton<PlayerSpawnData>()) return;
            ref var rwSpawnData = ref SystemAPI.GetSingletonRW<PlayerSpawnData>().ValueRW;

            // Cursor
            // TODO: remove it, or move to another system
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Spawn player
            var playerEntity = state.EntityManager.Instantiate(rwSpawnData.PlayerPrefab);
            // Spawn character at spawn point
            var character = state.EntityManager.Instantiate(rwSpawnData.CharacterPrefab);
            SystemAPI.SetComponent(character, SystemAPI.GetComponent<LocalTransform>(rwSpawnData.SpawnPoint));
            // Spawn camera
            var ecsCamera = state.EntityManager.Instantiate(rwSpawnData.CameraPrefab);
            // Assign camera & character to player
            var player = SystemAPI.GetComponent<PlayerData>(playerEntity);
            player.Character = character;
            player.Camera = ecsCamera;
            SystemAPI.SetComponent(playerEntity, player);
            state.EntityManager.DestroyEntity(SystemAPI.GetSingletonEntity<PlayerSpawnData>());
        }
        
    }
}