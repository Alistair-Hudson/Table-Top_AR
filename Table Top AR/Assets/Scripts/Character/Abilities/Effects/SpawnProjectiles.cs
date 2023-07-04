using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Items.Equipment.Weapons;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Effects
{
    [CreateAssetMenu(fileName = "SpawnProjectiles", menuName = "ScriptableObjects /AbilityStrategies/Effects/SpawnProjectiles")]
    public class SpawnProjectiles : EffectStrategy
    {
        [SerializeField]
        private Projectile _projectileToSpawn = null;
        [SerializeField]
        private float _damage = 0;
        [SerializeField]
        private int _projectileNumber = 1;
        [SerializeField]
        private bool _isLeftHand = false;
        [SerializeField]
        private bool _isAutoTarget = false;

        public override void StartEffect(AbilityData data, Action finished)
        {
            if (_isAutoTarget)
            {
                SpawnTargetingProjectiles(data);
            }
            else
            {
                SpawnNonTargetingProjectiles(data);
            }
            finished?.Invoke();
        }

        private void SpawnNonTargetingProjectiles(AbilityData data)
        {
            for (int i = 0; i < _projectileNumber; i++)
            {
                Projectile projectile = Instantiate(_projectileToSpawn, data.User.GetComponent<Combat>().GetHandTransform(_isLeftHand));
                projectile.AimLocation = data.TargetedPoint;
                projectile.Instigator = data.User;
                projectile.Damage = _damage;
            }
        }

        private void SpawnTargetingProjectiles(AbilityData data)
        {
            foreach (var target in data.Targets)
            {
                Projectile projectile = Instantiate(_projectileToSpawn, data.User.GetComponent<Combat>().GetHandTransform(_isLeftHand));
                projectile.Target = target.GetComponent<Health>();
                projectile.Instigator = data.User;
                projectile.Damage = _damage;
            }
        }
    }
}