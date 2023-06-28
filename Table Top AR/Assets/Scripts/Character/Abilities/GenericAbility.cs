using System.Collections;
using System.Collections.Generic;
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
        private TargetingStrategy _targetingStrategy = null;
        [SerializeField]
        private FilterStrategy[] _filterStrategies = null;
        [SerializeField]
        private EffectStrategy[] _effectStrategies = null;

        public string DisplayName { get => _displayName; }
        public string Description { get => _description; }
        public Sprite Icon { get => _icon; }
        public float ManaCost { get => _manaCost; }

        public void UseAbility(GameObject user)
        {
            Debug.Log($"Used {_displayName}");
            AbilityData data = new AbilityData(user, _filterStrategies, _effectStrategies);
            _targetingStrategy.StartTargeting(data, () =>
            {
                TargetAquired(data);
            });
        }

        private void TargetAquired(AbilityData data)
        {
            foreach (var filter in _filterStrategies)
            {
                data.Targets = filter.Filter(data.Targets);
            }
            
            foreach (var effect in _effectStrategies)
            {
                effect.StartEffect(data, EffectFinished);
            }
        }

        private void EffectFinished()
        {

        }
    }
}