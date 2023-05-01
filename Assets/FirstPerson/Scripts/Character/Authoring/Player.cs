using UnityEngine;
using Unity.Entities;

namespace FP.Core.Character
{
    [DisallowMultipleComponent]
    public class Player: MonoBehaviour
    {
        public GameObject Character;
        
        public GameObject Camera;

        public class Baker : Baker<Player>
        {
            public override void Bake(Player authoring)
            {
                AddComponent(GetEntity(TransformUsageFlags.Dynamic), new PlayerData
                {
                    Character = GetEntity(authoring.Character, TransformUsageFlags.Dynamic),
                    Camera = GetEntity(authoring.Camera, TransformUsageFlags.Dynamic),
                });
                
                AddComponent(GetEntity(TransformUsageFlags.None),new InputData());
            }
        }
    }
}