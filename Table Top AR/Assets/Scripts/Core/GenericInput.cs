using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character;
using UnityEngine;

namespace TableTopAR.Core
{
    public class GenericInput : MonoBehaviour
    {
        private Movement characterMovement;
        private Combat characterCombat;

        protected virtual void Awake()
        {
            characterMovement = GetComponent<Movement>();
            characterCombat = GetComponent<Combat>();
        }

        protected void ProcessRaycast(Ray ray)
        {
            bool hitEnemy = false;
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