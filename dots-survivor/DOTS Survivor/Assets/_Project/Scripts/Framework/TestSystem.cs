using DOTSSurvivor;
using System.CodeDom.Compiler;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace DOTSSurvivor
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct RotatorInitSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<DirectoryManaged>();
        }

        // This OnUpdate accesses managed objects, so it cannot be burst compiled.
        public void OnUpdate(ref SystemState state)
        {
            var directory = SystemAPI.ManagedAPI.GetSingleton<DirectoryManaged>();
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            // Instantiate the associated GameObject from the prefab.
            foreach (var (dir, entity) in
                     SystemAPI.Query<DirectoryManaged>()
                         .WithEntityAccess())
            {
                Debug.Log(dir.PlayerSync.life);
                dir.PlayerSync.life = 10;
            }
        }
    }
}