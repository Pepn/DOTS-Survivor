using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DOTSSurvivor
{
    public class ControllerAuthoring : MonoBehaviour
    {
        public float player_speed = 5.0f;

        class Baker : Baker<ControllerAuthoring>
        {
            public override void Bake(ControllerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Controller
                {
                    player_speed = authoring.player_speed,
                    Direction = float3.zero,
                });
            }
        }
    }

    public struct Controller : IComponentData
    {
        public float player_speed;
        [ReadOnly] public float3 Direction;
    }
}

