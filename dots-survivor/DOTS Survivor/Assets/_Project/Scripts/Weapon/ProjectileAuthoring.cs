using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace DOTSSurvivor
{
    public class ProjectileAuthoring : MonoBehaviour
    {
        class Baker : Baker<ProjectileAuthoring>
        {
            public override void Bake(ProjectileAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<ProjectileData>(entity);
            }
        }
    }

    public struct ProjectileData : IComponentData
    {
        public float3 Velocity;
        public float TimeLeft;
    }
}
