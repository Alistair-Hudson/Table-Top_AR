using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character;
using UnityEngine;

namespace TableTopAR.Core
{
    [RequireComponent(typeof(Movement), typeof(Combat), typeof(Health))]
    public class GenericInput : MonoBehaviour
    {
        private Movement _movement;
        private Combat _combat;
        private Health _health;

        protected virtual void Awake()
        {
            _movement = GetComponent<Movement>();
            _combat = GetComponent<Combat>();
            _health = GetComponent<Health>();
        }

        protected void ProcessRaycast(Ray ray)
        {
            if (_health.IsDead)
            {
                return;
            }
            bool hitEnemy = false;
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent<CombatTarget>(out CombatTarget target))
                {
                    if (!_combat.CanAttack(target))
                    {
                        continue;
                    }
                    _combat.SetAttackTarget(target);
                    hitEnemy = true;
                    break;
                }
            }
            if (!hitEnemy && hits.Length > 0)
            {
                _movement.SetDestination(hits[0].point);
            }
        }
    }
}