using System;
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
        [SerializeField]
        private float _aggroCoolDown = 2f;
        [SerializeField]
        private PatrolPath _patrolPath;
        [SerializeField]
        private float _waypointTolerence = 0.5f;
        [SerializeField]
        private float _maxWayPointDwellTime = 5f;
        [SerializeField]
        private float _alertDist = 5f;
        
        private Vector3 _guadPos;
        private float _timeSinceSuspected = Mathf.Infinity;
        private float _timeSinceAgrro = Mathf.Infinity;
        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private float _wayPointDwellTime;
        private int _currentWaypoint = 0;

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
            if (Vector3.Distance(transform.position, _player.transform.position) < _chaseDistance || _timeSinceAgrro < _aggroCoolDown)
            {
                _timeSinceSuspected = 0;
                _combat.SetAttackTarget(_player.GetComponent<CombatTarget>());
                AgrroMob();
            }
            else if (_timeSinceSuspected < _suspectTime)
            {
                _actionScheduler.CancelCurrentAction();
                _timeSinceSuspected += Time.deltaTime;
            }
            else
            {
                Patrol();
                _timeSinceArrivedAtWaypoint += Time.deltaTime;
            }
            _timeSinceAgrro += Time.deltaTime;
        }

        private void AgrroMob()
        {
            var hits = Physics.SphereCastAll(transform.position, _alertDist, Vector3.up, 0);
            foreach (var hit in hits)
            {
                if (hit.collider.TryGetComponent<AIController>(out var ai))
                {
                    ai.Aggro();
                }
            }
        }

        public void Aggro()
        {
            _timeSinceAgrro = 0;
            _combat.SetAttackTarget(_player.GetComponent<CombatTarget>());
        }

        private void Patrol()
        {
            Vector3 nextPosition = _guadPos;
            if (_patrolPath != null)
            {
                if (AtWayPoint())
                {
                    _timeSinceArrivedAtWaypoint = 0;
                    _wayPointDwellTime = UnityEngine.Random.Range(0f, _maxWayPointDwellTime);
                    CycleWayPoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if (_timeSinceArrivedAtWaypoint > _wayPointDwellTime)
            {
                _movement.SetDestination(nextPosition);
            }
        }

        private Vector3 GetCurrentWaypoint()
        {
            return _patrolPath.GetWayPoint(_currentWaypoint);
        }

        private void CycleWayPoint()
        {
            _currentWaypoint = _patrolPath.GetNextIndex(_currentWaypoint);
        }

        private bool AtWayPoint()
        {
            return (Vector3.Distance(transform.position, GetCurrentWaypoint()) <= _waypointTolerence);
        }

        //Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
    }
}
