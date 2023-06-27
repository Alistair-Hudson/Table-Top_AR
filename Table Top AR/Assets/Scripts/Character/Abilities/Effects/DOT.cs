using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Effects
{
    [CreateAssetMenu(fileName = "DOT", menuName = "ScriptableObjects /AbilityStrategies/Effects/DOT")]
    public class DOT : EffectStrategy
    {
        [SerializeField]
        private float _damagePerSec = 0;
        [SerializeField]
        private float _runTime = 0;

        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach(var target in data.Targets)
            {
                Health targetHealth = target.GetComponent<Health>();
                targetHealth.StartCoroutine(RunDOT(targetHealth, data.User));
            }
            finished?.Invoke();
        }

        private IEnumerator RunDOT(Health health, GameObject user)
        {
            float time = 0;
            while (time < _runTime)
            {
                yield return null;
                health.TakeDamage(user, _damagePerSec * Time.deltaTime);
                if (health.IsDead)
                {
                    break;
                }
                time += Time.deltaTime;
            }
        }
    }
}