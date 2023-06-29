using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "AutoTargeting", menuName = "ScriptableObjects /AbilityStrategies/Targeting/AutoTargeting")]
    public class AutoTargeting : TargetingStrategy
    {
        [SerializeField]
        private float _areaEffectRadius;

        public override void StartTargeting(AbilityData data, Action finished)
        {
            data.Targets = GetObjectsInRadius(data);
            finished();
        }

        private IEnumerable<GameObject> GetObjectsInRadius(AbilityData data)
        {
            var hits = Physics.SphereCastAll(data.TargetedPoint, _areaEffectRadius, Vector3.up, 0);
            foreach (var hit in hits)
            {
                yield return hit.collider.gameObject;
            }
        }
    }
}