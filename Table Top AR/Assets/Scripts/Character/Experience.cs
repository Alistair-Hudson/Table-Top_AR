using System.Collections;
using System.Collections.Generic;
using TableTopAR.Saving;
using UnityEngine;

namespace TableTopAR.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        private float totalExperience = 0;
        public float TotalExperience { get => totalExperience; }

        public void GainExperience(float gained)
        {
            totalExperience += gained;
        }

        public object CaptureState()
        {
            return totalExperience;
        }

        public void RestoreState(object state)
        {
            totalExperience = (float)state;
        }
    }
}