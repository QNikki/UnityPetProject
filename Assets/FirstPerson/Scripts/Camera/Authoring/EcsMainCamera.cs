using UnityEngine;
using Unity.Entities;

namespace FP.Core.Character
{
 [DisallowMultipleComponent]
    public class EcsMainCamera: MonoBehaviour
    {
        public class Baker : Baker<EcsMainCamera>
        {
            public override void Bake(EcsMainCamera authoring)
            {
                AddComponent<EcsMainCameraData>(GetEntity(TransformUsageFlags.Dynamic));
            }
        }
    }
}