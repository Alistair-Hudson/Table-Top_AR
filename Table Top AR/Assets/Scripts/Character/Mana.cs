using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Saving;
using TableTopAR.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace TableTopAR.Character
{
    public class Mana : MonoBehaviour, ISaveable
    {
        private float _maxMana = -1;
        private float _currentMana = -1;
        private float _manaRegenPerSec = 1;
        private BaseStats baseStats;

        public float CurrentMana { get => _currentMana; }
        public float MaxMana { get => _maxMana; }
        public float ManaPercentage { get => 100 * _currentMana / _maxMana; }

        public UnityEvent OnManaChange = new UnityEvent();

        private void Start()
        {
            baseStats = GetComponent<BaseStats>();
            baseStats.OnStatsUpdate += UpdateMana;
            _maxMana = baseStats.GetStat(Stats.Stats.Mana);
            _manaRegenPerSec = baseStats.GetStat(Stats.Stats.ManaRegen);
            if (_currentMana < 0)
            {
                _currentMana = _maxMana;
            }
            OnManaChange.Invoke();
            StartCoroutine(ManaRegen());
        }

        private IEnumerator ManaRegen()
        {
            while (true)
            {
                RestoreMana(_manaRegenPerSec * Time.deltaTime);
                yield return null;
            }
        }

        private void UpdateMana()
        {
            var oldMaxMana = _maxMana;
            _maxMana = baseStats.GetStat(Stats.Stats.Mana);
            _currentMana += _maxMana - oldMaxMana;
            _manaRegenPerSec = baseStats.GetStat(Stats.Stats.ManaRegen);
        }

        /// <summary>
        /// Returns true if there is enough mana, returns fals eif there is not enough
        /// </summary>
        /// <param name="mana"></param>
        /// <returns></returns>
        public void ConsumeMana(float mana)
        {
            _currentMana -= mana;
            OnManaChange.Invoke();
        }

        public void RestoreMana(float restore)
        {
            _currentMana = Mathf.Min(_maxMana, _currentMana + restore);
            OnManaChange.Invoke();
        }

        public object CaptureState()
        {
            return _currentMana;
        }

        public void RestoreState(object state)
        {
            _currentMana = (float)state;
        }
    }
}