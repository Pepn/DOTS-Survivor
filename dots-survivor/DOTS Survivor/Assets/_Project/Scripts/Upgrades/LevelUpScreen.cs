using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DOTSSurvivor
{
    public class LevelUpScreen : MonoBehaviour
    {
        [SerializeField] List<SkillSO> possibleSkills = new List<SkillSO>();

        List<SelectSkillButton> selectSkillButtons;
        [SerializeField] private GameObject containerPanel;
        // Start is called before the first frame update
        void Start()
        {
            selectSkillButtons = GetComponentsInChildren<SelectSkillButton>(true).ToList();
            LevelUp();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void LevelUp()
        {
            // populate UI
            int slot1, slot2, slot3;
            slot1 = Random.Range(0, possibleSkills.Count - 1);
            slot2 = Random.Range(0, possibleSkills.Count - 1);
            slot3 = Random.Range(0, possibleSkills.Count - 1);
            selectSkillButtons[0].Skill = possibleSkills[slot1];
            selectSkillButtons[1].Skill = possibleSkills[slot2];
            selectSkillButtons[2].Skill = possibleSkills[slot3];

            containerPanel.SetActive(true);

            //pause game
            GameStateManager.Instance.PauseGame();
        }

        public void OnDisable()
        {
            //pause game
            GameStateManager.Instance.UnpauseGame();
        }
    }
}
