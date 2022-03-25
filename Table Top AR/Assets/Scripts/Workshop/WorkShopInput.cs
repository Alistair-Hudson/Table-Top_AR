using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TableTopAR.Character;

namespace TableTopAR.WorkShop
{
    public class WorkShopInput : MonoBehaviour
    {
        [SerializeField]
        private Movement characterMovement;

        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    characterMovement.SetDestination(hit.point);
                }
            }
        }
    }
}
