using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace DOTSSurvivor
{
    // An authoring component is just a normal MonoBehavior.
    public class MonsterAuthoring : MonoBehaviour
    {
        public float MovementSpeed = 5.0f;

        class Baker : Baker<MonsterAuthoring>
        {
            public override void Bake(MonsterAuthoring authoring)
            {
                // The entity will be moved
                var entity = GetEntity(TransformUsageFlags.Dynamic | TransformUsageFlags.NonUniformScale);
                AddComponent(entity, new MonsterData
                {
                    MovementSpeed = authoring.MovementSpeed,
                }) ;

                AddComponent(entity, new Unspawned());
            }
        }
    }

    public struct MonsterData : IComponentData
    {
        public float MovementSpeed;
    }

    public struct Unspawned : IComponentData { }
}
