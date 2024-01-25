using Unity.Entities;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace DOTSSurvivor
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float MaxHealth;
        public float CurrentHealth;
        public float CurrentXP;
        public int CurrentLevel;
        public float Score, ScoreMultiplier;

        class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerData
                {
                    MaxHealth = authoring.MaxHealth,
                    CurrentHealth = authoring.CurrentHealth,
                    CurrentXP = authoring.CurrentXP,
                    CurrentLevel = authoring.CurrentLevel,
                    Score = authoring.Score,
                    ScoreMultiplier = authoring.ScoreMultiplier,
                });
            }
        }
    }

    public struct PlayerData : IComponentData
    {
        public float MaxHealth;
        public float CurrentHealth;
        public float CurrentXP;
        public int CurrentLevel;
        public float Score;
        public float ScoreMultiplier;
    }
}
