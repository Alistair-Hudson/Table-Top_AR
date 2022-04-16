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
        private float _timeSinceLastAttack = Mathf.Infinity;

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
                transform.LookAt(_target.transform);
                _animator.ResetTrigger("stopAttack");
                _animator.SetTrigger("attack");
                _timeSinceLastAttack = 0;
            }
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            return !combatTarget.CharacterHealth.IsDead;
        }

        public void SetAttackTarget(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _target = combatTarget;
        }

        public void Cancel()
        {
            _animator.ResetTrigger("attack");
            _animator.SetTrigger("stopAttack");
            _target = null;
        }

        //Animation Event
        private void Hit()
        {
            if (_target == null)
            {
                return;
            }
            _target.CharacterHealth.TakeDamage(_weaponDamage);
        }
    }
}
