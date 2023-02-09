using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TableTopAR.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 100)]
        [SerializeField]
        private int startingLevel = 1;
        [SerializeField]
        private CharacterClass characterClass;
        [SerializeField]
        Progression progression = null;

        private int currentLevel = 0;

        public int CurrentLevel { get => currentLevel; }

        private void Awake()
        {
            currentLevel = CalculateLevel();
        }

        public float GetStat(Stats stat)
        {
            return progression.GetStat(stat, characterClass, CalculateLevel());
        }

        public int CalculateLevel()
        {
            var experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;

            float currenXP = experience.TotalExperience;
            int penultimateLevel = progression.GetLevels(Stats.XPToLevel, characterClass);
            for (int i = 1; i <= penultimateLevel; i++)
            {
                float xpToLevel = progression.GetStat(Stats.XPToLevel, characterClass, i);
                if (currenXP < xpToLevel)
                {
                    return i;
                }
            }
            return penultimateLevel + 1;
        }
    }
}
