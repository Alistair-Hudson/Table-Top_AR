using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.Items.Inventory
{
    [CreateAssetMenu(menuName = ("ScriptableObjects/Items/Equipment/Equipable Item"))]
    public class EquipableItem : InventoryItem
    {
        [SerializeField]
        private EquipmentType _type = EquipmentType.None;
        public EquipmentType Type { get => _type; }
    }
}