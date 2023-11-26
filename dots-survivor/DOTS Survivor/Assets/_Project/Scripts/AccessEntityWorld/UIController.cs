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

        private void OnEnable()
        {
            var DisplayInfoSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<DisplayInfoSystem>();
            if(DisplayInfoSystem != null)
            {
                DisplayInfoSystem.OnUpdateHealth += UpdateHealth;
                DisplayInfoSystem.OnUpdateTotalEntities += UpdateEntitiesCount;
            }
            else
            {
                Debug.LogWarning("DisplayInfoSystem not found !");
            }
        }

        private void UpdateEntitiesCount(int count)
        {
            entityCountTmp.text = count.ToString();
        }

        private void UpdateHealth(int health, int maxHealth)
        {
        }
    }
}
