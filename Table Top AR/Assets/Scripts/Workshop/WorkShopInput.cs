using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TableTopAR.Character;

namespace TableTopAR.WorkShop
{
    public class WorkShopInput : MonoBehaviour
    {
        private Movement characterMovement;
        private Combat characterCombat;
        private Camera mainCamera;

        private void Awake()
        {
            characterMovement = GetComponent<Movement>();
            characterCombat = GetComponent<Combat>();
            mainCamera = Camera.main;
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                bool hitEnemy = false;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray);
                foreach (var hit in hits)
                {
                    if (hit.transform.TryGetComponent<CombatTarget>(out CombatTarget target))
                    {
                        characterCombat.Attack(target);
                        hitEnemy = true;
                        break;
                    }
                }
                if (!hitEnemy && hits.Length > 0)
                {
                    characterMovement.SetDestination(hits[0].point);
                }
            }
        }
    }
}
