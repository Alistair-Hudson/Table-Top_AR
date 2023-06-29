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
                if (!_canRetarget && data.ChainedTargets.Contains(target))
                {
                    continue;
                }
                else
                {
                    Debug.Log($"Chain targeted {target.name}");
                    data.TargetedPoint = target.transform.position;
                    data.ChainedTargets.Add(target);
                    data.Chained++;
                    Chain(data);
                    break;
                }
            }
            finished?.Invoke();
        }

        private void Chain(AbilityData data)
        {
            if (data.Chained >= _maxChain)
            {
                return;
            }

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