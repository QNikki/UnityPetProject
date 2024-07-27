using Unity.Entities;

namespace DZM.Player
{
    public struct PlayerComponentData : IComponentData
    {
        public Entity Character;

        public float LookSensitivity;
    }
}