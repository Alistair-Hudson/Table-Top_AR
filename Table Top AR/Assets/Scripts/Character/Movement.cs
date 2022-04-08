using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TableTopAR.Core;

namespace TableTopAR.Character
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
    public class Movement : MonoBehaviour, IAction
    {
        private NavMeshAgent navMesh;
        private Animator animator;
        private ActionScheduler actionScheduler;

        private void Awake()
        {
            navMesh = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            UpdateAnimator();
        }

        public void SetDestination(Vector3 destinastion)
        {
            actionScheduler.StartAction(this);
            navMesh.isStopped = false;
            navMesh.destination = destinastion;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMesh.velocity;
            Vector3 localVel = transform.InverseTransformDirection(velocity);
            float speed = localVel.z;
            animator.SetFloat("forwardSpeed", speed);
        }

        public void Cancel()
        {
            navMesh.isStopped = true;
        }

        #region Animation Events
        private void FootR()
        {

        }

        private void FootL()
        {

        }
        #endregion
    }
}
