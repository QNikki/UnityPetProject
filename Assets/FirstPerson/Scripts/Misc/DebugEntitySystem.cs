using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace FP.Core.Character
{
    [WorldSystemFilter(WorldSystemFilterFlags.BakingSystem)]
    [UpdateInGroup(typeof(BakingSystemGroup), OrderLast = true)]
    public partial class DebugEntitySystem : SystemBase
    {
        private EntityQuery _noSectionQuery;
     
        protected override void OnCreate()
        {
            base.OnCreate();
             
            _noSectionQuery = GetEntityQuery(
                new EntityQueryDesc
                {
                    None = new[] {ComponentType.ReadWrite<SceneSection>()},
                    Options = EntityQueryOptions.IncludePrefab | EntityQueryOptions.IncludeDisabledEntities
                }
            );
             
            RequireForUpdate(_noSectionQuery);
        }
     
        protected override void OnUpdate()
        {
            if (_noSectionQuery.IsEmpty)
            {
                return;
            }
            var entities = _noSectionQuery.ToEntityArray(Allocator.TempJob);
            foreach (var entity in entities)
            {
                Debug.LogWarning($"{EntityManager.GetName(entity)} has no SceneSection!");
            }
            entities.Dispose();
        }
    }

}