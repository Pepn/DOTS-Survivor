using DOTSSurvivor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkillButton : MonoBehaviour
{
    private SkillSO skill;
    [SerializeField] TextMeshProUGUI title, description;
    [SerializeField] Image skillDesciptionImage;

    public SkillSO Skill {
        get
        {
            return skill;
        }
        set
        {
            skill = value;
            PopulateUI();
        } 
    }

    private void PopulateUI()
    {
        title.text = skill.Name;
        description.text = skill.Description;
        skillDesciptionImage = skill.skillDesciptionImage;
    }

    public void SelectSkill()
    {
        Debug.LogWarning("Test Clicky");
        skill.skillFunction?.Invoke();
        transform.parent.gameObject.SetActive(false);

        // unpause game
        GameStateManager.Instance.UnpauseGame();
    }
}
