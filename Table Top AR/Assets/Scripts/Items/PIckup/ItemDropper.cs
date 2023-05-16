using System.Collections;
using System.Collections.Generic;
using TableTopAR.Items.Inventory;
using TableTopAR.Saving;
using UnityEngine;

namespace TableTopAR.Items.Pickups
{
    public class ItemDropper : MonoBehaviour, ISaveable
    {
        [System.Serializable]
        private struct DropRecord
        {
            public string ItemID;
            public SerializableVector3 Position;
            public int Number;
        }

        //STATE
        private List<Pickup> _droppedItems = new List<Pickup>();

        public void DropItem(InventoryItem item, int number)
        {
            SpawnPickup(item, GetDropLocation(), number);
        }

        private void RemoveDestroyedDrops()
        {
            var newList = new List<Pickup>();
            foreach (var item in _droppedItems)
            {
                if (item != null)
                {
                    newList.Add(item);
                }
            }
            _droppedItems = newList;
        }

        protected virtual Vector3 GetDropLocation()
        {
            return transform.position;
        }

        public void SpawnPickup(InventoryItem item, Vector3 spawnLocation, int number)
        {
            var pickup = item.SpawnPickup(spawnLocation, number);
            _droppedItems.Add(pickup);
        }

        public object CaptureState()
        {
            RemoveDestroyedDrops();
            var droppedItemsList = new DropRecord[_droppedItems.Count];
            for (int i = 0; i < droppedItemsList.Length; i++)
            {
                droppedItemsList[i].ItemID = _droppedItems[i].Item.GetItemID();
                droppedItemsList[i].Position = new SerializableVector3(_droppedItems[i].transform.position);
                droppedItemsList[i].Number = _droppedItems[i].Number;
            }
            return droppedItemsList;
        }

        public void RestoreState(object state)
        {
            var droppedItemsList = (DropRecord[])state;
            foreach (var item in droppedItemsList)
            {
                var pickupItem = InventoryItem.GetFromID(item.ItemID);
                Vector3 position = item.Position.ToVector();
                int number = item.Number;
                SpawnPickup(pickupItem, position, number);
            }
        }
    }
}