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

            foreach (var target in data.Targets)
            {
                AbilityData newAbilityData = new AbilityData(target, data.FilterStrategies, data.EffectStrategies);
                Chain(0, newAbilityData);
            }
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
            foreach (var filter in data.FilterStrategies)
            {
                data.Targets = filter.Filter(data.Targets);
            }

            foreach (var effect in data.EffectStrategies)
            {
                effect.StartEffect(data, EffectFinished);
            }
        }

        private void EffectFinished()
        {
            
        }
    }
}