using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TableTopAR.Core;

namespace TableTopAR.Character
{
    [RequireComponent(typeof(Movement), typeof(ActionScheduler), typeof(Animator))]
    public class Combat : MonoBehaviour, IAction
    {
        [SerializeField]
        private float _combatRange = 1f;
        [SerializeField]
        private float _timeBewteenAttacks = 1.5f;
        [SerializeField]
        private float _weaponDamage = 5;

        private CombatTarget _target;
        private Movement _movement;
        private ActionScheduler _actionScheduler;
        private Animator _animator;
        private float _timeSinceLastAttack = 0;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;

            if (_target == null)
            {
                return;
            }
            if (Vector3.Distance(transform.position, _target.transform.position) > _combatRange)
            {
                _movement.SetDestination(_target.transform.position);
            }
            else
            {
                _movement.Cancel();
                ProcessAttack();
            }
        }

        private void ProcessAttack()
        {
            if (_target.CharacterHealth.IsDead)
            {
                return;
            }

            if (_timeSinceLastAttack >= _timeBewteenAttacks)
            {
                _animator.SetTrigger("attack");
                _timeSinceLastAttack = 0;
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _target = combatTarget;
        }

        public void Cancel()
        {
            _animator.SetTrigger("stopAttack");
            _target = null;
        }

        //Animation Event
        private void Hit()
        {
            _target.CharacterHealth.TakeDamage(_weaponDamage);
        }
    }
}
