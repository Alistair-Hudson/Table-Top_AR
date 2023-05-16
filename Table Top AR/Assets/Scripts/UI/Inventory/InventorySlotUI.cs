using GameDevTV.Core.UI.Dragging;
using TableTopAR.Core;
using TableTopAR.Items.Inventory;
using UnityEngine;

namespace TableTopAR.UI.Inventory
{
    public class InventorySlotUI : MonoBehaviour, IDragContainer<InventoryItem>, IItemHolder
    {
        [SerializeField]
        private InventoryItemIcon icon = null;

        private int _index = 0;
        private PlayerInventory _inventory = null;

        public void Setup(PlayerInventory inventory, int index)
        {
            _inventory = inventory;
            _index = index;
            icon.SetItem(inventory.GetItemInSlot(index).Item, _inventory.GetNumberInSlot(index));
        }

        public int MaxAcceptable(InventoryItem item)
        {
            if (_inventory.HasSpaceFor(item))
            {
                return int.MaxValue;
            }
            return 0;
        }
        public void AddItems(InventoryItem item, int number)
        {
            _inventory.AddItemToSlot(_index, item, number);
        }

        public InventoryItem GetItem()
        {
            return _inventory.GetItemInSlot(_index).Item;
        }

        public int GetNumber()
        {
            return _inventory.GetItemInSlot(_index).Number;
        }

        public void RemoveItems(int number)
        {
            _inventory.RemoveFromSlot(_index, number);
        }
    }
}