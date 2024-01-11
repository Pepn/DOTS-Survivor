using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Feedbacks;
using Unity.Entities;

namespace DOTSSurvivor
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] MMF_Player OnIncreaseScore;
        [SerializeField] TextMeshProUGUI _scoreTmp;

        private void Awake()
        {
        }
        private void OnEnable()
        {
            var DisplayInfoSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<DisplayInfoSystem>();
            if (DisplayInfoSystem != null)
            {
                DisplayInfoSystem.OnUpdateTotalEntities += (int score) => OnIncreaseScore.PlayFeedbacks();
                DisplayInfoSystem.OnUpdateTotalEntities += (int score) => UpdateText(score);
            }
        }

        private void OnDisable()
        {
            var DisplayInfoSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<DisplayInfoSystem>();
            if (DisplayInfoSystem != null)
            {
                DisplayInfoSystem.OnUpdateTotalEntities -= (int score) => OnIncreaseScore.PlayFeedbacks();
                DisplayInfoSystem.OnUpdateTotalEntities -= (int score) => UpdateText(score);
            }
        }

        private void UpdateText(int score)
        {
            _scoreTmp.text = $"Score: {score}";
        }
    }
}
