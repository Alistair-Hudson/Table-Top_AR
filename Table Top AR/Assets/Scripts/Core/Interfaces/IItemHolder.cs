using System.Collections;
using System.Collections.Generic;
using TableTopAR.Items.Inventory;
using UnityEngine;

namespace TableTopAR.Core
{
    public interface IItemHolder
    {
        public InventoryItem GetItem();
    }
}