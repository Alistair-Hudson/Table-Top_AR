using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Items.Inventory
{
    [CreateAssetMenu(fileName = "ActionItem", menuName = "ScriptableObjects/Items/ActionItem", order = 0)]
    public class ActionItem : InventoryItem
    {
        [SerializeField]
        private bool _isConsumable = false;

        public bool IsConsumable { get => _isConsumable; }

        public virtual void Use(GameObject user)
        {

        }
    }
}