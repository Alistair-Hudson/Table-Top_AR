using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.Character
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRayCastable
    {
        private Health _health;
        public Health CharacterHealth { get => _health; private set => _health = value; }

        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public bool HandleRaycast(GenericInput callingInput)
        {
            if (!callingInput.Combat.CanAttack(this))
            {
                return false;
            }

            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            {
                callingInput.Combat.SetAttackTarget(this);
            }
            return true;
        }

        private void Awake()
        {
            _health = GetComponent<Health>();
        }
    }
}
