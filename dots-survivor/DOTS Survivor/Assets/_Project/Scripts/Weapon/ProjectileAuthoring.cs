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

                AddComponent<Projectile>(entity);
            }
        }
    }

    public struct Projectile : IComponentData
    {
        public float3 Velocity; 
    }
}