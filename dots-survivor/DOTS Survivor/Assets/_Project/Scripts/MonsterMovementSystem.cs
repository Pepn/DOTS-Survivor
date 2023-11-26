using DOTSSurvivor;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor;
using UnityEngine;

namespace DOTSSurvivor
{
    [RequireMatchingQueriesForUpdate]
    public partial class MonsterMovementSystem : SystemBase
    {
        partial struct QueryJob : IJobEntity
        {
            public float DeltaTime;
            public float3 TargetPos;
            // Iterates over all SampleComponents and increments their value
            public void Execute(ref MonsterData sample, ref LocalTransform transform)
            {
                // Calculate the direction vector from current position to target position
                float3 direction = math.normalize(TargetPos - transform.Position);

                // Move towards the target position with a constant speed
                transform.Position += direction * sample.MovementSpeed * DeltaTime;
            }
        }

        // Query that matches QueryJob, specified for `BoidTarget`
        EntityQuery queryMonsters;

        protected override void OnCreate()
        {
            queryMonsters = GetEntityQuery(ComponentType.ReadWrite<MonsterData>(), ComponentType.ReadWrite<LocalTransform>());
        }

        protected override void OnUpdate()
        {
            // get player position //TODO: optimize
            var transformLookup = SystemAPI.GetComponentLookup<LocalTransform>(true);
            var controllerEntity = SystemAPI.GetSingletonEntity<Controller>();
            var controller = SystemAPI.GetSingleton<Controller>();
            var controllerTransform = transformLookup[controllerEntity];

            // Uses the BoidTarget query
            var job = new QueryJob()
            {
                DeltaTime = SystemAPI.Time.DeltaTime,
                TargetPos = controllerTransform.Position,
            };

            job.ScheduleParallel(queryMonsters);
        }
    }
}
