using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Effects
{
    [CreateAssetMenu(fileName = "SpawnTargetEffect", menuName = "ScriptableObjects /AbilityStrategies/Effects/SpawnTargetEffect")]
    public class SpawnTargetEffect : EffectStrategy
    {
        [SerializeField]
        private GameObject _prefabToSpawn = null;
        [SerializeField]
        private float _destroyDelay = -1;

        public override void StartEffect(AbilityData data, Action finished)
        {
            var newInstance = Instantiate(_prefabToSpawn, data.TargetedPoint, Quaternion.identity);
            if (_destroyDelay >= 0)
            {
                Destroy(newInstance, _destroyDelay);
            }
            finished?.Invoke();
        }
    }
}