using System;
using Unity.Entities;

namespace FP.Core.Character
{
    [Serializable]
    public struct PlayerSpawnData: IComponentData
    {
        public Entity SpawnPoint;
        
        public Entity CharacterPrefab;
        
        public Entity CameraPrefab;
        
        public Entity PlayerPrefab;
    }
}