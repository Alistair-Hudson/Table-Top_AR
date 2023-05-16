using GameDevTV.Core.UI.Dragging;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using TableTopAR.Items.Pickups;
using UnityEngine;

namespace TableTopAR.Items.Inventory
{
    public class InventoryDrop : MonoBehaviour, IDragDestination<InventoryItem>
    {
        public int MaxAcceptable(InventoryItem item)
        {
            return int.MaxValue;
        }

        public void AddItems(InventoryItem item, int number)
        {
            var player = FindAnyObjectByType<GenericInput>();
            player.GetComponent<ItemDropper>().DropItem(item, number);
        }
    }
}