using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Character.Abilities
{
    public class AbilityData
    {
        private GameObject _user = null;
        private IEnumerable<GameObject> _targets = null;
        private FilterStrategy[] _filterStrategies;
        private EffectStrategy[] _effectStrategies;

        public GameObject User { get => _user; }
        public IEnumerable<GameObject> Targets { get => _targets; set => _targets = value; }
        public FilterStrategy[] FilterStrategies { get => _filterStrategies; }
        public EffectStrategy[] EffectStrategies { get => _effectStrategies; }

        public AbilityData(GameObject user, FilterStrategy[] filterStrategies, EffectStrategy[] effectStrategies)
        {
            _user = user;
            _filterStrategies = filterStrategies;
            _effectStrategies = effectStrategies;
        }
    }
}