using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Effects
{
    [CreateAssetMenu(fileName = "HOT", menuName = "ScriptableObjects /AbilityStrategies/Effects/HOT")]
    public class HOT : EffectStrategy
    {
        [SerializeField]
        private float _healPerSec = 0;
        [SerializeField]
        private float _runTime = 0;

        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach(var target in data.Targets)
            {
                Health targetHealth = target.GetComponent<Health>();
                targetHealth.StartCoroutine(RunHOT(targetHealth, data.User));
            }
            finished?.Invoke();
        }

        private IEnumerator RunHOT(Health health, GameObject user)
        {
            float time = 0;
            while (time < _runTime)
            {
                yield return null;
                health.RestoreHealth(_healPerSec * Time.deltaTime);
                if (health.IsDead)
                {
                    break;
                }
                time += Time.deltaTime;
            }
        }
    }
}