using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Systems
{
    [AlwaysUpdateSystem]
    public partial class InputSystem : SystemBase, PCContrlos.IPlayerInputActions
    {
        private PCContrlos _inputActions;

        private EntityQuery _movementInputQuery;

        private EntityQuery _abilityInputQuery;


        private Vector2 _movement;

        private Vector2 _looking;

        private bool _jumped;

        private bool _ability1;

        private bool _ability2;

        private bool _rightHand;

        private bool _leftHand;

        protected override void OnStartRunning() => _inputActions.Enable();

        protected override void OnStopRunning() => _inputActions.Disable();

        public void OnAbility1(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _ability1 = true;
            }
        }

        public void OnAbility2(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _ability1 = true;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _jumped = true;
            }

        }

        public void OnLeftHand(InputAction.CallbackContext context) => _leftHand = context.performed;

        public void OnLook(InputAction.CallbackContext context) => _looking = context.ReadValue<Vector2>();

        public void OnMovement(InputAction.CallbackContext context) => _movement = context.ReadValue<Vector2>();

        public void OnRightHand(InputAction.CallbackContext context) => _rightHand = context.performed;

        protected override void OnCreate()
        {
            base.OnCreate();
            _inputActions = new PCContrlos();
            _inputActions.PlayerInput.SetCallbacks(this);
            _movementInputQuery = GetEntityQuery(typeof(Components.InputMovement));
            _abilityInputQuery = GetEntityQuery(typeof(Components.InputAbility));
        }

        protected override void OnUpdate()
        {
            // movement
            if (_movementInputQuery.CalculateEntityCount() == 0)
            {
                EntityManager.CreateEntity(typeof(Components.InputMovement));
            }

            _movementInputQuery.SetSingleton(value: new InputMovement
            {
                Looking = _looking,
                Movement = _movement,
                Jumped = _jumped,
            });

            if (_abilityInputQuery.CalculateEntityCount() == 0)
            {
                EntityManager.CreateEntity(typeof(Components.InputAbility));
            }

            _abilityInputQuery.SetSingleton(new Components.InputAbility
            {
                AbilityOne = _ability1,
                AbilityTwo = _ability2,
                LeftHand = _leftHand,
                RightHand = _rightHand,
            });
        }
    }

    [UpdateAfter(typeof(InputSystem))]
    public partial class MovementOneTwoManySystem : SystemBase
    {
        protected override void OnUpdate()
        {
            InputMovement input = GetSingleton<InputMovement>();
            Entities
                .WithName("MovementOneTwoManySystemJob")
                .WithBurst()
                .ForEach((ref MovementComponent moveComp) =>
                {
                    moveComp.InputMovement = input;
                }
                ).ScheduleParallel();
        }
    }
}
