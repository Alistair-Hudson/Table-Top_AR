using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.Items.Equipment.Weapons
{
    [CreateAssetMenu(fileName = "GenericWeapon", menuName = "ScriptableObjects/Items/Equipment/GenericWeapon", order = 0)]
    public class GenericWeapon : ScriptableObject
    {
        [SerializeField]
        private GameObject _weaponPrefab = null;
        [SerializeField]
        private AnimatorOverrideController _weaponOverride = null;
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
        private bool isRighthanded = true;

        [SerializeField]
        private Projectile projectilePrefab = null;

        public bool HasProjectile { get => projectilePrefab == null; }

        public void Spawn(Transform rhTransform, Transform lhTransform, Animator animator) {
            if (_weaponPrefab != null)
            {
                Transform handTransform = isRighthanded ? rhTransform : lhTransform;
                Instantiate(_weaponPrefab, handTransform);
            }
            if (_weaponOverride != null)
            {
                animator.runtimeAnimatorController = _weaponOverride;
            }
        }

        public void FireProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Transform handTransform = isRighthanded ? rightHand : leftHand;
            Projectile projectileInstance = Instantiate(projectilePrefab, handTransform);
            projectileInstance.Target = target;
            projectileInstance.Damage = WeaponDamage;
        }
    }
}