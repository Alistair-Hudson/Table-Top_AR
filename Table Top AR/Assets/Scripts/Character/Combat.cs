using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TableTopAR.Core;

namespace TableTopAR.Character
{
    public class Combat : MonoBehaviour, IAction
    {
        [SerializeField]
        private float combatRange = 1f;

        private Transform target;
        private Movement movement;
        private ActionScheduler actionScheduler;

        private void Awake()
        {
            movement = GetComponent<Movement>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }
            if (Vector3.Distance(transform.position, target.position) > combatRange)
            {
                movement.SetDestination(target.position);
            }
            else
            {
                movement.Cancel();
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}
