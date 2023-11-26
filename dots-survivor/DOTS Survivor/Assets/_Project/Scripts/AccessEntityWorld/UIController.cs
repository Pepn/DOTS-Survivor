using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using UnityEngine;

namespace DOTSSurvivor
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI entityCountTmp;
        [SerializeField] private TextMeshProUGUI fpsTmp;

        private void OnEnable()
        {
            var DisplayInfoSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<DisplayInfoSystem>();
            if(DisplayInfoSystem != null)
            {
                DisplayInfoSystem.OnUpdateTotalEntities += UpdateEntitiesCount;
            }
            else
            {
                Debug.LogWarning("DisplayInfoSystem not found !");
            }
        }

        private void UpdateEntitiesCount(int count)
        {
            entityCountTmp.text = $"ENTS: {count.ToString()}";

        }

        private void Update()
        {
            fpsTmp.text = $"FPS: {(1.0f / Time.deltaTime).ToString(".")}";
        }

        private void UpdateHealth(int health, int maxHealth)
        {
        }
    }
}
