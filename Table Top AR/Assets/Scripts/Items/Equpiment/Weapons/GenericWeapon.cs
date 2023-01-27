using System;
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
        private object weapon;
        const string weaponName = "Weapon";

        public bool HasProjectile { get => projectilePrefab == null; }

        public void Spawn(Transform rhTransform, Transform lhTransform, Animator animator) {

            DestroyCurrentWeapon(rhTransform, lhTransform);

            if (_weaponPrefab != null)
            {
                Transform handTransform = isRighthanded ? rhTransform : lhTransform;
                var weapon = Instantiate(_weaponPrefab, handTransform);
                weapon.name = weaponName;
            }
            if (_weaponOverride != null)
            {
                animator.runtimeAnimatorController = _weaponOverride;
            }
            else
            {
                var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
                if (overrideController != null)
                {
                    animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
                }
            }
        }

        private void DestroyCurrentWeapon(Transform rhTransform, Transform lhTransform)
        {
            Transform oldWeapon = rhTransform.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = lhTransform.Find(weaponName);
            }
            if (oldWeapon != null)
            {
                oldWeapon.name = "DESTROYING";
                Destroy(oldWeapon.gameObject);
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