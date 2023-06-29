using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TableTopAR.Core;
using TableTopAR.Items.Equipment.Weapons;
using TableTopAR.Saving;
using TableTopAR.Stats;
using TableTopAR.Items.Inventory;
using System;
using TableTopAR.Items.Equipment;
using UnityEngine.Events;

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
        private GenericWeaponConfig _defaultWeapon = null;

        private CombatTarget _target;
        private CharacterEquipment _equipment;
        private Movement _movement;
        private ActionScheduler _actionScheduler;
        private Animator _animator;
        private float _timeSinceLastAttack = Mathf.Infinity;
        private GenericWeaponConfig _currentWeaponConfig = null;
        private Weapon _currentWeapon = null;
        private BaseStats _baseStats;

        public UnityEvent OnAbilityCast = new UnityEvent();

        private void Awake()
        {
            _baseStats = GetComponent<BaseStats>();
            _movement = GetComponent<Movement>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
            _equipment = GetComponent<CharacterEquipment>();
            
        }

        private void Start()
        {
            _equipment.EquipmentUpdated += UpdateWeapon;
            if (_currentWeaponConfig == null)
            {
                _equipment.AddItem(EquipmentType.SingleHanded, _defaultWeapon);
            }
        }

        private void UpdateWeapon()
        {
            var weapon = _equipment.GetItemInSlot(EquipmentType.SingleHanded) as GenericWeaponConfig;
            EquipWeapon(weapon);

        }

        public void EquipWeapon(GenericWeaponConfig weapon)
        {
            if (weapon == null)
            {
                weapon = Resources.Load<GenericWeaponConfig>("Weapons/Unarmed");
            }
            _currentWeaponConfig = weapon;
            _currentWeapon = weapon.Spawn(_rhTransform, _lhTransform, _animator);
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;

            if (_target == null)
            {
                return;
            }
            if (Vector3.Distance(transform.position, _target.transform.position) > _currentWeaponConfig.CombatRannge)
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

            if (_timeSinceLastAttack >= _currentWeaponConfig.TimeBetweenAttacks)
            {
                transform.LookAt(_target.transform);
                _animator.ResetTrigger("stopAttack");
                _animator.SetTrigger("attack");
                _timeSinceLastAttack = 0;
            }
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget.CharacterHealth.IsDead)
            {
                return false;
            }
            if (!_movement.CanMoveTo(combatTarget.transform.position) && Vector3.Distance(transform.position, combatTarget.transform.position) < _currentWeaponConfig.CombatRannge)
            {
                return false;
            }
            return true;
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

        #region AnimationEvents
        private void Hit()
        {
            if (_target == null)
            {
                return;
            }
            float baseDamage = _baseStats.GetStat(Stats.Stats.BaseDamage);
            _target.CharacterHealth.TakeDamage(gameObject, baseDamage);
            _currentWeapon?.OnHit();
        }

        private void Shoot()
        {
            _currentWeaponConfig.FireProjectile(_rhTransform, _lhTransform, _target.GetComponent<Health>(), gameObject, _baseStats.GetStat(Stats.Stats.BaseDamage));
            _currentWeapon.OnShoot();
        }

        public void AbilityCast()
        {
            OnAbilityCast.Invoke();
        }

        #endregion
        public object CaptureState()
        {
            return _currentWeaponConfig.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            GenericWeaponConfig weapon = Resources.Load<GenericWeaponConfig>("Weapons/" + weaponName);
            if (weapon == null)
            {
                weapon = _defaultWeapon;
            }
            EquipWeapon(weapon);
        }

    }
}
