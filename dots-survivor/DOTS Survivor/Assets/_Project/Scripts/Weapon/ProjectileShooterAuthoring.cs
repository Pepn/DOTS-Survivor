using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DOTSSurvivor
{
    public class ProjectileShootingAuthoring : MonoBehaviour
    {
        public float MovementSpeedMetersPerSecond = 5.0f;
        public GameObject Projectile;

        class Baker : Baker<ProjectileShootingAuthoring>
        {
            public override void Bake(ProjectileShootingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new ProjectileShooter
                {
                    Speed = authoring.MovementSpeedMetersPerSecond,
                    Projectile = GetEntity(authoring.Projectile, TransformUsageFlags.Dynamic),
                });
            }
        }
    }

    public struct ProjectileShooter : IComponentData
    {
        public float Speed; // meters per second
        public Entity Projectile; // TODO HOW TO SET THIS IN EDITOR, NEXT HOW TO SPAWN PROJECTILECOMPONENT OR ASPECT TO ENTITY
    }
}