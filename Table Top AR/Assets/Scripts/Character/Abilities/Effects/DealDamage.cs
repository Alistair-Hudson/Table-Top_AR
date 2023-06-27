using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Effects
{
    [CreateAssetMenu(fileName = "DealDamage", menuName = "ScriptableObjects /AbilityStrategies/Effects/DealDamage")]
    public class DealDamage : EffectStrategy
    {
        [SerializeField]
        private float _damage = 0;

        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach(var target in data.Targets)
            {
                target.GetComponent<Health>().TakeDamage(data.User, _damage);
            }
            finished?.Invoke();
        }
    }
}