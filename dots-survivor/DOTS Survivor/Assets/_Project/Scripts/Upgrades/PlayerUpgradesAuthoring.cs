using System.Xml;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DOTSSurvivor
{
    public class PlayerUpgradesAuthoring : MonoBehaviour
    {
        class Baker : Baker<PlayerUpgradesAuthoring>
        {
            public override void Bake(PlayerUpgradesAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new PlayerUpgradeHexagons { });
                SetComponentEnabled<PlayerUpgradeHexagons>(entity, false);

                AddComponent(entity, new PlayerUpgradeIncreaseDamage { });
                SetComponentEnabled<PlayerUpgradeIncreaseDamage>(entity, false);
            }
        }
    }

    public struct PlayerUpgradeHexagons : IComponentData, IEnableableComponent
    {
    }

    public struct PlayerUpgradeIncreaseDamage : IComponentData, IEnableableComponent
    {
        public float ExtraDamage;
    }
}