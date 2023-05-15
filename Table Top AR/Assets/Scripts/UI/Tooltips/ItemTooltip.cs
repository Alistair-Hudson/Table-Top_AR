using System.Collections;
using System.Collections.Generic;
using TableTopAR.Items.Inventory;
using TMPro;
using UnityEngine;

namespace TableTopAR.UI.Tooltips
{
    public class ItemTooltip : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _title = null;
        [SerializeField]
        private TMP_Text _body = null;

        public void Setup(InventoryItem item)
        {
            _title.text = item.GetDisplayName();
            _body.text = item.GetDescription();
        }
    }
}