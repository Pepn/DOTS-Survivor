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
        private float hp, xp, score;
        private float scoreMultiplier;

        private void OnEnable()
        {
            var DisplayInfoSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerStateSystem>();
            if (DisplayInfoSystem != null)
            {
                DisplayInfoSystem.OnUpdateHealth += UpdateHP;
                DisplayInfoSystem.OnUpdateXP += UpdateXP;
            }
            else
            {
                Debug.LogWarning("DisplayInfoSystem not found !");
            }
        }

        private void UpdateHP(float hp, float maxHealth)
        {
            this.hp = hp;
            hpTmp.text = $"HP asdf: {this.hp} MAX: {maxHealth}";

            // game over
            if(hp <= 0)
            {
                GameStateManager.Instance.PauseGame();
                FindObjectOfType<DeathScreen>(true).gameObject.SetActive(true);
            }
        }

        private void UpdateXP(float xp)
        {
            this.xp = xp;
            xpTmp.text = $"XP: {this.xp}";
        }

        private void Update()
        {
        }
    }
}
