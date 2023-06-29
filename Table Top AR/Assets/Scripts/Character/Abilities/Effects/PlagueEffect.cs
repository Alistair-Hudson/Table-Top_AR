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
        [SerializeField]
        private AutoTargeting _autoTarget = null;
        [SerializeField]
        private FilterStrategy[] _filterStrategies;
        [SerializeField]
        private List<EffectStrategy> _effectStrategies = new List<EffectStrategy>();

        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach (var target in data.Targets)
            {
                var targetHealth = target.GetComponent<Health>();
                if (targetHealth.IsDead)
                {
                    continue;
                }
                else
                {
                    targetHealth.StartCoroutine(PlagueCountDown(targetHealth, data));
                    Debug.Log($"Plague has spread to {targetHealth.gameObject.name}");
                    break;
                }
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
                    SpreadPlague(targetsHealth, data);
                    break;
                }

                time += Time.deltaTime;
            }
        }

        private void SpreadPlague(Health targetsHealth, AbilityData data)
        {
            data.TargetedPoint = targetsHealth.transform.position;
            _autoTarget.StartTargeting(data, () =>
            {
                TargetAquired(data);
            });
        }

        private void TargetAquired(AbilityData data)
        {
            foreach (var filter in _filterStrategies)
            {
                data.Targets = filter.Filter(data.Targets);
            }

            foreach (var effect in _effectStrategies)
            {
                effect.StartEffect(data, EffectFinished);
            }
        }

        private void EffectFinished()
        {
            
        }
    }
}