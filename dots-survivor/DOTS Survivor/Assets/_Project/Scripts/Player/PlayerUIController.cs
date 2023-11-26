using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using UnityEngine;

namespace DOTSSurvivor
{
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hpTmp;
        [SerializeField] private TextMeshProUGUI xpTmp;

        private void OnEnable()
        {
            var DisplayInfoSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerStateSystem>();
            if (DisplayInfoSystem != null)
            {
                DisplayInfoSystem.OnUpdateHealth += UpdateHP;
            }
            else
            {
                Debug.LogWarning("DisplayInfoSystem not found !");
            }
        }

        private void UpdateHP(float hp, float maxHealth)
        {
            hpTmp.text = $"HP asdf: {hp.ToString()} MAX: {maxHealth}";
        }

        private void UpdateXP(float xp)
        {
            hpTmp.text = $"XP: {xp.ToString()}";
        }

        private void Update()
        {
        }
    }
}
