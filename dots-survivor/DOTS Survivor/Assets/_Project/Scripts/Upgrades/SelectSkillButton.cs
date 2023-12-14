using DOTSSurvivor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkillButton : MonoBehaviour
{
    private SkillSO skill;
    [SerializeField] TextMeshProUGUI title, description;
    [SerializeField] Image skillDesciptionImage;

    public SkillSO Skill {
        get
        {
            return skill;
        }
        set
        {
            skill = value;
            PopulateUI();
        } 
    }

    private void PopulateUI()
    {
        title.text = skill.Name;
        description.text = skill.Description;
        skillDesciptionImage = skill.skillDesciptionImage;
    }

    public void SelectSkill()
    {
        Debug.LogWarning("Test Clicky");
        AddSkillToPlayer();
        transform.parent.gameObject.SetActive(false);
    }

    public void AddSkillToPlayer()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        // Find an existing entity with MyComponent
        EntityQuery query = entityManager.CreateEntityQuery(typeof(PlayerData));
        Entity entity = query.GetSingletonEntity();

        // Create an entity and add the MyComponent to it
        Entity pj = entityManager.CreateEntity(typeof(ProjectileShootingAuthoring));

        // If the entity doesn't have MyComponent, add it
        //if (!entityManager.HasComponent<ProjectileShooter>(entity))
        //{
        //    // Add MyComponent to the existing entity
        //    entityManager.AddComponentData(entity, new ProjectileShooter {
        //        Speed = 25,//math.clamp(Unity.Mathematics.Random.CreateFromIndex(0).NextFloat(), 10f, 100f),
        //        ProjectileLifeTime = 5,
        //        Projectile = pj
        //    });
        //}
        //else
        //{
        //    Debug.Log("Update Player Component!");
        //    // If the entity already has MyComponent, update its data
        //    var myComponentData = entityManager.GetComponentData<ProjectileShooter>(entity);
        //    myComponentData.Speed = 42.0f;
        //    entityManager.SetComponentData(entity, myComponentData);
        //}
    }
}
