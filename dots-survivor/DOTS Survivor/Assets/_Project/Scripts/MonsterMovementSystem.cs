using DOTSSurvivor;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
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
            // Iterates over all SampleComponents and increments their value
            public void Execute(ref MonsterData sample, ref LocalTransform transform)
            {
                transform.Position.x += 1.0f * DeltaTime;
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
            // Uses the BoidTarget query
            var job = new QueryJob()
            {
                DeltaTime = SystemAPI.Time.DeltaTime,
            };

            job.ScheduleParallel(queryMonsters);
        }
    }
}
