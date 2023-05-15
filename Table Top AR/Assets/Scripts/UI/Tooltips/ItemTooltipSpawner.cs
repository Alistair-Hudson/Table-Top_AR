using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.UI.Tooltips
{
    [RequireComponent(typeof(IItemHolder))]
    public class ItemTooltipSpawner : TooltipSpawner
    {
        private IItemHolder _itemHolder = null;

        protected override void Awake()
        {
            base.Awake();
            _itemHolder = GetComponent<IItemHolder>();
        }

        public override bool CanCreateTooltip()
        {
            var item = _itemHolder.GetItem();
            if (!item)
            {
                return false;
            }
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            var itemTooltip = tooltip.GetComponent<ItemTooltip>();
            if (!itemTooltip)
            {
                return;
            }   

            var item = _itemHolder.GetItem();
            itemTooltip.Setup(item);
        }
    }
}