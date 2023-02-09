using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "ScriptableObjects/Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [System.Serializable]
        private class ProgressionCharacterClass
        {
            [SerializeField]
            private CharacterClass characterClass;
            public CharacterClass CharacterClass { get => characterClass; }
            [SerializeField]
            private ProgressionStat[] stats;
            public ProgressionStat[] Stats { get => stats; }
        }

        [System.Serializable]
        private class ProgressionStat
        {
            [SerializeField]
            private Stats stat;
            public Stats Stat { get => stat; }
            [SerializeField]
            private float[] levels;
            public float[] Levels { get => levels; }
        }

        [SerializeField]
        private ProgressionCharacterClass[] progressionCharacter = null;

        private Dictionary<CharacterClass, Dictionary<Stats, float[]>> statLookUp = null;

        public float GetStat(Stats stat, CharacterClass characterClass, int level)
        {
            if (statLookUp == null)
            {
                BuildLookUp();
            }
            
            float[] levels = statLookUp[characterClass][stat];
            return (level <= levels.Length ? levels[level - 1] : levels[levels.Length - 1]);
        }

        private void BuildLookUp()
        {
            statLookUp = new Dictionary<CharacterClass, Dictionary<Stats, float[]>>();
            foreach (var pc in progressionCharacter)
            {
                var statTable = new Dictionary<Stats, float[]>();
                foreach (ProgressionStat ps in pc.Stats)
                {
                    statTable.Add(ps.Stat, ps.Levels);
                }
                statLookUp.Add(pc.CharacterClass, statTable);
            }
        }

        public int GetLevels(Stats stat, CharacterClass characterClass)
        {
            if (statLookUp == null)
            {
                BuildLookUp();
            }

            float[] levles = statLookUp[characterClass][stat];
            return levles.Length;
        }
    }
}