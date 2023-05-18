using System.Collections;
using System.Collections.Generic;
using TableTopAR.Items.Inventory;
using TableTopAR.Saving;
using TableTopAR.Stats;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
            public int SceneBuildIndex;
        }

        [SerializeField]
        private float _scatterDist = 1;
        [SerializeField]
        private DropLibrary _dropLibray;

        private const int ATTEMPTS = 30;

        //STATE
        private List<Pickup> _droppedItems = new List<Pickup>();
        private List<DropRecord> _dropRecords = new List<DropRecord>();

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
            var randomPoint = transform.position + Random.insideUnitSphere * _scatterDist;
            for (int i = 0; i < ATTEMPTS; i++)
            {
                if (NavMesh.SamplePosition(randomPoint, out var hit, 0.1f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
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
            var droppedItemsList = new List<DropRecord>();
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            foreach (var dropped in _droppedItems)
            {
                var droppedItem = new DropRecord
                {
                    ItemID = dropped.Item.GetItemID(),
                    Position = new SerializableVector3(dropped.transform.position),
                    Number = dropped.Number,
                    SceneBuildIndex = sceneIndex
                };
                droppedItemsList.Add(droppedItem);
            }
            droppedItemsList.AddRange(_dropRecords);
            return droppedItemsList;
        }

        public void RestoreState(object state)
        {
            var droppedItemsList = (List<DropRecord>)state;
            int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            _dropRecords.Clear();
            foreach (var item in droppedItemsList)
            {
                if (item.SceneBuildIndex != sceneBuildIndex)
                {
                    _dropRecords.Add(item);
                    continue;
                }
                var pickupItem = InventoryItem.GetFromID(item.ItemID);
                Vector3 position = item.Position.ToVector();
                int number = item.Number;
                SpawnPickup(pickupItem, position, number);
            }
        }

        public void EnemyDrops()
        {
            var level = GetComponent<BaseStats>().CurrentLevel;

            var items = _dropLibray.GetRandomDrops(level);
            foreach (var item in items)
            {
                DropItem(item.Item, item.Number);
            }
        }
    }
}