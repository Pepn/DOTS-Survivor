using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DOTSSurvivor
{
    public class ProjectileShootingAuthoring : MonoBehaviour
    {
        public float MovementSpeedMetersPerSecond = 5.0f;
        public GameObject Projectile;
        public float ProjectileLifeTime = 5.0f;

        class Baker : Baker<ProjectileShootingAuthoring>
        {
            public override void Bake(ProjectileShootingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);


                AddComponent(entity, new ProjectileShooter
                {
                    Speed = 25,//math.clamp(Unity.Mathematics.Random.CreateFromIndex(0).NextFloat(), 10f, 100f),
                    ProjectileLifeTime = authoring.ProjectileLifeTime,
                    Projectile = GetEntity(authoring.Projectile, TransformUsageFlags.Dynamic),
                });
            }
        }
    }

    public struct ProjectileShooter : IComponentData
    {
        public float Speed;
        public float ProjectileLifeTime;
        public Entity Projectile;
    }
}