using DOTSSurvivor;
using Unity.Entities;
using UnityEngine;

// TODO AUDIO: Make it just like we send commands from ECS -> normal Unity, but now instead of upping score we play audio from pool
public class AudioSourceAuthoring : MonoBehaviour
{
    public GameObject audioPfb;

    class Baker : Baker<AudioSourceAuthoring>
    {
        public override void Bake(AudioSourceAuthoring authoring)
        {
            // The entity will be moved
            var entity = GetEntity(TransformUsageFlags.Dynamic | TransformUsageFlags.NonUniformScale);
            AddComponent(entity, new AudioSourceData
            {
                audioSource = GetEntity(authoring.audioPfb, TransformUsageFlags.Dynamic),
            });
        }
    }
}

public struct AudioSourceData : IComponentData
{
    public Entity audioSource;
}
