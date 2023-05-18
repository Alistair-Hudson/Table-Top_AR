using System;
using TableTopAR.Core;
using TableTopAR.Saving;
using UnityEngine;

namespace TableTopAR.Items.Inventory
{
    /// <summary>
    /// Provides storage for the player inventory. A configurable number of
    /// slots are available.
    ///
    /// This component should be placed on the GameObject tagged "Player".
    /// </summary>
    public class CharacterInventory : MonoBehaviour, ISaveable
    {
        public struct InventorySlot
        {
            public InventoryItem Item;
            public int Number;
        }

        // CONFIG DATA
        [Tooltip("Allowed size")]
        [SerializeField] int inventorySize = 16;

        // STATE
        InventorySlot[] slots;

        // PUBLIC
         
        /// <summary>
        /// Broadcasts when the items in the slots are added/removed.
        /// </summary>
        public event Action inventoryUpdated;

        /// <summary>
        /// Convenience for getting the player's inventory.
        /// </summary>
        public static CharacterInventory GetPlayerInventory()
        {
            var player = FindObjectOfType<GenericInput>();
            return player.GetComponent<CharacterInventory>();
        }

        /// <summary>
        /// Could this item fit anywhere in the inventory?
        /// </summary>
        public bool HasSpaceFor(InventoryItem item)
        {
            return FindSlot(item) >= 0;
        }

        /// <summary>
        /// How many slots are in the inventory?
        /// </summary>
        public int GetSize()
        {
            return slots.Length;
        }

        /// <summary>
        /// Attempt to add the items to the first available slot.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>Whether or not the item could be added.</returns>
        public bool AddToFirstEmptySlot(InventoryItem item, int number)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            slots[i].Item = item;
            slots[i].Number += number;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }

        /// <summary>
        /// Is there an instance of the item in the inventory?
        /// </summary>
        public bool HasItem(InventoryItem item)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (object.ReferenceEquals(slots[i], item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Return the item type in the given slot.
        /// </summary>
        public InventorySlot GetItemInSlot(int slot)
        {
            return slots[slot];
        }

        public int GetNumberInSlot(int index)
        {
            return slots[index].Number;
        }

        /// <summary>
        /// Remove the item from the given slot.
        /// </summary>
        public void RemoveFromSlot(int slot, int number)
        {
            slots[slot].Number -= number;
            if (slots[slot].Number <= 0)
            {
                slots[slot].Number = 0;
                slots[slot].Item = null;
            }
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }

        /// <summary>
        /// Will add an item to the given slot if possible. If there is already
        /// a stack of this type, it will add to the existing stack. Otherwise,
        /// it will be added to the first empty slot.
        /// </summary>
        /// <param name="slot">The slot to attempt to add to.</param>
        /// <param name="item">The item type to add.</param>
        /// <returns>True if the item was added anywhere in the inventory.</returns>
        public bool AddItemToSlot(int slot, InventoryItem item, int number)
        {
            if (slots[slot].Item != null)
            {
                return AddToFirstEmptySlot(item, number);
            }

            slots[slot].Item = item;
            slots[slot].Number += number;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }

        // PRIVATE

        private void Awake()
        {
            slots = new InventorySlot[inventorySize];
        }

        /// <summary>
        /// Find a slot that can accomodate the given item.
        /// </summary>
        /// <returns>-1 if no slot is found.</returns>
        private int FindSlot(InventoryItem item)
        {
            if (!item.IsStackable())
            {
                return FindEmptySlot();
            }
            for (int i = 0; i < slots.Length; i++)
            {
                if (object.ReferenceEquals(slots[i].Item, item))
                {
                    return i;
                }
            }
            return FindEmptySlot();
        }

        /// <summary>
        /// Find an empty slot.
        /// </summary>
        /// <returns>-1 if all slots are full.</returns>
        private int FindEmptySlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].Item == null)
                {
                    return i;
                }
            }
            return -1;
        }

        [System.Serializable]
        private struct InventorySlotRecord
        {
            public string ItemID;
            public int Number;
        }

        object ISaveable.CaptureState()
        {
            var slotRecords = new InventorySlotRecord[inventorySize];
            for (int i = 0; i < inventorySize; i++)
            {
                if (slots[i].Item != null)
                {
                    slotRecords[i].ItemID = slots[i].Item.GetItemID();
                    slotRecords[i].Number = slots[i].Number;
                }
            }
            return slotRecords;
        }

        void ISaveable.RestoreState(object state)
        {
            var slotRecords = (InventorySlotRecord[])state;
            for (int i = 0; i < inventorySize; i++)
            {
                slots[i].Item = InventoryItem.GetFromID(slotRecords[i].ItemID);
                slots[i].Number = slotRecords[i].Number;
            }
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }
    }
}