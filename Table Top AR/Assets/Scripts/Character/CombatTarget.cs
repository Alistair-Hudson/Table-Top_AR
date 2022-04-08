using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Character
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        private Health _health;
        public Health CharacterHealth { get => _health; private set => _health = value; }

        private void Awake()
        {
            _health = GetComponent<Health>();
        }
    }
}
