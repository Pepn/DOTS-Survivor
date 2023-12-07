using Unity.Entities;
using UnityEngine;

namespace DOTSSurvivor
{
    public class ExperienceSpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float SpawnChance;

        class Baker : Baker<ExperienceSpawnerAuthoring>
        {
            public override void Bake(ExperienceSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new ExperienceSpawner
                {
                    XPPrefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    SpawnChance = authoring.SpawnChance,
                });
            }
        }
    }

    struct ExperienceSpawner : IComponentData
    {
        public Entity XPPrefab;
        public float SpawnChance;
    }
}