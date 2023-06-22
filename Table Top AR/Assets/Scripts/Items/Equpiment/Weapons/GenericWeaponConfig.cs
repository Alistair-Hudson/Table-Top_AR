using Sirenix.OdinInspector;
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
        [HorizontalGroup("GameData", 75)]
        [PreviewField(75)]
        [SerializeField]
        private Weapon _weaponPrefab = null;
        
        [VerticalGroup("GameData/Stats")]
        [SerializeField]
        private float _combatRange = 1f;
        [VerticalGroup("GameData/Stats")]
        [SerializeField]
        private float _timeBewteenAttacks = 1.5f;
        [VerticalGroup("GameData/Stats")]
        [SerializeField]
        private float _weaponDamage = 5;
        [VerticalGroup("GameData/Stats")]
        [SerializeField]
        private float weaponDamageBonus = 0;
        [VerticalGroup("GameData/Stats")]
        [SerializeField]
        private bool isRighthanded = true;
        [VerticalGroup("GameData/Stats")]
        [SerializeField]
        private bool fireProjectileFromRight = false;
        [VerticalGroup("GameData/Stats")]
        [SerializeField]
        private bool isDualWield = false;

        [SerializeField]
        private AnimatorOverrideController _weaponOverride = null;
        
        [PreviewField(75)]
        [SerializeField]
        private Projectile projectilePrefab = null;
        private object weapon;
        const string weaponName = "Weapon";

        public float CombatRannge { get => _combatRange; }
        public float TimeBetweenAttacks { get => _timeBewteenAttacks; }
        public float WeaponDamage { get => _weaponDamage; }
        public float WeaponDamageBonus { get => weaponDamageBonus; }
        
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