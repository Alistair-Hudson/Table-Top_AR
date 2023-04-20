using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TableTopAR.Core;
using TableTopAR.Items.Equipment.Weapons;
using TableTopAR.Saving;
using TableTopAR.Stats;

namespace TableTopAR.Character
{
    [RequireComponent(typeof(Movement), typeof(ActionScheduler), typeof(Animator))]
    public class Combat : MonoBehaviour, IAction, ISaveable, IStatModifier
    {
        [SerializeField]
        private Transform _rhTransform = null;
        [SerializeField]
        private Transform _lhTransform = null;
        [SerializeField]
        private GenericWeaponConfig _defaultWeapon = null;

        private CombatTarget _target;
        private Movement _movement;
        private ActionScheduler _actionScheduler;
        private Animator _animator;
        private float _timeSinceLastAttack = Mathf.Infinity;
        private GenericWeaponConfig _currentWeaponConfig = null;
        private Weapon _currentWeapon = null;
        private BaseStats baseStats;

        private void Start()
        {
            baseStats = GetComponent<BaseStats>();
            _movement = GetComponent<Movement>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
            
            if (_currentWeaponConfig == null)
            {
                EquipWeapon(_defaultWeapon);
            }
        }

        public void EquipWeapon(GenericWeaponConfig weapon)
        {
            if (weapon == null)
            {
                return;
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
            float baseDamage = baseStats.GetStat(Stats.Stats.BaseDamage);
            _target.CharacterHealth.TakeDamage(gameObject, baseDamage);
            _currentWeapon?.OnHit();
        }

        private void Shoot()
        {
            _currentWeaponConfig.FireProjectile(_rhTransform, _lhTransform, _target.GetComponent<Health>(), gameObject, baseStats.GetStat(Stats.Stats.BaseDamage));
            _currentWeapon.OnShoot();
        }

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

        public IEnumerable<float> GetAdditiveModifier(Stats.Stats stat)
        {
            switch (stat)
            {
                case Stats.Stats.BaseDamage:
                    yield return _currentWeaponConfig.WeaponDamage;
                    break;
                default:
                    yield return 0;
                    break;
            }
        }

        public IEnumerable<float> GetPercentageModifier(Stats.Stats stat)
        {
            switch (stat)
            {
                case Stats.Stats.BaseDamage:
                    yield return _currentWeaponConfig.WeaponDamageBonus;
                    break;
                default:
                    yield return 0;
                    break;
            }
        }
    }
}
