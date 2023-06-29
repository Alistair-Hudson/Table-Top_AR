using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.AI;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Effects
{
    [CreateAssetMenu(fileName = "OrientToTarget", menuName = "ScriptableObjects /AbilityStrategies/Effects/OrientToTarget")]
    public class OrientToTarget : EffectStrategy
    {
        public override void StartEffect(AbilityData data, Action finished)
        {
            if (data.IsUserCaster)
            {
                data.User.transform.LookAt(data.TargetedPoint);
            }
            finished?.Invoke();
        }
    }
}