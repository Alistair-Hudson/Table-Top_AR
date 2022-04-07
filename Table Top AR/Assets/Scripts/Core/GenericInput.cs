using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character;
using UnityEngine;

namespace TableTopAR.Core
{
    public class GenericInput : MonoBehaviour
    {
        private Movement _characterMovement;
        private Combat _characterCombat;


        protected virtual void Awake()
        {
            _characterMovement = GetComponent<Movement>();
            _characterCombat = GetComponent<Combat>();
        }

        protected void ProcessRaycast(Ray ray)
        {
            bool hitEnemy = false;
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent<CombatTarget>(out CombatTarget target))
                {
                    _characterCombat.Attack(target);
                    hitEnemy = true;
                    break;
                }
            }
            if (!hitEnemy && hits.Length > 0)
            {
                _characterMovement.SetDestination(hits[0].point);
            }
        }
    }
}