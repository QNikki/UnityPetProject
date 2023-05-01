using Unity.Entities;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

namespace BoardGame.HexGrid
{
    public class HexRenderAuthoring : MonoBehaviour
    {
        [field: SerializeField]
        public TileType HextType { get; private set; }
        
        [field: SerializeField]
        public MeshFilter Filter { get; private set; }

        [field: SerializeField]
        public MeshRenderer Renderer { get; private set; }

        public class HexRenderBaker : Baker<HexRenderAuthoring>
        {
            public override void Bake(HexRenderAuthoring authoring)
            {
                var entity = GetEntity(new TransformUsageFlags());
                AddComponent(entity, new HexRenderComponent { });
            }
        }
    }
}