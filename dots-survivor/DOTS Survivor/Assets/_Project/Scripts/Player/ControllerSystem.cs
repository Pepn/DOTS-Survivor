using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DOTSSurvivor
{
    [UpdateAfter(typeof(InputSystem))]
    public partial struct ControllerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputState>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var input = SystemAPI.GetSingleton<InputState>();

            foreach (var (transform, controller) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRW<Controller>>())
            {
                // Move around with WASD
                var move = new float3(input.Horizontal, input.Vertical, 0);
                move = move * controller.ValueRO.player_speed * SystemAPI.Time.DeltaTime;
                transform.ValueRW.Position += move;
            }
        }
    }
}
