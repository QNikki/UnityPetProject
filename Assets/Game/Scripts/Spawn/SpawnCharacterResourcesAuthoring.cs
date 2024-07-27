using Unity.Entities;
using UnityEngine;

namespace DZM.Spawn
{
    public class SpawnCharacterResourcesAuthoring : MonoBehaviour
    {
        [Header("General Parameters")] 
        [field: SerializeField] public float RespawnTime { get; private set; } = 4f;
        
        [Header("Ghost Prefabs")] 
        [field: SerializeField] public GameObject PlayerGhost { get; private set; }
        
        [field: SerializeField] public GameObject CharacterGhost { get; private set; }
        
        [field: SerializeField] public GameObject RailgunGhost { get; private set; }
        
        [field: SerializeField] public GameObject MachineGunGhost { get; private set; }
        
        [field: SerializeField] public GameObject RocketLauncherGhost { get; private set; }
        
        [field: SerializeField] public GameObject ShotgunGhost { get; private set; }

        [Header("Other Prefabs")]
        [field: SerializeField] public GameObject SpectatorPrefab { get; private set; }
        
        public class Baker : Baker<SpawnCharacterResourcesAuthoring>
        {
            public override void Bake(SpawnCharacterResourcesAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new SpawnCharacterResourcesComponentData
                {
                    RespawnTime = authoring.RespawnTime,
                    PlayerGhost = GetEntity(authoring.PlayerGhost, TransformUsageFlags.Dynamic),
                    CharacterGhost = GetEntity(authoring.CharacterGhost, TransformUsageFlags.Dynamic),
                    RailgunGhost = GetEntity(authoring.RailgunGhost, TransformUsageFlags.Dynamic),
                    MachineGunGhost = GetEntity(authoring.MachineGunGhost, TransformUsageFlags.Dynamic),
                    RocketLauncherGhost = GetEntity(authoring.RocketLauncherGhost, TransformUsageFlags.Dynamic),
                    ShotgunGhost = GetEntity(authoring.ShotgunGhost, TransformUsageFlags.Dynamic),
            
                    SpectatorPrefab = GetEntity(authoring.SpectatorPrefab, TransformUsageFlags.Dynamic),
                });
            }
        }
    }
}