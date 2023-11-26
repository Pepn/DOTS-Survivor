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

        private float deltaTime = 0.0f;

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
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

            if (Time.frameCount % 10 == 0) // Update every 10 frames for better accuracy
            {
                float fps = 1.0f / deltaTime;
                fpsTmp.text = $"FPS: {fps:0.}"; // Format to display only whole numbers
            }
        }

        private void UpdateHealth(int health, int maxHealth)
        {
        }
    }
}
