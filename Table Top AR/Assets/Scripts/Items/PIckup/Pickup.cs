using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using TableTopAR.Items.Inventory;
using UnityEngine;

namespace TableTopAR.Items.Pickups
{
    public class Pickup : MonoBehaviour
    {
        //STATE
        private InventoryItem _item;
        public InventoryItem Item { get => _item; }

        //CACHED
        private PlayerInventory _inventory;

        public bool CanBePickedUp { get =>  _inventory.HasSpaceFor(_item); }

        private void Awake()
        {
            var player = FindObjectOfType<GenericInput>();
            _inventory = player.GetComponent<PlayerInventory>();
        }

        public void Setup(InventoryItem item)
        {
            _item = item;
        }

        public void PickupItem()
        {
            if (_inventory.AddToFirstEmptySlot(_item))
            {
                Destroy(gameObject);
            }
        }

    }
}