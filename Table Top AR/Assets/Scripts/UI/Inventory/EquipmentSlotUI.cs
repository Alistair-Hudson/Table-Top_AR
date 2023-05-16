using GameDevTV.Core.UI.Dragging;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using TableTopAR.Items.Inventory;
using UnityEngine;

namespace TableTopAR.UI.Inventory
{
    public class EquipmentSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        [SerializeField]
        private InventoryItemIcon _icon = null;
        [SerializeField]
        private EquipmentType _type = EquipmentType.None;

        private PlayerEquipment _equipment = null;

        private void Awake()
        {
            var player = FindObjectOfType<GenericInput>();
            _equipment = player.GetComponent<PlayerEquipment>();
            _equipment.EquipmentUpdated += RedrawUI;
        }

        private void Start()
        {
            RedrawUI();
        }

        public void AddItems(InventoryItem item, int number)
        {
            if (!(item is EquipableItem))
            {
                return;
            }
            if ((item as EquipableItem).Type != _type)
            {
                return;
            }

            _equipment.AddItem(_type, item as EquipableItem);
        }

        public InventoryItem GetItem()
        {
            return _equipment.GetItemInSlot(_type);
        }

        public int GetNumber()
        {
            if (_equipment == null)
            {
                return 0;
            }
            return 1;
        }

        public int MaxAcceptable(InventoryItem item)
        {
            if (!(item is EquipableItem))
            {
                return 0;
            }
            if ((item as EquipableItem).Type != _type)
            {
                return 0;
            }
            if (_equipment != null)
            {
                return 0;
            }
            return 1;
        }

        public void RemoveItems(int number)
        {
            _equipment.RemoveItem(_type);
        }

        private void RedrawUI()
        {
            _icon.SetItem(_equipment.GetItemInSlot(_type));
        }
    }
}