using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.VirtualTexturing;
using static UnityEngine.EventSystems.EventTrigger;

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
                //Debug.LogWarning("Player Upgrade Hexagon Enabled!");
            }
            else
            {
                //Debug.LogWarning("Player Upgrade Hexagon NOT ENABLED!");
            }

            // assume all upgrades are unique
            if (player.PlayerUpgradeIncreaseDamage.ValueRO)
            {
                foreach (var projectileShooter in SystemAPI.Query<RefRW<ProjectileShooter>>())
                {
                    // perhaps projectileShooters should have different Component that contains the tracking entity
                    // this way it should be easier queryable
                    if(projectileShooter.ValueRO.TrackingEntity == player.self)
                    {
                        Debug.LogWarning("PlayerUpgradeIncreaseDamage!");
                        projectileShooter.ValueRW.Damage += 1.0f;
                    }
                }

                Debug.LogWarning("PlayerUpgradeIncreaseDamage Enabled!");
                player.PlayerUpgradeIncreaseDamage.ValueRW = false;
            }
            else
            {
                //Debug.LogWarning("PlayerUpgradeIncreaseDamage NOT ENABLED!");
            }
        }
    }
}