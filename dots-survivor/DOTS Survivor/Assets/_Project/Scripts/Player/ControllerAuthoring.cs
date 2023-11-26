using Unity.Entities;
using UnityEngine;

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
                });
            }
        }
    }

    public struct Controller : IComponentData
    {
        public float player_speed;
    }
}

