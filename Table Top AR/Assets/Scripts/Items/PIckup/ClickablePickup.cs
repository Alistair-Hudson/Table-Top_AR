using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.Items.PickUp
{
    [RequireComponent(typeof(Pickup))]
    public class ClickablePickup : MonoBehaviour, IRayCastable
    {
        private Pickup _pickup;

        private void Awake()
        {
            _pickup = GetComponent<Pickup>();
        }

        public CursorType GetCursorType()
        {
            if (_pickup.CanBePickedUp)
            {
                return CursorType.PickUp;
            }
            else
            {
                return CursorType.FullPickup;
            }
        }

        public bool HandleRaycast(GenericInput callingInput)
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            {
                _pickup.PickupItem();
            }
            return true;
        }
    }
}