using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace DOTSSurvivor
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial struct PlayerShootingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (projectileShooter, localToWorld) in
                     SystemAPI.Query<ProjectileShooter, RefRO<LocalToWorld>>()
                         .WithAll<ProjectileShooter>())
            {
                Entity instance = state.EntityManager.Instantiate(projectileShooter.Projectile);

                state.EntityManager.SetComponentData(instance, new LocalTransform
                {
                    Position = new float3(0,0,0),//SystemAPI.GetComponent<LocalToWorld>(projectileShooter.CannonBallSpawn).Position,
                    Rotation = quaternion.identity,
                    Scale = 1.0f, //SystemAPI.GetComponent<LocalTransform>(turret.CannonBallPrefab).Scale
                });

                state.EntityManager.SetComponentData(instance, new Projectile
                {
                    Velocity = localToWorld.ValueRO.Up * 20.0f
                });

                //state.EntityManager.SetComponentData(instance, new URPMaterialPropertyBaseColor
                //{
                //    Value = turret.Color
                //});
            }
        }
    }
}
