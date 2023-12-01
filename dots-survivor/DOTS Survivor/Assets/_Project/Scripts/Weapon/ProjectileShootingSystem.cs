using System;
using System.Diagnostics;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace DOTSSurvivor
{
    public partial struct ProjectileSpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputState>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //var playerInput = SystemAPI.GetSingleton<InputState>();
            //var playerDirection = new float3(playerInput.Horizontal, playerInput.Vertical, 0);
            //var playerDirectionNormalized = math.normalize(playerDirection);
            //
            //float3 offsetDir = CalculateDirection(playerDirectionNormalized, 10);
            //float3 offsetDir2 = CalculateDirection(playerDirectionNormalized, -10);
            //float3 offsetDir3 = CalculateDirection(playerDirectionNormalized, -20);
            //float3 offsetDir4 = CalculateDirection(playerDirectionNormalized, 20);
            //
            //foreach (var (projectileShooter, localToWorld) in
            //         SystemAPI.Query<ProjectileShooter, RefRO<LocalToWorld>>()
            //             .WithAll<ProjectileShooter>())
            //{
            //    ShootInDirection(ref state, projectileShooter, localToWorld, playerDirection);
            //    ShootInDirection(ref state, projectileShooter, localToWorld, offsetDir);
            //    ShootInDirection(ref state, projectileShooter, localToWorld, offsetDir2);
            //    ShootInDirection(ref state, projectileShooter, localToWorld, offsetDir3);
            //    ShootInDirection(ref state, projectileShooter, localToWorld, offsetDir4);
            //}
        }

        private float3 CalculateDirection(float3 inputDirection, float angleOffset)
        {
            // Calculate the angle in radians
            float angle = math.atan2(inputDirection.y, inputDirection.x);

            // Convert the angle to degrees
            float angleDegrees = math.degrees(angle);

            // Add 10 degrees to the angle
            angle += math.radians(angleOffset);

            // Calculate X and Y components
            float x = math.cos(angle);
            float y = math.sin(angle);

            return new float3(x, y, 0);
        }

        private void ShootInDirection(ref SystemState state, ProjectileShooter projectileShooter, RefRO<LocalToWorld> localToWorld, float3 playerDirection)
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
                Velocity = playerDirection * 20.0f,
                TimeLeft = projectileShooter.ProjectileLifeTime
            });

            //state.EntityManager.SetComponentData(instance, new URPMaterialPropertyBaseColor
            //{
            //    Value = turret.Color
            //});
        }
    }
}
