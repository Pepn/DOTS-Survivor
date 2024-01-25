using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Feedbacks;
using Unity.Entities;

namespace DOTSSurvivor
{
    public class TimerDisplay : MonoBehaviour
    {
        [SerializeField] MMF_Player OnLowTime;
        [SerializeField] TextMeshProUGUI _timeTmp;
        [field: SerializeField] public float TimeLeft { get; private set; }

        private GameControllerSystem gameControllerSystem;

        private void OnEnable()
        {
            gameControllerSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<GameControllerSystem>();
            if (gameControllerSystem != null)
            {
                // score
                gameControllerSystem.OnUpdateTime += (float time) => TimeLeft = time;
                //gameControllerSystem.OnUpdateScore += (float score) => OnIncreaseScore.PlayFeedbacks();
                gameControllerSystem.OnUpdateTime += (float time) => UpdateText(time, "Survive:\n", _timeTmp);
            }
            else
            {
                Debug.LogWarning("DisplayInfoSystem not found !");
            }
        }

        private void OnDisable()
        {
            if (gameControllerSystem != null)
            {
                gameControllerSystem.OnUpdateTime -= (float time) => TimeLeft = time;
                //gameControllerSystem.OnUpdateScore += (float score) => OnIncreaseScore.PlayFeedbacks();
                gameControllerSystem.OnUpdateTime -= (float time) => UpdateText(time, "Survive:\n", _timeTmp);
            }
            else
            {
                Debug.LogWarning("DisplayInfoSystem not found !");
            }
        }

        private void UpdateText(float time, string text, TextMeshProUGUI tmp)
        {
            tmp.text = $"{text}{time.ToString("0")}";
        }
    }
}
