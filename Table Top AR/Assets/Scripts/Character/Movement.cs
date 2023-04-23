using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TableTopAR.Core;
using TableTopAR.Saving;

namespace TableTopAR.Character
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(Health))]
    [RequireComponent(typeof(ActionScheduler))]
    public class Movement : MonoBehaviour, IAction, ISaveable
    {
        [System.Serializable]
        public struct MovementSaveData
        {
            public SerializableVector3 Position;
            public SerializableVector3 Rotation;
        }

        [SerializeField]
        private float maxNavPathLength = 40f;

        private NavMeshAgent _navMesh;
        private Animator _animator;
        private ActionScheduler _actionScheduler;
        private Health _health;

        private void Start()
        {
            _navMesh = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            _navMesh.enabled = !_health.IsDead;

            UpdateAnimator();
        }

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            if (!NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path))
            {
                return false;
            }
            if (path.status != NavMeshPathStatus.PathComplete)
            {
                return false;
            }
            if (GetPathLength(path) > maxNavPathLength)
            {
                return false;
            }

            return true;
        }


        private float GetPathLength(NavMeshPath path)
        {
            float distance = 0;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return distance;
        }

        public void SetDestination(Vector3 destinastion)
        {
            _actionScheduler.StartAction(this);
            _navMesh.isStopped = false;
            _navMesh.destination = destinastion;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = _navMesh.velocity;
            Vector3 localVel = transform.InverseTransformDirection(velocity);
            float speed = localVel.z;
            _animator.SetFloat("forwardSpeed", speed);
        }

        public void Cancel()
        {
            _navMesh.isStopped = true;
        }

#region Animation Events
        private void FootR()
        {

        }

        private void FootL()
        {

        }
#endregion

        public object CaptureState()
        {
            MovementSaveData data = new MovementSaveData
            {
                Position = new SerializableVector3(transform.position),
                Rotation = new SerializableVector3(transform.eulerAngles)
            };
            return data;
        }

        public void RestoreState(object state)
        {
            MovementSaveData data = (MovementSaveData)state;
            _navMesh.enabled = false;
            transform.position = data.Position.ToVector();
            transform.eulerAngles = data.Rotation.ToVector();
            _navMesh.enabled = true;
        }
    }
}
