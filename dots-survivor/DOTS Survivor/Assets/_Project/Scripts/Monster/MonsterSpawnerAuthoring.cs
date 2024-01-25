using Unity.Entities;
using UnityEngine;

namespace DOTSSurvivor
{
    // An authoring component is just a normal MonoBehavior that has a Baker<T> class.
    public class MonsterSpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float SpawnCooldownStart;
        public float SpawnCooldownEnd;
        public int SpawnAmountStart;
        public int SpawnAmountEnd;
        

        class Baker : Baker<MonsterSpawnerAuthoring>
        {
            public override void Bake(MonsterSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new MonsterSpawner
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    SpawnCooldownStart = authoring.SpawnCooldownStart,
                    SpawnCooldownEnd = authoring.SpawnCooldownEnd,
                    SpawnAmountStart = authoring.SpawnAmountStart,
                    SpawnAmountEnd = authoring.SpawnAmountEnd,
                });
            }
        }
    }

    struct MonsterSpawner : IComponentData
    {
        public Entity Prefab;
        public float SpawnCooldownStart;
        public float SpawnCooldownEnd;
        public int SpawnAmountStart;
        public int SpawnAmountEnd;
    }
}
