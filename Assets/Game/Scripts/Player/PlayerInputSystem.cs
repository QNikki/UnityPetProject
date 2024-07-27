using DZM.Base;
using Unity.Entities;
using UnityEngine;

namespace DZM.Player
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class PlayerInputSystem : SystemBase
    {
        private PlayerInputActions _inputActions;

        private PlayerInputActions.PlayerMovementActions InputMap => _inputActions.PlayerMovement;

        protected override void OnCreate()
        {
            base.OnCreate();
            
            RequireForUpdate<FixedTickComponentData>();
            RequireForUpdate(SystemAPI.QueryBuilder().WithAll<PlayerInputComponentData, PlayerComponentData>().Build());
            
            _inputActions = new PlayerInputActions();
            _inputActions.PlayerMovement.Enable();
        }

        protected override void OnUpdate()
        {
            var tick = SystemAPI.GetSingleton<FixedTickComponentData>().Tick;
            foreach (var (playerInput, player) in SystemAPI.Query<RefRW<PlayerInputComponentData>, RefRO<PlayerComponentData>>())
            {
                playerInput.ValueRW.Move = Vector2.ClampMagnitude(InputMap.Move.ReadValue<Vector2>(), 1f);
                playerInput.ValueRW.LookDelta = InputMap.Look.ReadValue<Vector2>() * player.ValueRO.LookSensitivity;
                if (InputMap.Jump.WasPressedThisFrame())
                {
                    playerInput.ValueRW.JumpPressed.Set(tick);
                }
            }
        }
    }
}