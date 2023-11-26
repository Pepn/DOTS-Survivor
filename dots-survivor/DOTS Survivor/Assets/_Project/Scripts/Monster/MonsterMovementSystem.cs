using DOTSSurvivor;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor;
using UnityEngine;

namespace DOTSSurvivor
{
    [BurstCompile]
    public partial struct MonsterMovementSystem : ISystem
    {
        [BurstCompile]
        partial struct MoveMonsterJob : IJobEntity
        {
            public float DeltaTime;
            public float3 TargetPos;
            public EntityCommandBuffer ECB;
            // Iterates over all SampleComponents and increments their value
            public void Execute(ref MonsterData sample, ref LocalTransform transform, Entity entity)
            {
                // Calculate the direction vector from current position to target position
                float3 direction = math.normalize(TargetPos - transform.Position);

                // Move towards the target position with a constant speed
                transform.Position += direction * sample.MovementSpeed * DeltaTime;

                // If the new position intersects the player with a wall, don't move the player.
                if (math.distancesq(transform.Position, TargetPos) <= 5.0f)
                {
                    ECB.DestroyEntity(entity);
                }
            }
        }

        // Query that matches QueryJob, specified for `BoidTarget`
        EntityQuery queryMonsters;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            queryMonsters = SystemAPI.QueryBuilder().WithAll<MonsterData, LocalTransform>().Build();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // get player position //TODO: optimize
            var transformLookup = SystemAPI.GetComponentLookup<LocalTransform>(true);
            var controllerEntity = SystemAPI.GetSingletonEntity<Controller>();
            var controller = SystemAPI.GetSingleton<Controller>();
            var controllerTransform = transformLookup[controllerEntity];

            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

            // Uses the BoidTarget query
            var job = new MoveMonsterJob()
            {
                DeltaTime = SystemAPI.Time.DeltaTime,
                TargetPos = controllerTransform.Position,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
            };

            job.Schedule(queryMonsters);
        }
    }
}
