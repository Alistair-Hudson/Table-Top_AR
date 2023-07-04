using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "SelfTagret", menuName = "ScriptableObjects /AbilityStrategies/Targeting/SelfTagret")]
    public class SelfTagret : TargetingStrategy
    {
        public override void StartTargeting(AbilityData data, Action finished)
        {
            data.Targets = new GameObject[] { data.User};
            data.TargetedPoint = data.User.transform.position;
            finished();
        }
    }
}