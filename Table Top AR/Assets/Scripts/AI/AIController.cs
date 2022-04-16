using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.AI
{
    [RequireComponent(typeof(Movement), typeof(Combat), typeof(Health))]
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private float _chaseDistance = 5f;
        [SerializeField]
        private float _suspectTime = 2f;
        
        private Vector3 _guadPos;
        private float _timeSinceSuspected = 0;

        private GenericInput _player;
        private Movement _movement;
        private Combat _combat;
        private Health _health;
        private ActionScheduler _actionScheduler;

        private void Awake()
        {
            _guadPos = transform.position;

            _player = FindObjectOfType<GenericInput>();
            _movement = GetComponent<Movement>();
            _combat = GetComponent<Combat>();
            _health = GetComponent<Health>();
            _actionScheduler = GetComponent<ActionScheduler>();

        }

        private void Update()
        {
            if (_health.IsDead)
            {
                return;
            }
            if (Vector3.Distance(transform.position, _player.transform.position) < _chaseDistance)
            {
                _timeSinceSuspected = 0;
                _combat.SetAttackTarget(_player.GetComponent<CombatTarget>());
            }
            else if (_timeSinceSuspected < _suspectTime)
            {
                _actionScheduler.CancelCurrentAction();
                _timeSinceSuspected += Time.deltaTime;
            }
            else
            {
                _movement.SetDestination(_guadPos);
            }
        }

        //Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
    }
}
