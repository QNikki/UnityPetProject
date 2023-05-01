using UnityEngine;
using Unity.Entities;

namespace FP.Core.Character
{
    [DisallowMultipleComponent]
    public class CameraTarget : MonoBehaviour
    {
        public GameObject Target;

        public class Baker : Baker<CameraTarget>
        {
            public override void Bake(CameraTarget authoring)
            {
                AddComponent(GetEntity(TransformUsageFlags.Dynamic),new CameraTargetData
                {
                    Target = GetEntity(authoring.Target, TransformUsageFlags.Dynamic),
                });
            }
        }
    }
}