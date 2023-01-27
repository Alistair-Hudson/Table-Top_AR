using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character;
using UnityEngine;

namespace TableTopAR.Items.Equipment.Weapons
{
    [RequireComponent(typeof(Collider))]
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField]
        private GenericWeapon _weapon = null;
        [SerializeField]
        private float respawnTime = 10;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<AR.PlayerInput>(out var player))
            {
                player.GetComponent<Combat>().EquipWeapon(_weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool show)
        {
            GetComponent<Collider>().enabled = show;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(show);
            }
        }
    }
}