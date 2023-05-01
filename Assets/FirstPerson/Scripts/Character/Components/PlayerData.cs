using System;
using Unity.Entities;

namespace FP.Core.Character
{
    [Serializable]
    public struct PlayerData : IComponentData
    {
        public Entity Character;
        
        public Entity Camera;
    }
}