using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TableTopAR.Character;

namespace TableTopAR.AR
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        private Camera _arCamera;
        [SerializeField]
        private Transform _rayCastPoint;
        [SerializeField]
        private Movement characterMovement;

        public NavMeshAgent Character { get; set; }

        private void Awake()
        {
            Character = characterMovement.GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                Ray ray = _arCamera.ScreenPointToRay(_rayCastPoint.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    characterMovement.SetDestination(hit.point);
                }
            }
        }
    }
}
