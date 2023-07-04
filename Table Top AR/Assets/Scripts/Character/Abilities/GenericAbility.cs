using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.Character.Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability")]
    public class GenericAbility : ScriptableObject
    {
        [Tooltip("Item name to be displayed in UI.")]
        [SerializeField] 
        private string _displayName = null;
        [Tooltip("Item description to be displayed in UI.")]
        [SerializeField] [TextArea] 
        private string _description = null;
        [Tooltip("The UI icon to represent this item in the inventory.")]
        [SerializeField]
        private Sprite _icon = null;
        [SerializeField]
        private float _manaCost = 0;
        [SerializeField]
        private float _coolDownTime = 0;
        [SerializeField]
        private bool _isPassive = false;

        [SerializeField]
        private TargetingStrategy _targetingStrategy = null;
        [SerializeField]
        private FilterStrategy[] _filterStrategies = null;
        [SerializeField]
        private EffectStrategy[] _effectStrategies = null;

        public string DisplayName { get => _displayName; }
        public string Description { get => _description; }
        public Sprite Icon { get => _icon; }
        public float ManaCost { get => _manaCost; }
        public float CoolDownTime { get => _coolDownTime; }
        public bool IsPassive { get => _isPassive; }

        public void UseAbility(GameObject user)
        {
            if (!CheckCooldown(user))
            {
                return;
            }
            if (!CheckMana(user))
            {
                return;
            }
            Debug.Log($"Used {_displayName}");
            AbilityData data = new AbilityData(user, true);
            var ac = user.GetComponent<ActionScheduler>();
            ac.StartAction(data);
            _targetingStrategy.StartTargeting(data, () =>
            {
                TargetAquired(data);
            });
        }

        private void TargetAquired(AbilityData data)
        {
            if (data.IsCanceled)
            {
                return;
            }

            foreach (var filter in _filterStrategies)
            {
                data.Targets = filter.Filter(data.Targets);
            }

            data.User.GetComponent<CoolDownStore>().StartCoolDown(this, _coolDownTime);
            data.User.GetComponent<Mana>().ConsumeMana(_manaCost);
            foreach (var effect in _effectStrategies)
            {
                effect.StartEffect(data, EffectFinished);
            }
        }

        private void EffectFinished()
        {

        }

        private bool CheckCooldown(GameObject user)
        {
            var cooldowns = user.GetComponent<CoolDownStore>();
            if (cooldowns.CooldownTimeRemaining(this) > 0)
            {
                Debug.Log("Cooldown in progress");
                return false;
            }
            return true;
        }

        private bool CheckMana(GameObject user)
        {
            var manaPool = user.GetComponent<Mana>();
            if (manaPool.CurrentMana < _manaCost)
            {
                Debug.Log("not enough mana");
                return false;
            }
            return true;
        }
    }
}