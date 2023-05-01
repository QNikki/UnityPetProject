using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

namespace FP.Core.Character
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [CreateAfter(typeof(PresentationSystemGroup))]
    public partial class MainCameraSystem : SystemBase
    {
        private Transform _monoBehCamera;

        protected override void OnCreate()
        {
            _monoBehCamera = MbMainCamera.Camera.transform;
        }

        protected override void OnUpdate()
        {
            if (!SystemAPI.HasSingleton<EcsMainCameraData>())
            {
                return;
            }

            var ecsCamera = SystemAPI.GetSingletonEntity<EcsMainCameraData>();
            var targetLocalToWorld = SystemAPI.GetComponent<LocalToWorld>(ecsCamera);
            _monoBehCamera.SetPositionAndRotation(targetLocalToWorld.Position,
                targetLocalToWorld.Rotation);
        }
    }
}