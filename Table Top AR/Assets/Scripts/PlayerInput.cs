using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Camera _arCamera;
    [SerializeField]
    private Transform _rayCastPoint;
    [SerializeField]
    private NavMeshAgent _character;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Ray ray = _arCamera.ScreenPointToRay(_rayCastPoint.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _character.SetDestination(hit.point);
            }
        }
    }
}
