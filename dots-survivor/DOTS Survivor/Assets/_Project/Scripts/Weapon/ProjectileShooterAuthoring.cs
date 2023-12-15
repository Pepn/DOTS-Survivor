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
        public float ShootingAngle = 0;
        public float Damage = 1.0f;
        public GameObject TrackingEntity;

        class Baker : Baker<ProjectileShootingAuthoring>
        {
            public override void Bake(ProjectileShootingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new ProjectileShooter
                {
                    Speed = 25,//math.clamp(Unity.Mathematics.Random.CreateFromIndex(0).NextFloat(), 10f, 100f),
                    ProjectileLifeTime = authoring.ProjectileLifeTime,
                    ShootingAngle = authoring.ShootingAngle,
                    Damage = authoring.Damage,
                    Projectile = GetEntity(authoring.Projectile, TransformUsageFlags.Dynamic),
                    TrackingEntity = GetEntity(authoring.TrackingEntity, TransformUsageFlags.Dynamic)
                });
            }
        }
    }

    public struct ProjectileShooter : IComponentData
    {
        public float Speed;
        public float ProjectileLifeTime;
        public float ShootingAngle;
        public float Damage;
        public Entity Projectile;
        public Entity TrackingEntity;
    }
}