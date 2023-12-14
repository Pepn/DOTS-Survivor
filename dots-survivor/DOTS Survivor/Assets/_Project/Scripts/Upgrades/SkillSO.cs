using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DOTSSurvivor
{
    [CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill")]
    public class SkillSO : ScriptableObject
    {
        public string Name;
        public string Description;
        public Image skillDesciptionImage;
        public GameObject ProjectileShooterPrefab;
    }
}
