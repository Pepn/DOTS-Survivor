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
        public float Score { get; private set; }
        [SerializeField] MMF_Player OnIncreaseMultiplier;
        [SerializeField] TextMeshProUGUI _multiplierTmp;
        public float Multiplier { get; private set; }
        PlayerStateSystem _displayInfoSystem;
        private void OnEnable()
        {
            _displayInfoSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerStateSystem>();
            if (_displayInfoSystem != null)
            {
                // score
                _displayInfoSystem.OnUpdateScore += (float score) => Score = score;
                _displayInfoSystem.OnUpdateScore += (float score) => OnIncreaseScore.PlayFeedbacks();
                _displayInfoSystem.OnUpdateScore += (float score) => UpdateText(score, "Score:", _scoreTmp);

                // multiplier
                _displayInfoSystem.OnUpdateScore += (float multiplier) => Multiplier = multiplier;
                _displayInfoSystem.OnUpdateScoreMultiplier += (float multiplier) => OnIncreaseMultiplier.PlayFeedbacks();
                _displayInfoSystem.OnUpdateScoreMultiplier += (float multiplier) => UpdateText(multiplier, "Multiplier:", _multiplierTmp);
            }
            else
            {
                Debug.LogWarning("DisplayInfoSystem not found !");
            }
        }

        private void OnDisable()
        {
            if (_displayInfoSystem != null)
            {
                // score
                _displayInfoSystem.OnUpdateScore -= (float score) => Score = score;
                _displayInfoSystem.OnUpdateScore -= (float score) => OnIncreaseScore.PlayFeedbacks();
                _displayInfoSystem.OnUpdateScore -= (float score) => UpdateText(score, "Score", _scoreTmp);

                // multiplier
                _displayInfoSystem.OnUpdateScore -= (float multiplier) => Multiplier = multiplier;
                _displayInfoSystem.OnUpdateScoreMultiplier -= (float multiplier) => OnIncreaseMultiplier.PlayFeedbacks();
                _displayInfoSystem.OnUpdateScoreMultiplier -= (float multiplier) => UpdateText(multiplier, "Multiplier:", _multiplierTmp);
            }
            else
            {
                Debug.LogWarning("DisplayInfoSystem not found !");
            }
        }

        private void UpdateText(float score, string text, TextMeshProUGUI tmp)
        {
            tmp.text = $"{text}{score}";
        }
    }
}
