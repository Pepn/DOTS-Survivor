using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace DOTSSurvivor
{
    // An authoring component is just a normal MonoBehavior.
    public class ExperienceAuthoring : MonoBehaviour
    {
        public float XPAmount = 1.0f;

        class Baker : Baker<ExperienceAuthoring>
        {
            public override void Bake(ExperienceAuthoring authoring)
            {
                // The entity will be moved
                var entity = GetEntity(TransformUsageFlags.Dynamic | TransformUsageFlags.NonUniformScale);
                AddComponent(entity, new XPData
                {
                    XPAmount = authoring.XPAmount,
                });
            }
        }
    }

    public struct XPData : IComponentData
    {
        public float XPAmount;
    }
}
