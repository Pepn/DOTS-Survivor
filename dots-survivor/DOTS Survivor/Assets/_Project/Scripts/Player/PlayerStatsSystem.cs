using System;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace DOTSSurvivor
{
    public partial class PlayerStateSystem : SystemBase
    {
        public Action<float, float> OnUpdateHealth;

        protected override void OnCreate()
        {
            this.RequireForUpdate<PlayerData>();
        }

        protected override void OnUpdate()
        {
            OnUpdateHealth.Invoke(SystemAPI.GetSingleton<PlayerData>().CurrentHealth, 10f);
        }
    }
}