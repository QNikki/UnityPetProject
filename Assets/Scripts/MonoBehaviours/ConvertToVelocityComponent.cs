using Unity.Entities;
using Unity.Mathematics;
using Components;
using UnityEngine;

namespace MonoBehaviours
{
    [AddComponentMenu("PetProject / IJobEntityBatch / Velocity Comp")]
   // [ConverterVersion("joe", 1)]
    public class ConvertToVelocityComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float DegreesPerSecond = 360.0F;

        // The MonoBehaviour data is converted to ComponentData on the entity.
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new VelocityComponent { AngularVelocity = math.radians(DegreesPerSecond) };
            dstManager.AddComponentData(entity, data);
        }
    }
}