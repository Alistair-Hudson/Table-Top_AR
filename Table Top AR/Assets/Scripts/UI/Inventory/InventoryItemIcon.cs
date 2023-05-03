using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Items.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace TableTopAR.UI.Inventory
{
    public class InventoryItemIcon : MonoBehaviour
    {
        private Image _iconImage = null;

        private void Awake()
        {
            _iconImage = GetComponent<Image>(); 
        }

        public void SetItem(InventoryItem item)
        {
            if (item == null)
            {
                _iconImage.enabled = false;
                return;
            }

            _iconImage.enabled = true;
            _iconImage.sprite = item.GetIcon();
        }
    }
}