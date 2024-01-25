using Unity.Entities;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace DOTSSurvivor
{
    public class GameControllerAuthoring : MonoBehaviour
    {
        public float MaxTime;

        class Baker : Baker<GameControllerAuthoring>
        {
            public override void Bake(GameControllerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new GameControllerData
                {
                    TimeLeft = authoring.MaxTime,
                    MaxTime = authoring.MaxTime,
                });
            }
        }
    }

    public struct GameControllerData : IComponentData
    {
        public float TimeLeft;
        public float MaxTime;

        /// <summary>
        /// value between 0 and 1
        /// </summary>
        public float PercentageTimeComplete
        {
            get
            {
                if (MaxTime == 0)
                {
                    return 0; // To avoid division by zero
                }

                return ((MaxTime - TimeLeft) / MaxTime);
            }
        }
    }
}
