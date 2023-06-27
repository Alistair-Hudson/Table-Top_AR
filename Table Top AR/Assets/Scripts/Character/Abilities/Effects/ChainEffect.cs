using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.AI;
using TableTopAR.Character.Abilities.Targeting;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Effects
{
    [CreateAssetMenu(fileName = "ChainEffect", menuName = "ScriptableObjects /AbilityStrategies/Effects/ChainEffect")]
    public class ChainEffect : EffectStrategy
    {
        [SerializeField]
        private int _maxChain = 10;
        [SerializeField]
        private bool _canRetarget = false;

        public override void StartEffect(AbilityData data, Action finished)
        {

            Chain(0, data);
            finished?.Invoke();
        }

        private void Chain(int chained, AbilityData data)
        {
            if (chained >= _maxChain)
            {
                return;
            }

            var newTargeting = ScriptableObject.CreateInstance(typeof(AutoTargeting)) as AutoTargeting;
            newTargeting.StartTargeting(data, () =>
            {
                TargetAquired(data);
            });
            
        }

        private void TargetAquired(AbilityData data)
        {

        }
    }
}