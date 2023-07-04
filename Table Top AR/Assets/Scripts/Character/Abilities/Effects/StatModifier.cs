using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Stats;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Effects
{
    [CreateAssetMenu(fileName = "StatModifier", menuName = "ScriptableObjects /AbilityStrategies/Effects/StatModifier")]
    public class StatModifier : EffectStrategy
    {
        public class StatBuffer : MonoBehaviour, IStatModifier
        {
            public float RunTime = float.PositiveInfinity;
            public Dictionary<Stats.Stats, float> AdditiveStatValues = new Dictionary<Stats.Stats, float>();
            public Dictionary<Stats.Stats, float> PercentageStatValues = new Dictionary<Stats.Stats, float>();
            public BuffEffectOverTime BuffEffectOverTime = BuffEffectOverTime.None;

            private IEnumerator Start()
            {
                while (RunTime > 0)
                {
                    yield return null;
                    RunTime -= Time.deltaTime;
                }
                Destroy(this);
            }

            public IEnumerable<float> GetAdditiveModifier(Stats.Stats stat)
            {
                switch (BuffEffectOverTime)
                {
                    case BuffEffectOverTime.None:
                        yield return AdditiveStatValues[stat];
                        break;
                    case BuffEffectOverTime.Decrease:
                        yield return AdditiveStatValues[stat];
                        break;
                    case BuffEffectOverTime.Increase:
                        yield return AdditiveStatValues[stat];
                        break;
                }
            }

            public IEnumerable<float> GetPercentageModifier(Stats.Stats stat)
            {
                switch (BuffEffectOverTime)
                {
                    case BuffEffectOverTime.None:
                        yield return PercentageStatValues[stat];
                        break;
                    case BuffEffectOverTime.Decrease:
                        yield return PercentageStatValues[stat];
                        break;
                    case BuffEffectOverTime.Increase:
                        yield return PercentageStatValues[stat];
                        break;
                }
            }
        }

        public enum BuffEffectOverTime
        {
            None = 0,
            Decrease,
            Increase
        }

        [System.Serializable]
        public struct Modifier
        {
            public TableTopAR.Stats.Stats Stat;
            public float Value;
        }

        [SerializeField]
        private float _runTime = 0;
        [SerializeField]
        private BuffEffectOverTime _buffEffectOverTime = BuffEffectOverTime.None;
        [SerializeField]
        private Modifier[] _additiveStatModifiers;
        [SerializeField]
        private Modifier[] _pecentageStatModifiers;

        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach(var target in data.Targets)
            {
                var buffer = target.AddComponent<StatBuffer>();
                foreach (var modifier in _additiveStatModifiers)
                {
                    buffer.AdditiveStatValues.Add(modifier.Stat, modifier.Value);
                }
                foreach(var modifier in _pecentageStatModifiers)
                {
                    buffer.PercentageStatValues.Add(modifier.Stat, modifier.Value);
                }
                buffer.BuffEffectOverTime = _buffEffectOverTime;
                buffer.RunTime = _runTime;
            }
            finished?.Invoke();
        }
    }
}