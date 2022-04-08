using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Character
{
    [RequireComponent(typeof(Animator))]
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float _maxHealth = 100;

        private Animator _animator;
        private float _currentHealth;
        private bool isDead = false;

        public bool IsDead { get => isDead; private set => isDead = value; }

        private void Awake()
        {
            _currentHealth = _maxHealth;
            _animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                isDead = true;
                _animator.SetTrigger("death");
            }
        }
    }
}