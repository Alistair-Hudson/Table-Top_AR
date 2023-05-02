using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;
using TableTopAR.Saving;
using TableTopAR.Stats;
using System;
using UnityEngine.Events;

namespace TableTopAR.Character
{
    [RequireComponent(typeof(Animator))]
    public class Health : MonoBehaviour, ISaveable
    {
        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float, DamageType>
        {
        }

        public TakeDamageEvent OnTakeDamage;

        private float _maxHealth = -1;
        private Animator _animator;
        private float _currentHealth = -1;
        private bool isDead = false;
        private BaseStats baseStats;

        public float CurrentHealth { get => _currentHealth; }
        public float MaxHealth { get => _maxHealth; }
        public float HealthPercentage { get => 100 * _currentHealth / _maxHealth; }
        public bool IsDead { get => isDead; private set => isDead = value; }

        private void Start()
        {
            baseStats = GetComponent<BaseStats>();
            baseStats.onLevelChange += UpdateHealth;
            _maxHealth = baseStats.GetStat(Stats.Stats.Health);
            if (_currentHealth < 0)
            {
                _currentHealth = _maxHealth;
            }
            _animator = GetComponent<Animator>();
            OnTakeDamage.Invoke(0, DamageType.Invalid);
        }

        private void UpdateHealth()
        {
            _maxHealth = baseStats.GetStat(Stats.Stats.Health);
            _currentHealth = _maxHealth;
            OnTakeDamage.Invoke(0, DamageType.Invalid);
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            _currentHealth -= damage;
            OnTakeDamage.Invoke(damage, DamageType.Physical);
            if (_currentHealth <= 0)
            {
                isDead = true;
                GetComponent<ActionScheduler>().CancelCurrentAction();
                _animator.SetTrigger("death");
                if (instigator.TryGetComponent<Experience>(out var experience))
                {
                    experience.GainExperience(GetComponent<BaseStats>().GetStat(Stats.Stats.XPReward));
                }
            }
        }

        public void RestoreHealth(float restore)
        {
            OnTakeDamage.Invoke(restore, DamageType.Healing);
            _currentHealth = Mathf.Min(_maxHealth, _currentHealth + restore);
        }

        public object CaptureState()
        {
            return _currentHealth;
        }

        public void RestoreState(object state)
        {
            _currentHealth = (float)state;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                isDead = true;
                GetComponent<ActionScheduler>().CancelCurrentAction();
                _animator.SetTrigger("death");
            }
        }
    }
}