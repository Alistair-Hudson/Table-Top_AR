using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character;
using TableTopAR.Core;
using TableTopAR.Items.Inventory;
using UnityEngine;

namespace TableTopAR.Items.Equipment.Weapons
{
    [CreateAssetMenu(fileName = "GenericWeapon", menuName = "ScriptableObjects/Items/Equipment/GenericWeapon", order = 0)]
    public class GenericWeaponConfig : EquipableItem
    {
        [SerializeField]
        private Weapon _weaponPrefab = null;
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
        private float weaponDamageBonus = 0;
        public float WeaponDamageBonus { get => weaponDamageBonus; }
        [SerializeField]
        private bool isRighthanded = true;
        [SerializeField]
        private bool fireProjectileFromRight = false;
        [SerializeField]
        private bool isDualWield = false;

        [SerializeField]
        private Projectile projectilePrefab = null;
        private object weapon;
        const string weaponName = "Weapon";

        public bool HasProjectile { get => projectilePrefab == null; }

        public Weapon Spawn(Transform rhTransform, Transform lhTransform, Animator animator) {

            DestroyCurrentWeapon(rhTransform, lhTransform);
            Weapon weapon = null;
            if (_weaponPrefab != null)
            {
                if (isDualWield)
                {
                    weapon = Instantiate(_weaponPrefab, lhTransform);
                    weapon.name = weaponName + "Left";
                    weapon.transform.localScale *= -1;
                    weapon = Instantiate(_weaponPrefab, rhTransform);
                    weapon.name = weaponName + "Right";
                }
                else
                {
                    Transform handTransform = isRighthanded ? rhTransform : lhTransform;
                    weapon = Instantiate(_weaponPrefab, handTransform);
                    weapon.name = weaponName;
                }
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
            return weapon;
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

        public void FireProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float characterBaseDamage)
        {
            Transform handTransform = fireProjectileFromRight ? rightHand : leftHand;
            Projectile projectileInstance = Instantiate(projectilePrefab, handTransform.position, Quaternion.identity);
            projectileInstance.Target = target;
            projectileInstance.Instigator = instigator;
            projectileInstance.Damage = characterBaseDamage;
        }
    }
}