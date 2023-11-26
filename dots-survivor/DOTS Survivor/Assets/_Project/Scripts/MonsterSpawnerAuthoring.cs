using Unity.Entities;
using UnityEngine;

namespace DOTSSurvivor
{
    // An authoring component is just a normal MonoBehavior that has a Baker<T> class.
    public class MonsterSpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;

        class Baker : Baker<MonsterSpawnerAuthoring>
        {
            public override void Bake(MonsterSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new MonsterSpawner
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }

    struct MonsterSpawner : IComponentData
    {
        public Entity Prefab;
    }
}
