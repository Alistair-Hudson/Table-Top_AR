using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.AI;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Effects
{
    [CreateAssetMenu(fileName = "TriggerAnimationEffect", menuName = "ScriptableObjects /AbilityStrategies/Effects/TriggerAnimationEffect")]
    public class TriggerAnimationEffect : EffectStrategy
    {
        [SerializeField]
        private string _animationTrigger = "";

        public override void StartEffect(AbilityData data, Action finished)
        {
            if (data.IsUserCaster)
            {
                Animator animator = data.User.GetComponent<Animator>();
                animator.SetTrigger(_animationTrigger);
            }
            finished?.Invoke();
        }
    }
}