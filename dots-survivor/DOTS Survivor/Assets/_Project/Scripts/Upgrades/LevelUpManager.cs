using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DOTSSurvivor
{
    public class LevelUpManager : MonoBehaviour
    {
        int currentLevel = 1;
        List<float> levelUpBoundaries = new List<float>();
        private LevelUpScreen levelUpScreen;

        private void Awake()
        {
            levelUpScreen = FindObjectOfType<LevelUpScreen>();
        }

        void Start()
        {
            FillLevelUpArray();

            var DisplayInfoSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerStateSystem>();
            if (DisplayInfoSystem != null)
            {
                DisplayInfoSystem.OnUpdateXP += UpdateLevel;
            }
        }

        void UpdateLevel(float totalXP)
        {
            if(totalXP > levelUpBoundaries[currentLevel])
            {
                Debug.LogWarning($"LEVEL UP! {currentLevel} {totalXP}");
                levelUpScreen.LevelUp();
                currentLevel++;
            }
        }

        float CalculateXPForLevel(int level)
        {
            return (float)math.floor((1.0 / 4.0) * (2 * math.pow(level, 3) / 3));
        }

        void FillLevelUpArray()
        {
            //Debug.Log("Level\tXP Required");

            for (int level = 1; level <= 100; level++)
            {
                float xpRequired = CalculateXPForLevel(level);
                levelUpBoundaries.Add(xpRequired);
                Debug.Log($"{level}\t{xpRequired}");
            }
        }
    }
}
