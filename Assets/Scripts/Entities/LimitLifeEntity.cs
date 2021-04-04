using Components;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;


namespace Entities
{
    [AddComponentMenu("PetProject / Entities / Life Limit ")]
    class LimitLifeEntity : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float DegreesPerSecond = 360.0F;

        public float Lifetime = 5f;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) 
        {
            dstManager.AddComponentData<VelocityComponent>(entity, new VelocityComponent() 
            { 
                AngularVelocity = math.radians(DegreesPerSecond),
            });

            dstManager.AddComponentData<LifetimeComponent>(entity, new LifetimeComponent()
            {
                Value = Lifetime,
            }); 
        }
    }
}
