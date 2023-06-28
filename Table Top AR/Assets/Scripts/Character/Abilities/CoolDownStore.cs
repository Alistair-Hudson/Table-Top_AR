using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TableTopAR.Character.Abilities
{
    public class CoolDownStore : MonoBehaviour
    {
        private Dictionary<GenericAbility, float> _cooldownTimers = new Dictionary<GenericAbility, float>();

        private void Update()
        {
            var keys = new List<GenericAbility>(_cooldownTimers.Keys);
            foreach (var key in keys)
            {
                _cooldownTimers[key] = Mathf.Max(0, _cooldownTimers[key] -= Time.deltaTime);
            }
        }

        public void StartCoolDown(GenericAbility ability, float cooldownTime)
        {
            if (!_cooldownTimers.ContainsKey(ability))
            {
                _cooldownTimers.Add(ability, cooldownTime);
            }
            else
            {
                _cooldownTimers[ability] = cooldownTime;
            }
        }

        public float CooldownTimeRemaining(GenericAbility ability)
        {
            if (!_cooldownTimers.ContainsKey(ability))
            {
                return 0;
            }
            return _cooldownTimers[ability];
        }

        public float CooldownFraction(GenericAbility ability)
        {
            if (!_cooldownTimers.ContainsKey(ability))
            {
                return 0;
            }
            return _cooldownTimers[ability] / ability.CoolDownTime;
        }
    }
}