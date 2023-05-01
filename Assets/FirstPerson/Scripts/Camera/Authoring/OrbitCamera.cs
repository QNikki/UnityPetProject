using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using System.Collections.Generic;

namespace FP.Core.Character
{
    [DisallowMultipleComponent]
    public class OrbitCamera: MonoBehaviour
    {
        public List<GameObject> IgnoredEntities = new();
        
        public OrbitCameraData Camera = OrbitCameraData.Default;
        
        public class Baker : Baker<OrbitCamera>
        {
            public override void Bake(OrbitCamera authoring)
            {
                authoring.Camera.CurrentDistanceFromMovement = authoring.Camera.TargetDistance;
                authoring.Camera.CurrentDistanceFromObstruction = authoring.Camera.TargetDistance;
                authoring.Camera.PlanarForward = -math.forward();
                AddComponent(GetEntity(TransformUsageFlags.Dynamic), authoring.Camera);
                AddComponent(GetEntity(TransformUsageFlags.None),new CameraControlData());
                var ignoredEntitiesBuffer = AddBuffer<CameraIgnoreBufferData>(GetEntity(TransformUsageFlags.WorldSpace));
                foreach (var ignored in authoring.IgnoredEntities)
                {
                    ignoredEntitiesBuffer.Add(new CameraIgnoreBufferData
                    {
                        Entity = GetEntity(ignored, TransformUsageFlags.WorldSpace),
                    });
                }
            }
        }
    }
}