using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Character.Abilities
{
    public class AbilityData
    {
        private GameObject _user = null;
        private Vector3 _targetedPoint = Vector3.zero;
        private IEnumerable<GameObject> _targets = null;
        private FilterStrategy[] _filterStrategies;
        private EffectStrategy[] _effectStrategies;

        public GameObject User { get => _user; }
        public FilterStrategy[] FilterStrategies { get => _filterStrategies; }
        public EffectStrategy[] EffectStrategies { get => _effectStrategies; }

        public Vector3 TargetedPoint { get => _targetedPoint; set => _targetedPoint = value; }
        public IEnumerable<GameObject> Targets { get => _targets; set => _targets = value; }

        public AbilityData(GameObject user, FilterStrategy[] filterStrategies, EffectStrategy[] effectStrategies)
        {
            _user = user;
            _filterStrategies = filterStrategies;
            _effectStrategies = effectStrategies;
        }
    }
}