using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Items.Equipment;
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
        private Progression progression = null;
        [SerializeField]
        private bool useModifiers = false;

        private int currentLevel = 0;
        private Experience experience;

        public int CurrentLevel { get => currentLevel; }

        public event Action OnStatsUpdate;

        private void Awake()
        {
            experience = GetComponent<Experience>();
            GetComponent<CharacterEquipment>().EquipmentUpdated += UpdateStats;
        }

        private void Start()
        {
            currentLevel = CalculateLevel();
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void OnDestroy()
        {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel != currentLevel)
            {
                currentLevel = newLevel;
                OnStatsUpdate();
            }
        }

        private void UpdateStats()
        {
            OnStatsUpdate();
        }

        public float GetStat(Stats stat)
        {
            float statTotal = progression.GetStat(stat, characterClass, CalculateLevel());
            if (useModifiers)
            {
                statTotal += AdditiveModifiers(stat);
                statTotal *= PercentageModifiers(stat);
            }
            return statTotal;
        }

        private float PercentageModifiers(Stats stat)
        {
            float percentageTotal = 100;
            var modifiers = GetComponents<IStatModifier>();
            foreach (var modifier in modifiers)
            {
                var statMods = modifier.GetPercentageModifier(stat);
                foreach (var statMod in statMods)
                {
                    percentageTotal += statMod;
                }
            }

            return percentageTotal / 100;
        }

        private float AdditiveModifiers(Stats stat)
        {
            float statTotal = 0;
            var modifiers = GetComponents<IStatModifier>();
            foreach (var modifier in modifiers)
            {
                var statMods = modifier.GetAdditiveModifier(stat);
                foreach (var statMod in statMods)
                {
                    statTotal += statMod;
                }
            }

            return statTotal;
        }

        public int CalculateLevel()
        {
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
