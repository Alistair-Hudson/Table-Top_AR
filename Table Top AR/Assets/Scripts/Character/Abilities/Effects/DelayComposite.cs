using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.AI;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Effects
{
    [CreateAssetMenu(fileName = "DelayComposite", menuName = "ScriptableObjects /AbilityStrategies/Effects/DelayComposite")]
    public class DelayComposite : EffectStrategy
    {
        [SerializeField]
        private EffectStrategy[] _delayedEffects;

        private bool _abilityCast = false;

        public override void StartEffect(AbilityData data, Action finished)
        {
            var combatComponent = data.User.GetComponent<Combat>();
            combatComponent.OnAbilityCast.AddListener(AbilityCast);
            combatComponent.StartCoroutine(DelayedEffects(data, finished));
            finished?.Invoke();
        }

        private void AbilityCast()
        {
            _abilityCast = true;
        }

        private IEnumerator DelayedEffects(AbilityData data, Action finished)
        {
            yield return new WaitUntil(() => _abilityCast);
            foreach(var effect in _delayedEffects)
            {
                effect.StartEffect(data, finished);
            }
            data.User.GetComponent<Combat>().OnAbilityCast.RemoveListener(AbilityCast);
            _abilityCast = false;
        }
    }
}