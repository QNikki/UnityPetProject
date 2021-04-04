using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct MovementComponent : IComponentData
    {
        public float MovementSpeed;

        public float MaxMovementSpeed;

        public float RotationSpeed;

        public float JumpUpwardsSpeed;

        public float MaxSlope;

        public int MaxIterations;
    }
}
