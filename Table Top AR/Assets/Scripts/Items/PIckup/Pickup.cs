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
        [SerializeField]
        private int _number;
        public int Number { get => _number; }

        //CACHED
        private PlayerInventory _inventory;

        public bool CanBePickedUp { get =>  _inventory.HasSpaceFor(_item); }

        private void Awake()
        {
            var player = FindObjectOfType<GenericInput>();
            _inventory = player.GetComponent<PlayerInventory>();
        }

        public void Setup(InventoryItem item, int number)
        {
            _item = item;
            _number = number;
        }

        public void PickupItem()
        {
            if (_inventory.AddToFirstEmptySlot(_item, _number))
            {
                Destroy(gameObject);
            }
        }

    }
}