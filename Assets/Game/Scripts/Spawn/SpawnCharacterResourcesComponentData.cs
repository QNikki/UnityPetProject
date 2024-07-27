using Unity.Entities;

namespace DZM.Spawn
{
    public struct SpawnCharacterResourcesComponentData: IComponentData
    {
        public float RespawnTime;
        
        public Entity PlayerGhost;
        
        public Entity CharacterGhost;
        
        public Entity RailgunGhost;
        
        public Entity MachineGunGhost;
        
        public Entity RocketLauncherGhost;
        
        public Entity ShotgunGhost;

        public Entity SpectatorPrefab;
    }
}