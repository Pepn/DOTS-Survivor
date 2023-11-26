using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Rendering.Universal;

namespace DOTSSurvivor
{
    public partial struct MonsterSpawnSystem : ISystem
    {
        public partial struct SpawnSystem : ISystem
        {
            uint seedOffset;
            float spawnTimer;

            [BurstCompile]
            public void OnCreate(ref SystemState state)
            {
                state.RequireForUpdate<MonsterSpawner>();
            }

            [BurstCompile]
            public void OnUpdate(ref SystemState state)
            {
                const int count = 5;
                const float spawnWait = 0.05f;  // 0.05 seconds

                spawnTimer -= SystemAPI.Time.DeltaTime;
                if (spawnTimer > 0)
                {
                    return;
                }
                spawnTimer = spawnWait;

                // Remove the NewSpawn tag component from the entities spawned in the prior frame.
                var newSpawnQuery = SystemAPI.QueryBuilder().WithAll<Unspawned>().Build();
                state.EntityManager.RemoveComponent<Unspawned>(newSpawnQuery);

                // Spawn the boxes
                var prefab = SystemAPI.GetSingleton<MonsterSpawner>().Prefab;
                state.EntityManager.Instantiate(prefab, count, Allocator.Temp);

                // Get Player Pos
                var transformLookup = SystemAPI.GetComponentLookup<LocalTransform>(true);
                var controllerEntity = SystemAPI.GetSingletonEntity<Controller>();
                var controllerTransform = transformLookup[controllerEntity];

                // Every spawned box needs a unique seed, so the
                // seedOffset must be incremented by the number of boxes every frame.
                seedOffset += 1337;

                new RandomPositionJob
                {
                    SeedOffset = seedOffset,
                    PlayerPos = controllerTransform.Position,
                    MinSpawnDistance = 30f,
                }.ScheduleParallel();
            }
        }

        [WithAll(typeof(Unspawned))]
        [BurstCompile]
        partial struct RandomPositionJob : IJobEntity
        {
            public uint SeedOffset;
            public float3 PlayerPos;
            public float MinSpawnDistance;

            public void Execute([EntityIndexInQuery] int index, ref LocalTransform transform)
            {
                // Random instances with similar seeds produce similar results, so to get proper
                // randomness here, we use CreateFromIndex, which hashes the seed.
                var random = Random.CreateFromIndex(SeedOffset + (uint)index);
                var angle = random.NextFloat() * 2 * math.PI;
                // Generate a random radius within the specified range
                float radius = MinSpawnDistance;
                // Calculate the Cartesian coordinates based on polar coordinates
                float3 spawnPos = PlayerPos + new float3(
                    radius * math.cos(angle),
                    radius * math.sin(angle),
                    0f);

                transform.Position = spawnPos;
            }
        }


        //uint updateCounter;
        //
        //[BurstCompile]
        //public void OnCreate(ref SystemState state)
        //{
        //    // This call makes the system not update unless at least one entity in the world exists that has the Spawner component.
        //    state.RequireForUpdate<MonsterSpawner>();
        //}
        //
        //[BurstCompile]
        //public void OnUpdate(ref SystemState state)
        //{
        //    // Create a query that matches all entities having a RotationSpeed component.
        //    // (The query is cached in source generation, so this does not incur a cost of recreating it every update.)
        //    var spinningCubesQuery = SystemAPI.QueryBuilder().WithAll<MonsterData>().Build();
        //
        //    // Only spawn cubes when no cubes currently exist.
        //    if (spinningCubesQuery.IsEmpty)
        //    {
        //        var prefab = SystemAPI.GetSingleton<MonsterSpawner>().Prefab;
        //
        //        // Instantiating an entity creates copy entities with the same component types and values.
        //        var instances = state.EntityManager.Instantiate(prefab, 500, Allocator.Temp);
        //
        //        // Unlike new Random(), CreateFromIndex() hashes the random seed
        //        // so that similar seeds don't produce similar results.
        //        var random = Random.CreateFromIndex(updateCounter++);
        //
        //        foreach (var entity in instances)
        //        {
        //            // Update the entity's LocalTransform component with the new position.
        //            var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
        //            transform.ValueRW.Position = (random.NextFloat3() - new float3(0.5f, 0, 0.5f)) * 20;
        //        }
        //    }
        //}
    }
}
