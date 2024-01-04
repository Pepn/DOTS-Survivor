using DOTSSurvivor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Windows;

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
            public uint SeedOffset;

            // Iterates over all SampleComponents and increments their value
            public void Execute([EntityIndexInQuery] int index, ref MonsterData sample, ref LocalTransform transform, Entity entity)
            {
                // Random instances with similar seeds produce similar results, so to get proper
                // randomness here, we use CreateFromIndex, which hashes the seed.
                var random = Unity.Mathematics.Random.CreateFromIndex(SeedOffset + (uint)index);

                var offset = random.NextFloat3Direction() * 25.0f;
                //Debug.Log(offset);
                offset.z = 0;

                // Calculate the distance
                float distance = math.distance(TargetPos, transform.Position);
                float distanceHeuristicallyNormalized = distance / 50.0f;
                float3 offsetLerped = math.lerp(float3.zero, offset, distanceHeuristicallyNormalized);

                // Calculate the direction vector from current position to target position
                float3 direction = math.normalize(TargetPos + offsetLerped - transform.Position);

                // Move towards the target position with a constant speed
                transform.Position += direction * sample.MovementSpeed * DeltaTime;

                // If the new position intersects the player with a wall, don't move the player.
                if (math.distancesq(transform.Position, TargetPos) <= 2.0f)
                {
                    ECB.DestroyEntity(entity);

                    // Create dmg entity, todo make this some buffer or smth
                    var newHitEntity = ECB.CreateEntity();
                    ECB.AddComponent(newHitEntity, new DamageHit { Damage = 1.0f });
                }
            }
        }

        EntityQuery queryMonsters;
        uint seedOffset;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerData>();
            state.RequireForUpdate<InputState>();
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

            var minDist = 0.3f;

            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            var experienceSpawner = SystemAPI.GetSingletonRW<ExperienceSpawner>();
            Unity.Mathematics.Random rng = Unity.Mathematics.Random.CreateFromIndex(0);

            // check bullet monster collision
            foreach (var (monsterTransform, monsterEntity) in
                     SystemAPI.Query<RefRO<LocalTransform>>()
                         .WithAll<MonsterData>().WithEntityAccess())
            {
                foreach (var (projectileTransform, projectileEntity) in
                         SystemAPI.Query<RefRO<LocalTransform>>()
                             .WithAll<ProjectileData>().WithEntityAccess())
                {
                    if (math.distancesq(monsterTransform.ValueRO.Position, projectileTransform.ValueRO.Position) <= minDist)
                    {
                        ecb.DestroyEntity(monsterEntity);
                        ecb.DestroyEntity(projectileEntity);

                        float r = rng.NextFloat(0f, 1f);

                        // Spawn XP chance based
                        if (r > experienceSpawner.ValueRO.SpawnChance)
                        {
                            //Debug.Log($"{r} bigger than {experienceSpawner.ValueRO.SpawnChance} dont spawn!");
                            continue;
                        }

                        var newXPEntity = ecb.Instantiate(experienceSpawner.ValueRO.XPPrefab);
                        ecb.AddComponent(newXPEntity, new LocalTransform
                        {
                            Position = new float3(monsterTransform.ValueRO.Position.x, monsterTransform.ValueRO.Position.y, 0f),
                            Rotation = quaternion.Euler(new float3(45f, 45f, 45f)),
                            Scale = 1.0f,
                        });
                        break;
                    }
                }
            }

            //seedOffset += 1337;

            // Uses the BoidTarget query
            var job = new MoveMonsterJob()
            {
                SeedOffset = seedOffset,
                DeltaTime = SystemAPI.Time.DeltaTime,
                TargetPos = controllerTransform.Position,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
            };
            job.Schedule(queryMonsters);

        }
    }
}
