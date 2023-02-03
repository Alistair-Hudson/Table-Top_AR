using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TableTopAR.Core;
using TableTopAR.Items.Equipment.Weapons;
using TableTopAR.Saving;

namespace TableTopAR.Character
{
    [RequireComponent(typeof(Movement), typeof(ActionScheduler), typeof(Animator))]
    public class Combat : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField]
        private Transform _rhTransform = null;
        [SerializeField]
        private Transform _lhTransform = null;
        [SerializeField]
        private GenericWeapon _defaultWeapon = null;

        private CombatTarget _target;
        private Movement _movement;
        private ActionScheduler _actionScheduler;
        private Animator _animator;
        private float _timeSinceLastAttack = Mathf.Infinity;
        private GenericWeapon _currentWeapon = null;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
            
            if (_currentWeapon == null)
            {
                EquipWeapon(_defaultWeapon);
            }
        }

        public void EquipWeapon(GenericWeapon weapon)
        {
            _currentWeapon = weapon;
            weapon.Spawn(_rhTransform, _lhTransform, _animator);
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;

            if (_target == null)
            {
                return;
            }
            if (Vector3.Distance(transform.position, _target.transform.position) > _currentWeapon.CombatRannge)
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

            if (_timeSinceLastAttack >= _currentWeapon.TimeBetweenAttacks)
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
            _target.CharacterHealth.TakeDamage(gameObject, _currentWeapon.WeaponDamage);
        }

        private void Shoot()
        {
            _currentWeapon.FireProjectile(_rhTransform, _lhTransform, _target.GetComponent<Health>(), gameObject);
        }

        public object CaptureState()
        {
            return _currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            GenericWeapon weapon = Resources.Load<GenericWeapon>("Weapons/" + weaponName);
            if (weapon == null)
            {
                weapon = _defaultWeapon;
            }
            EquipWeapon(weapon);
        }
    }
}
