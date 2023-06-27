using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Character.Abilities
{
    public class AbilityData
    {
        private GameObject _user = null;
        private IEnumerable<GameObject> _targets = null;

        public GameObject User { get => _user; }
        public IEnumerable<GameObject> Targets { get => _targets; set => _targets = value; }

        public AbilityData(GameObject user)
        {
            _user = user;
        }
    }
}