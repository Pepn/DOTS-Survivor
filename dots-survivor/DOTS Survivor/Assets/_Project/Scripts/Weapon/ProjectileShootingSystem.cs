using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace DOTSSurvivor
{
    public partial struct ProjectileShootingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputState>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var playerMovementDirection = SystemAPI.GetSingleton<InputState>();

            foreach (var (projectileShooter, localToWorld) in
                     SystemAPI.Query<ProjectileShooter, RefRO<LocalToWorld>>()
                         .WithAll<ProjectileShooter>())
            {
                Entity instance = state.EntityManager.Instantiate(projectileShooter.Projectile);

                state.EntityManager.SetComponentData(instance, new LocalTransform
                {
                    Position = localToWorld.ValueRO.Position,
                    Rotation = quaternion.identity,
                    Scale = SystemAPI.GetComponent<LocalTransform>(projectileShooter.Projectile).Scale
                });

                state.EntityManager.SetComponentData(instance, new ProjectileData
                {
                    Velocity = new float3(playerMovementDirection.Horizontal, playerMovementDirection.Vertical, 0) * 20.0f,
                    TimeLeft = projectileShooter.ProjectileLifeTime
                });

                //state.EntityManager.SetComponentData(instance, new URPMaterialPropertyBaseColor
                //{
                //    Value = turret.Color
                //});
            }
        }
    }
}
