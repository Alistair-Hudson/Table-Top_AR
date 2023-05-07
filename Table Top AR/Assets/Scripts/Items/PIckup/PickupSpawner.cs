using System.Collections;
using System.Collections.Generic;
using TableTopAR.Items.Inventory;
using TableTopAR.Saving;
using UnityEngine;

namespace TableTopAR.Items.PickUp
{
    public class PickupSpawner : MonoBehaviour, ISaveable
    {
        //CONFIG
        [SerializeField]
        private InventoryItem _item = null;

        public bool IsCollected { get => GetPickup() == null; }

        private void Awake()
        {
            SpawnPickup();
        }

        public Pickup GetPickup()
        {
            return GetComponentInChildren<Pickup>();
        }

        private void SpawnPickup()
        {
            var spawnedPickup = _item.SpawnPickup(transform.position);
            spawnedPickup.transform.SetParent(transform);
        }

        private void DestroyPickup()
        {
            if (GetPickup())
            {
                Destroy(GetPickup().gameObject);
            }
        }

        public object CaptureState()
        {
            return IsCollected;
        }

        public void RestoreState(object state)
        {
            bool shouldBeCollected = (bool)state;

            if (shouldBeCollected && !IsCollected)
            {
                DestroyPickup();
            }

            if (!shouldBeCollected && IsCollected)
            {
                SpawnPickup();
            }
        }

    }
}