using System;
using System.Diagnostics;
using Unity.Burst;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
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
            var controllerDirection = SystemAPI.GetSingleton<Controller>();
            var playerDirection = controllerDirection.Direction;
            var playerDirectionNormalized = math.normalize(playerDirection);

            foreach (var (projectileShooter, localToWorld, entity) in
                     SystemAPI.Query<RefRW<ProjectileShooter>, RefRO<LocalToWorld>>()
                         .WithAll<ProjectileShooter>().WithEntityAccess())
            {
                // Get Player Pos
                var transformLookup = SystemAPI.GetComponentLookup<LocalTransform>(true);
                var controllerEntity = SystemAPI.GetSingletonEntity<Controller>();
                var controllerTransform = transformLookup[projectileShooter.ValueRO.TrackingEntity];

                state.EntityManager.SetComponentData(entity, new LocalTransform
                {
                    Position = controllerTransform.Position,
                    Rotation = quaternion.identity,
                    Scale = 1.0f
                });

                float3 offsetDir = CalculateDirection(playerDirectionNormalized, projectileShooter.ValueRO.ShootingAngle);

                // check if we should shoot, if we do this check earlier the bullet spawns behind us for some reason..
                projectileShooter.ValueRW.Timer += SystemAPI.Time.DeltaTime;
                if (projectileShooter.ValueRW.Timer > projectileShooter.ValueRO.AttackSpeed)
                {
                    projectileShooter.ValueRW.Timer = 0;
                    //ShootInDirection(ref state, projectileShooter, localToWorld, playerDirection);
                    ShootInDirection(ref state, projectileShooter.ValueRW, localToWorld, offsetDir);
                }

            }
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
