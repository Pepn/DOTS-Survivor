using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace DOTSSurvivor
{
    [UpdateAfter(typeof(ControllerSystem))]
    public partial struct CameraSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Controller>();
        }

        // This OnUpdate accesses managed objects and so cannot be Burst-compiled
        public void OnUpdate(ref SystemState state)
        {
            if (Camera.main != null)
            {
                var transformLookup = SystemAPI.GetComponentLookup<LocalTransform>(true);
                var controllerEntity = SystemAPI.GetSingletonEntity<Controller>();
                var controller = SystemAPI.GetSingleton<Controller>();
                var controllerTransform = transformLookup[controllerEntity];
                var cameraTransform = Camera.main.transform;

                var newCamPos = controllerTransform.Position + new float3(0, 0, -20);
                // Set the camera position to the player's position
                cameraTransform.position = math.lerp(cameraTransform.position, newCamPos, 0.3f);

                // Look at the player's position
                //cameraTransform.LookAt(controllerTransform.Position);
                //cameraTransform.rotation = new Quaternion(0,0,0,0);
                //cameraTransform.rotation = math.mul(controllerTransform.Rotation,
                //    quaternion.RotateX(controller.camera_pitch));
            }
        }
    }
}
