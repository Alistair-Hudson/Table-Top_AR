using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TableTopAR.UI.Tooltips
{
    public class TooltipSpawner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private GameObject _tooltipPrefab = null;

        private GameObject _tooltip = null;

        protected virtual void Awake()
        {

        }

        protected virtual void OnDestroy()
        {
            ClearTooltip();
        }

        protected  virtual void OnDisable()
        {
            ClearTooltip();
        }

        private void ClearTooltip()
        {
            if (_tooltip)
            {
                Destroy(_tooltip.gameObject);
            }
        }

        public virtual bool CanCreateTooltip()
        {
            return true;
        }

        public virtual void UpdateTooltip(GameObject tooltip)
        {

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            var parentCanvas = GetComponentInParent<Canvas>();

            if (_tooltip && !CanCreateTooltip())
            {
                ClearTooltip();
            }

            if (!_tooltip && CanCreateTooltip())
            {
                _tooltip = Instantiate(_tooltipPrefab, parentCanvas.transform);
            }

            if (_tooltip)
            {
                UpdateTooltip(_tooltip);
                PositionToolTip();
            }
        }

        private void PositionToolTip()
        {
            Canvas.ForceUpdateCanvases();

            var tooltipCorners = new Vector3[4];
            _tooltip.GetComponent<RectTransform>().GetWorldCorners(tooltipCorners);
            var slotCorners = new Vector3[4];
            GetComponent<RectTransform>().GetWorldCorners(slotCorners);

            bool below = transform.position.y > Screen.height / 2;
            bool right = transform.position.x > Screen.width / 2;

            int slotCorner = GetCornerIndex(below, right);
            int tooltipCorner = GetCornerIndex(below, right);

            _tooltip.transform.position = slotCorners[slotCorner] - tooltipCorners[tooltipCorner] + _tooltip.transform.position;
        }

        private int GetCornerIndex(bool below, bool right)
        {
            if (below && !right)
            {
                return 0;
            }
            else if (!below && !right)
            {
                return 1;
            }
            else if (!below && right)
            {
                return 2;
            }
            else 
            { 
                return 3; 
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ClearTooltip();
        }
    }
}