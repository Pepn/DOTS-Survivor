using System.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace DOTSSurvivor
{
    // Define a buffer element type (e.g., an item in an inventory)
    public struct DamageHit : IComponentData
    {
        public Entity Target;   // Example: Entity reference for the item
        public float Damage;      // Example: Quantity of the item
    }

    // Example system that adds an item to the inventory
    public partial struct DamageSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            //var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            //
            //var playerData = SystemAPI.GetSingletonRW<PlayerData>();
            //foreach (var (hit, entity) in SystemAPI.Query<DamageHit>().WithEntityAccess())
            //{
            //    playerData.ValueRW.CurrentHealth -= hit.Damage;
            //    ecb.DestroyEntity(entity);
            //}
        }
    }
}