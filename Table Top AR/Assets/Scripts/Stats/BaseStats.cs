using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TableTopAR.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 100)]
        [SerializeField]
        private int statringLevel = 1;
        [SerializeField]
        private CharacterClass characterClass;
        [SerializeField]
        Progression progression = null;
        public float GetHealth()
        {
            return progression.GetHealth(characterClass, statringLevel);
        }

        public int GetXPReward()
        {
            return 10;
        }
    }
}
