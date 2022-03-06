using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct MovementComponent: IComponentData
    {
        public float MovementSpeed;

        public float RotationSpeed;

        public float JumpUpwardsSpeed;

        public InputMovement InputMovement;
    }

    public struct ForwardMoveComponent: IComponentData
    {
        public float FowardSpeed;

        public float MaxFowardSpeed;

        public float DesiredForwardSpeed;
    }

    public struct VerticalMoveComponent: IComponentData 
    {
        public bool ISGrounded;

        public float VerticalSpeed;

        public float JumpAbortSpeed;

        public bool ReadyForJump;

        public float Gravity;
    }

    public struct TargetRotationComponent: IComponentData
    {
        public float AngleDiff;

        public float4 TargetRotation;
    }
}
