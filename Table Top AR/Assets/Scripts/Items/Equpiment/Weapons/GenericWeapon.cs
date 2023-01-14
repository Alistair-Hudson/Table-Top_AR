using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Items.Equipment.Weapons
{
    [CreateAssetMenu(fileName = "GenericWeapon", menuName = "ScriptableObjects/Items/Equipment/GenericWeapon", order = 0)]
    public class GenericWeapon : ScriptableObject
    {
        [SerializeField]
        private float _combatRange = 1f;
        public float CombatRannge { get => _combatRange; }
        [SerializeField]
        private float _timeBewteenAttacks = 1.5f;
        public float TimeBetweenAttacks { get => _timeBewteenAttacks; }
        [SerializeField]
        private float _weaponDamage = 5;
        public float WeaponDamage { get => _weaponDamage; }
        [SerializeField]
        private GameObject _weaponPrefab = null;
        [SerializeField]
        private AnimatorOverrideController _weaponOverride = null;

        public void Spawn(Transform handTransform, Animator animator) {
            if (_weaponPrefab != null)
            {
                Instantiate(_weaponPrefab, handTransform);
            }
            if (_weaponOverride != null)
            {
                animator.runtimeAnimatorController = _weaponOverride;
            }
        }
    }
}