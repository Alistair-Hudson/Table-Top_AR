using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using TableTopAR.Items.Inventory;
using TableTopAR.Saving;
using TableTopAR.Stats;
using UnityEngine;

namespace TableTopAR.Items.Equipment
{
    public class CharacterEquipment : MonoBehaviour, ISaveable, IStatModifier
    {
        protected Dictionary<EquipmentType, EquipableItem> _equippedItems = new Dictionary<EquipmentType, EquipableItem>();

        public event Action EquipmentUpdated;

        public static CharacterEquipment GetPlayerEquipment()
        {
            var player = FindObjectOfType<GenericInput>();
            return player.GetComponent<CharacterEquipment>();
        }

        public EquipableItem GetItemInSlot(EquipmentType equipmentType)
        {
            if (!_equippedItems.ContainsKey(equipmentType))
            {
                return null;
            }
            return _equippedItems[equipmentType];
        }

        public void AddItem(EquipmentType equipmentType, EquipableItem equipableItem)
        {
            if (equipmentType != equipableItem.Type)
            {
                return;
            }
            if (!_equippedItems.ContainsKey(equipmentType))
            {
                _equippedItems.Add(equipmentType, equipableItem);
            }
            else
            {
                _equippedItems[equipmentType] = equipableItem;
            }
            EquipmentUpdated();
        }

        public void RemoveItem(EquipmentType equipmentType)
        {
            _equippedItems.Remove(equipmentType);
            EquipmentUpdated();
        }

        public object CaptureState()
        {
            var equipmentRecords = new Dictionary<EquipmentType, string>();
            foreach (var pair in _equippedItems)
            {
                equipmentRecords.Add(pair.Key, pair.Value.GetItemID());
            }
            return equipmentRecords;
        }

        public void RestoreState(object state)
        {
            _equippedItems = new Dictionary<EquipmentType, EquipableItem>();
            var equipedRecords = (Dictionary<EquipmentType, string>)state;
            foreach (var pair in equipedRecords)
            {
                _equippedItems.Add(pair.Key, InventoryItem.GetFromID(pair.Value) as EquipableItem);
            }
        }

        public IEnumerable<float> GetAdditiveModifier(Stats.Stats stat)
        {
            foreach (var slot in _equippedItems.Keys)
            {
                var item = GetItemInSlot(slot) as IStatModifier;
                if (item == null)
                {
                    continue;
                }

                foreach (float modifier in item.GetAdditiveModifier(stat))
                {
                    yield return modifier;
                }
            }
        }

        public IEnumerable<float> GetPercentageModifier(Stats.Stats stat)
        {
            foreach (var slot in _equippedItems.Keys)
            {
                var item = GetItemInSlot(slot) as IStatModifier;
                if (item == null)
                {
                    continue;
                }

                foreach (float modifier in item.GetPercentageModifier(stat))
                {
                    yield return modifier;
                }
            }
        }
    }
}