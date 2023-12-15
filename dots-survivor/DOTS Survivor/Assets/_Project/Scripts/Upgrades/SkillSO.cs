using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DOTSSurvivor
{
    [CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill")]
    public class SkillSO : ScriptableObject
    {
        public string Name;
        public string Description;
        public Image skillDesciptionImage;
        public GameObject ProjectileShooterPrefab;
        public UnityEvent skillFunction;


        public void AddSkillToPlayer()
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            EntityQuery query = entityManager.CreateEntityQuery(typeof(PlayerData));
            Entity entity = query.GetSingletonEntity();

            if (!entityManager.HasComponent<PlayerUpgradeHexagons>(entity))
            {
                Debug.LogError("PLAYER HAS NO UPGRADEA ATTACHED..");
            }
            else
            {
                Debug.Log("Update Player Component!");
                entityManager.SetComponentEnabled<PlayerUpgradeHexagons>(entity, true);
            }
        }
        public void PlayerUpgradeIncreaseDamage()
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            EntityQuery query = entityManager.CreateEntityQuery(typeof(PlayerData));
            Entity entity = query.GetSingletonEntity();

            if (!entityManager.HasComponent<PlayerUpgradeIncreaseDamage>(entity))
            {
                Debug.LogError("PLAYER HAS NO UPGRADEA ATTACHED..");
            }
            else
            {
                Debug.Log("Update Player Component!");
                entityManager.SetComponentEnabled<PlayerUpgradeIncreaseDamage>(entity, true);

                //update stats
                var data = entityManager.GetComponentData<PlayerUpgradeIncreaseDamage>(entity);
                data.ExtraDamage = 42.0f;
                entityManager.SetComponentData(entity, data);
            }
        }
    }
}
