using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.AI;
using TableTopAR.Character.Abilities.Targeting;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Effects
{
    [CreateAssetMenu(fileName = "PlagueEffect", menuName = "ScriptableObjects /AbilityStrategies/Effects/PlagueEffect")]
    public class PlagueEffect : EffectStrategy
    {
        [SerializeField]
        private float plagueTime = 10;

        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach (var target in data.Targets)
            {
                var targetHealth = target.GetComponent<Health>();
                targetHealth.StartCoroutine(PlagueCountDown(targetHealth, data));
            }
            finished?.Invoke();
        }

        private IEnumerator PlagueCountDown(Health targetsHealth, AbilityData data)
        {
            float time = 0;
            while (time < plagueTime)
            {
                yield return null;
                if (targetsHealth.IsDead)
                {
                    AbilityData newData = new AbilityData(targetsHealth.gameObject, data.FilterStrategies, data.EffectStrategies);
                    var newTargeting = ScriptableObject.CreateInstance(typeof(AutoTargeting)) as AutoTargeting;
                    newTargeting.StartTargeting(newData, () =>
                    {
                        TargetAquired(newData);
                    });

                    break;
                }

                time += Time.deltaTime;
            }
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