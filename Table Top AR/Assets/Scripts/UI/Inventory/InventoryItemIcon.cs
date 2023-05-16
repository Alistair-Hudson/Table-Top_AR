using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Items.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TableTopAR.UI.Inventory
{
    public class InventoryItemIcon : MonoBehaviour
    {
        [SerializeField]
        private GameObject _stackContainer = null;
        [SerializeField]
        private TMP_Text _stackNumber = null;

        private Image _iconImage = null;

        private void Awake()
        {
            _iconImage = GetComponent<Image>(); 
        }

        public void SetItem(InventoryItem item, int number)
        {
            if (item == null)
            {
                _iconImage.enabled = false;
                _stackContainer.SetActive(false);
                return;
            }

            _iconImage.enabled = true;
            _iconImage.sprite = item.GetIcon();

            if (number <= 1)
            {
                _stackContainer.SetActive(false);
            }
            else
            {
                _stackContainer.SetActive(true);
                if (number > 99)
                {
                    _stackNumber.text = "99+";
                }
                else
                {
                    _stackNumber.text = number.ToString();
                }
            }
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