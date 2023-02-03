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

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (var pc in progressionCharacter)
            {
                if (pc.CharacterClass == characterClass)
                {
                    //return pc.Health[level - 1];
                }
            }
            return 0;
        }
    }
}