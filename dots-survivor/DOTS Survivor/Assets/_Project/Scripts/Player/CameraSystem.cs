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
                var controllerTransform = transformLookup[controllerEntity];
                var cameraTransform = Camera.main.transform;
           
                //default
                var newCamPos = controllerTransform.Position + new float3(0, 0, -20);
                cameraTransform.rotation = new Quaternion(0,0,0,0);
                //angled
                //var newCamPos = controllerTransform.Position + new float3(4.11f, -5.17f, -11.36f);
                //cameraTransform.rotation = new Quaternion(-0.293f, -0.0757f, 0.322f, 0.897f);

                //third person
                //var newCamPos = controllerTransform.Position + new float3(0, -2.2f, -3.3f);
                //cameraTransform.rotation = new Quaternion(-0.437f, 0, 0, 0.899f);

                cameraTransform.position = math.lerp(cameraTransform.position, newCamPos, 0.3f);
            }
        }
    }
}
