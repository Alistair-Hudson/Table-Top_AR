using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TableTopAR.Character;
using TableTopAR.Core;

namespace TableTopAR.WorkShop
{
    public class WorkShopInput : GenericInput
    {
        private Camera mainCamera;

        protected override void Awake()
        {
            base.Awake();
            mainCamera = Camera.main;    
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                ProcessRaycast(ray);
            }
        }
    }
}
