using System;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace DOTSSurvivor
{
    public partial class DisplayInfoSystem : SystemBase
    {
        public Action<int> OnUpdateTotalEntities;

        protected override void OnCreate()
        {
            this.RequireForUpdate<MonsterData>();
        }
        protected override void OnUpdate()
        {
            OnUpdateTotalEntities?.Invoke(  GetEntityQuery(ComponentType.ReadOnly<MonsterData>()).CalculateEntityCount() + 
                                            GetEntityQuery(ComponentType.ReadOnly<ProjectileData>()).CalculateEntityCount() +
                                            GetEntityQuery(ComponentType.ReadOnly<XPData>()).CalculateEntityCount()
                                            );
        }
    }
}