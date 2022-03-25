using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TableTopAR.Character
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent navMesh;
        private Animator animator;

        private void Awake()
        {
            navMesh = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            UpdateAnimator();
        }

        public void SetDestination(Vector3 destinastion)
        {
            navMesh.destination = destinastion;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMesh.velocity;
            Vector3 localVel = transform.InverseTransformDirection(velocity);
            float speed = localVel.z;
            animator.SetFloat("forwardSpeed", speed);
        }
    }
}
