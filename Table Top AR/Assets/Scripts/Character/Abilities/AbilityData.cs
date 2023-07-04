using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.Character.Abilities
{
    public class AbilityData : IAction
    {
        private GameObject _user = null;
        private Vector3 _targetedPoint = Vector3.zero;
        private IEnumerable<GameObject> _targets = null;
        private List<GameObject> _chainedTargets = new List<GameObject>();
        private int _chained = 0;
        private bool _isUserCaster = false;

        private bool _isCanceled = false;

        public GameObject User { get => _user; }
        public bool IsUserCaster { get => _isUserCaster; }
        public bool IsCanceled { get => _isCanceled; }

        public Vector3 TargetedPoint { get => _targetedPoint; set => _targetedPoint = value; }
        public IEnumerable<GameObject> Targets { get => _targets; set => _targets = value; }
        public List<GameObject> ChainedTargets { get => _chainedTargets; set => _chainedTargets = value; }
        public int Chained { get => _chained; set => _chained = value; }

        public AbilityData(GameObject user, bool isCaster = false)
        {
            _user = user;
            _isUserCaster = isCaster;
            _targetedPoint = user.transform.position;
        }

        public void Cancel()
        {
            _isCanceled = true;
        }
    }
}