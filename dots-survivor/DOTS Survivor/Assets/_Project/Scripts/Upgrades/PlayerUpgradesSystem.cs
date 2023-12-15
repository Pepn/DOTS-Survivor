using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.VirtualTexturing;

namespace DOTSSurvivor
{
    public partial struct PlayerUpgradesSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerUpgradeHexagons>();
            state.RequireForUpdate<PlayerUpgradeIncreaseDamage>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            PlayerAspect player = SystemAPI.GetAspect<PlayerAspect>(SystemAPI.GetSingletonEntity<Controller>());

            // assume all upgrades are unique
            if (player.PlayerUpgradeHexagons.ValueRO)
            {
                Debug.LogWarning("Player Upgrade Hexagon Enabled!");
            }
            else
            {
                Debug.LogWarning("Player Upgrade Hexagon NOT ENABLED!");
            }

            // assume all upgrades are unique
            if (player.PlayerUpgradeIncreaseDamage.ValueRO)
            {
                Debug.LogWarning("PlayerUpgradeIncreaseDamage Enabled!");
            }
            else
            {
                Debug.LogWarning("PlayerUpgradeIncreaseDamage NOT ENABLED!");
            }
        }
    }
}