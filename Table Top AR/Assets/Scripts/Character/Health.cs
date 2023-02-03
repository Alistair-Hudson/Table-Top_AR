using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;
using TableTopAR.Saving;
using TableTopAR.Stats;

namespace TableTopAR.Character
{
    [RequireComponent(typeof(Animator))]
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField]
        private float _maxHealth = 100;

        private Animator _animator;
        private float _currentHealth;
        private bool isDead = false;

        public float CurrentHealth { get => _currentHealth; }
        public float HealthPercentage { get => 100 * _currentHealth / _maxHealth; }
        public bool IsDead { get => isDead; private set => isDead = value; }

        private void Awake()
        {
            _maxHealth = GetComponent<BaseStats>().GetHealth();
            _currentHealth = _maxHealth;
            _animator = GetComponent<Animator>();
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                isDead = true;
                GetComponent<ActionScheduler>().CancelCurrentAction();
                _animator.SetTrigger("death");
                if (instigator.TryGetComponent<Experience>(out var experience))
                {
                    experience.GainExperience(GetComponent<BaseStats>().GetXPReward());
                }
            }
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
                isDead = true;
                GetComponent<ActionScheduler>().CancelCurrentAction();
                _animator.SetTrigger("death");
            }
        }
    }
}