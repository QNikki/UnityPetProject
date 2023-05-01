using System;
using UnityEngine;
using Unity.Entities;

namespace FP.Core.Character
{
    public class PlayerSpawn: MonoBehaviour
    {
         public GameObject CharacterSpawnPoint;
         
         public GameObject CharacterPrefab;
         
         public GameObject EcsCameraPrefab;
         
         public GameObject PlayerPrefab;
                
        public class Baker : Baker<PlayerSpawn>
        {
            public override void Bake(PlayerSpawn authoring)
            {
                AddComponent(GetEntity(TransformUsageFlags.Dynamic), new PlayerSpawnData
                {
                    SpawnPoint = GetEntity(authoring.CharacterSpawnPoint,TransformUsageFlags.None),
                    CharacterPrefab = GetEntity(authoring.CharacterPrefab, TransformUsageFlags.None),
                    CameraPrefab = GetEntity(authoring.EcsCameraPrefab, TransformUsageFlags.None),
                    PlayerPrefab = GetEntity(authoring.PlayerPrefab, TransformUsageFlags.None),
                });
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, radius:0.3f);
        }
    }
}