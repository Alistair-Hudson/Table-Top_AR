using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.UI.Inventory
{
    /// <summary>
    /// To be placed on the root of the inventory UI. Handles spawning all the
    /// inventory slot prefabs.
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        // CONFIG DATA
        [SerializeField] InventorySlotUI InventoryItemPrefab = null;

        // CACHE
        TableTopAR.Items.Inventory.CharacterInventory playerInventory;

        // LIFECYCLE METHODS

        private void Awake() 
        {
            playerInventory = TableTopAR.Items.Inventory.CharacterInventory.GetPlayerInventory();
            playerInventory.inventoryUpdated += Redraw;
        }

        private void Start()
        {
            Redraw();
        }

        // PRIVATE

        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < playerInventory.GetSize(); i++)
            {
                var itemUI = Instantiate(InventoryItemPrefab, transform);
                itemUI.Setup(playerInventory, i);
            }
        }
    }
}