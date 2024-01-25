using System;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace DOTSSurvivor
{
    public partial class GameControllerSystem : SystemBase
    {
        // game state
        public Action<float> OnUpdateTime;
        private float timePassed = 0f;

        protected override void OnCreate()
        {
            this.RequireForUpdate<GameControllerData>();
        }

        protected override void OnUpdate()
        {
            var GameControllerData = SystemAPI.GetSingletonRW<GameControllerData>();
            timePassed += SystemAPI.Time.DeltaTime;
            GameControllerData.ValueRW.TimeLeft -= SystemAPI.Time.DeltaTime;

            // Check if one second has passed
            if (timePassed >= 1.0f)
            {
                OnUpdateTime(GameControllerData.ValueRW.TimeLeft);

                Debug.Log($"UPdate time! {GameControllerData.ValueRW.TimeLeft}");
                // Reset the timer
                timePassed = 0f;
            }
        }
    }
}