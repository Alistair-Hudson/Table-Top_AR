using System;
using System.Collections;
using System.Collections.Generic;
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

        public void SetItem(Sprite item)
        {
            if (item == null)
            {
                _iconImage.enabled = false;
                return;
            }

            _iconImage.enabled = true;
            _iconImage.sprite = item;
        }

        public Sprite GetItem()
        {
            if (!_iconImage.enabled)
            {
                return null;
            }
            return _iconImage.sprite;
        }
    }
}