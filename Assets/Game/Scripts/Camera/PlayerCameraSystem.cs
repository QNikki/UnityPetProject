using Unity.Entities;
using Unity.Transforms;

namespace DZM.Camera
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial class PlayerCameraSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            if (PlayerCameraMb.Instance == null || !SystemAPI.HasSingleton<PlayerCameraComponentData>())
            {
                return;
            }

            var cameraEntity = SystemAPI.GetSingletonEntity<PlayerCameraComponentData>();
            var cameraData = SystemAPI.GetSingleton<PlayerCameraComponentData>();
            var targetLocalToWorld = SystemAPI.GetComponent<LocalToWorld>(cameraEntity);

            var camera = PlayerCameraMb.Instance;
            camera.transform.SetPositionAndRotation(targetLocalToWorld.Position, targetLocalToWorld.Rotation);
            camera.fieldOfView = cameraData.CurrentFoV;
        }
    }
}