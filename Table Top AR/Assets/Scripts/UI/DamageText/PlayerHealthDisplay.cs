using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character;
using TableTopAR.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TableTopAR.UI.PlayerUI
{
    public class PlayerHealthDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image _healthDisplay = null; 

        private Health _health = null;

        void Start()
        {
            _health = GetComponentInParent<Health>();
            _health.OnTakeDamage.AddListener(UpdateHealthDisplay);
            _healthDisplay.fillAmount = _health.CurrentHealth / _health.MaxHealth;
        }

        private void UpdateHealthDisplay(float arg0, DamageType arg1)
        {
            _healthDisplay.fillAmount = _health.CurrentHealth / _health.MaxHealth;
        }
    }
}