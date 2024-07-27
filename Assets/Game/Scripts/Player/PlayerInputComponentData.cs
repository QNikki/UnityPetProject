using DZM.Base;
using Unity.Entities;
using Unity.Mathematics;

namespace DZM.Player
{
    public struct PlayerInputComponentData : IComponentData
    {
        public float2 Move;

        public float2 LookDelta;

        // maybe should replace to InputEvent
        public FixedInputEvent JumpPressed;
    }
}