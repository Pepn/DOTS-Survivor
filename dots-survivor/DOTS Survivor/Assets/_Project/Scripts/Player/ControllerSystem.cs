using System.Diagnostics;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DOTSSurvivor
{
    [UpdateAfter(typeof(InputSystem))]
    public partial struct ControllerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputState>();
            state.RequireForUpdate<PlayerData>();

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            var input = SystemAPI.GetSingleton<InputState>();
            var playerData = SystemAPI.GetSingletonRW<PlayerData>();
            foreach (var (transform, controller) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRW<Controller>>())
            {
                // Move around with WASD
                var move = new float3(input.Horizontal, input.Vertical, 0);
                if(move.x != 0 || move.y != 0)
                {
                    controller.ValueRW.Direction = move;
                }

                move = move * controller.ValueRO.player_speed * SystemAPI.Time.DeltaTime;
                transform.ValueRW.Position += move;

                // Check collision with XPs
                foreach (var (xpTransform, xpData, entity) in
                    SystemAPI.Query<RefRW<LocalTransform>, RefRW<XPData>>().WithEntityAccess())
                {
                    if (math.distance(xpTransform.ValueRO.Position, transform.ValueRO.Position) < 1f)
                    {
                        playerData.ValueRW.CurrentXP += xpData.ValueRO.XPAmount;
                        ECB.DestroyEntity(entity);
                    }
                }
            }
        }

        [BurstCompile]
        partial struct CollectExperienceJob : IJobEntity
        {
            public EntityCommandBuffer ECB;

            public void Execute(ref ProjectileData projectile, ref LocalTransform transform, Entity entity)
            {

                if (projectile.TimeLeft < 0f)
                {
                    ECB.DestroyEntity(entity);
                }
            }
        }

        EntityQuery projectileQuery;

    }
}
