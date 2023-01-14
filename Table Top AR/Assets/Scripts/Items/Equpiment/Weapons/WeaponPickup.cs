using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character;
using UnityEngine;

namespace TableTopAR.Items.Equipment.Weapons
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField]
        private GenericWeapon _weapon = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<AR.PlayerInput>(out var player))
            {
                player.GetComponent<Combat>().EquipWeapon(_weapon);
                Destroy(gameObject);
            }
        }
    }
}