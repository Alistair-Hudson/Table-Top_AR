using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Character
{
    public class Experience : MonoBehaviour
    {
        private int totalExperience = 0;

        public void GainExperience(int gained)
        {
            totalExperience += gained;
        }
    }
}