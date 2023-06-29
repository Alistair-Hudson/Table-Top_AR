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
        private List<GameObject> _chainedTargets = new List<GameObject>();
        private int _chained = 0;
        private bool _isUserCaster = false;

        public GameObject User { get => _user; }
        public FilterStrategy[] FilterStrategies { get => _filterStrategies; }
        public EffectStrategy[] EffectStrategies { get => _effectStrategies; }
        public bool IsUserCaster { get => _isUserCaster; }

        public Vector3 TargetedPoint { get => _targetedPoint; set => _targetedPoint = value; }
        public IEnumerable<GameObject> Targets { get => _targets; set => _targets = value; }
        public List<GameObject> ChainedTargets { get => _chainedTargets; set => _chainedTargets = value; }
        public int Chained { get => _chained; set => _chained = value; }

        public AbilityData(GameObject user, FilterStrategy[] filterStrategies, EffectStrategy[] effectStrategies, bool isCaster = false)
        {
            _user = user;
            _filterStrategies = filterStrategies;
            _effectStrategies = effectStrategies;
            _isUserCaster = isCaster;
        }
    }
}