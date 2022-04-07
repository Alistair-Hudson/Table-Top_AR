using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TableTopAR.Character;
using TableTopAR.Core;

namespace TableTopAR.AR
{
    public class PlayerInput : GenericInput
    {
        [SerializeField]
        private Camera _arCamera;
        [SerializeField]
        private Transform _rayCastPoint;

        public NavMeshAgent Character { get; set; }


        void Update()
        {
            if (Input.touchCount > 0)
            {
                Ray ray = _arCamera.ScreenPointToRay(_rayCastPoint.position);
                ProcessRaycast(ray);
            }
        }
    }
}
