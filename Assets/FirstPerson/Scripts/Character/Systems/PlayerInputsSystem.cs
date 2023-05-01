using Unity.Entities;
using UnityEngine;

namespace FP.Core.Character
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class PlayerInputsSystem : SystemBase
    {
        private CharacterInputActions InputActions { get; set; }

        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<FixedTickSystem.Singleton>();
            RequireForUpdate(SystemAPI.QueryBuilder().WithAll<PlayerData, InputData>().Build());
            InputActions = new();
            InputActions.Enable();
            InputActions.Player.Enable();
        }

        protected override void OnUpdate()
        {
            var charActions = InputActions.Player;
            uint fixedTick = SystemAPI.GetSingleton<FixedTickSystem.Singleton>().Tick;
            foreach (var (playerInputs, _) in SystemAPI.Query<RefRW<InputData>, PlayerData>())
            {
                // if (math.lengthsq(charActions.LookConst.ReadValue<Vector2>()) > math.lengthsq(charActions.LookDelta.ReadValue<Vector2>()))
                // {
                //     playerInputs.ValueRW.CameraLookInput =
                //         charActions.LookConst.ReadValue<Vector2>() * SystemAPI.Time.DeltaTime;
                // }
                // else
                // {
                //    playerInputs.ValueRW.CameraLookInput = charActions.LookDelta.ReadValue<Vector2>();
                // }
                
                playerInputs.ValueRW.Move = Vector2.ClampMagnitude(charActions.Move.ReadValue<Vector2>(), 1f);
                playerInputs.ValueRW.CameraLook = charActions.Look.ReadValue<Vector2>();
                playerInputs.ValueRW.CameraZoom = charActions.Zoom.ReadValue<float>();
                if (charActions.Jump.WasPressedThisFrame())
                {
                    playerInputs.ValueRW.JumpPressed.Set(fixedTick);
                }
            }
        }
    }
}