using DOTSSurvivor;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace DOTSSurvivor
{
    public partial struct ProjectileMovementSystem : ISystem
    {
        [BurstCompile]
        partial struct MoveProjectileJob : IJobEntity
        {
            public float DeltaTime;
            public EntityCommandBuffer ECB;

            public void Execute(ref ProjectileData projectile, ref LocalTransform transform, Entity entity)
            {
                transform.Position += projectile.Velocity * DeltaTime;
                projectile.TimeLeft -= DeltaTime;

                if (projectile.TimeLeft < 0f)
                {
                    ECB.DestroyEntity(entity);
                }
            }
        }

        EntityQuery projectileQuery;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        { 
            projectileQuery = SystemAPI.QueryBuilder().WithAll<ProjectileData, LocalTransform>().Build();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            //
            //var job = new MoveProjectileJob()
            //{
            //    DeltaTime = SystemAPI.Time.DeltaTime,
            //    ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
            //};
            //
            //job.Schedule(projectileQuery);
        }
    }
}
