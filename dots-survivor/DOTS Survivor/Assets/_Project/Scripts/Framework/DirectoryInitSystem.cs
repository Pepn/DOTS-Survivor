using System;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;
using Unity.Burst;
using TMPro;

namespace DOTSSurvivor 
{
    public partial struct DirectoryInitSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            // We need to wait for the scene to load before Updating, so we must RequireForUpdate at
            // least one component type loaded from the scene.

            state.RequireForUpdate<Controller>();
        }

        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            var go = GameObject.Find("Directory");
            if (go == null)
            {
                throw new Exception("GameObject 'Directory' not found.");
            }

            var directory = go.GetComponent<Directory>();

            var directoryManaged = new DirectoryManaged();
            directoryManaged.EntityCount = directory.EntityCount;

            var entity = state.EntityManager.CreateEntity();
            state.EntityManager.AddComponentData(entity, directoryManaged);
        }
    }

    public class DirectoryManaged : IComponentData
    {
        public TextMeshProUGUI EntityCount;

        // Every IComponentData class must have a no-arg constructor.
        public DirectoryManaged()
        {
        }
    }
}
