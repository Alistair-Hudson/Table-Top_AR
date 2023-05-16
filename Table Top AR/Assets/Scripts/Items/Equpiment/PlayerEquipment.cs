using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using TableTopAR.Saving;
using UnityEngine;

namespace TableTopAR.Items.Inventory
{
    public class PlayerEquipment : MonoBehaviour, ISaveable
    {
        private Dictionary<EquipmentType, EquipableItem> _equippedItems = new Dictionary<EquipmentType, EquipableItem>();

        public event Action EquipmentUpdated;

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
            EquipmentUpdated?.Invoke();
        }

        public void RemoveItem(EquipmentType equipmentType)
        {
            _equippedItems.Remove(equipmentType);
            EquipmentUpdated?.Invoke();
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
    }
}