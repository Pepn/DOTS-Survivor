using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

namespace DOTSSurvivor
{
    public readonly partial struct PlayerAspect : IAspect
    {
        public readonly Entity self;
        public readonly RefRW<LocalTransform> Transform;
        public readonly RefRO<Controller> Controller;
        public readonly RefRW<PlayerData> PlayerData;

        // Upgrades can be a seperate IAspect container aswell
        public readonly EnabledRefRW<PlayerUpgradeHexagons> PlayerUpgradeHexagons;
        public readonly EnabledRefRW<PlayerUpgradeIncreaseDamage> PlayerUpgradeIncreaseDamage;
    }
}
