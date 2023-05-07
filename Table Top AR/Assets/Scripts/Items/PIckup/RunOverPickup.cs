using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.Items.PickUp
{
    [RequireComponent(typeof(Pickup))]
    public class RunOverPickup : MonoBehaviour
    {
        private Pickup _pickup;

        private void Awake()
        {
            _pickup = GetComponent<Pickup>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<GenericInput>(out var input))
            {
                return;
            }

            _pickup.PickupItem();
        }
    }
}
