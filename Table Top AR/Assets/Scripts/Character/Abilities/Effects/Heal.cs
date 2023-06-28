using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Heal", menuName = "ScriptableObjects /AbilityStrategies/Effects/Heal")]
    public class Heal : EffectStrategy
    {
        [SerializeField]
        private float _heal = 0;

        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach(var target in data.Targets)
            {
                target.GetComponent<Health>().RestoreHealth(_heal);
            }
            finished?.Invoke();
        }
    }
}