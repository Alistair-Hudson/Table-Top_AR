using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.AI;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Effects
{
    [CreateAssetMenu(fileName = "AgroEnemies", menuName = "ScriptableObjects /AbilityStrategies/Effects/AgroEnemies")]
    public class AgroEnemies : EffectStrategy
    {
        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach(var target in data.Targets)
            {
                target.GetComponent<AIController>().Aggro(data.User.GetComponent<CombatTarget>());
            }
            finished?.Invoke();
        }
    }
}