using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using TableTopAR.Items.Inventory;
using TableTopAR.Stats;
using UnityEngine;

namespace TableTopAR.Items.Equipment
{
    [CreateAssetMenu(menuName = ("ScriptableObjects/Items/Equipment/Equipable Item"))]
    public class EquipableItem : InventoryItem, IStatModifier
    {
        [System.Serializable]
        public struct StatModifier
        {
            public TableTopAR.Stats.Stats Stat;
            public float Value;
        }

        [SerializeField]
        private EquipmentType _type = EquipmentType.None;
        public EquipmentType Type { get => _type; }
        [SerializeField]
        private StatModifier[] _additiveStatModifiers;
        [SerializeField]
        private StatModifier[] _pecentageStatModifiers;

        public IEnumerable<float> GetAdditiveModifier(Stats.Stats stat)
        {
            foreach (var modifier in _additiveStatModifiers)
            {
                if (modifier.Stat == stat)
                {
                    yield return modifier.Value;
                }
            }
        }

        public IEnumerable<float> GetPercentageModifier(Stats.Stats stat)
        {
            foreach (var modifier in _pecentageStatModifiers)
            {
                if (modifier.Stat == stat)
                {
                    yield return modifier.Value;
                }
            }
        }
    }
}