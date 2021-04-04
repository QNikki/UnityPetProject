using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct MovementComponent : IComponentData
    {
        public float MovementSpeed;

        public float RotationSpeed;

        public float JumpUpwardsSpeed;

        public InputMovement InputMovement;
    }
}
