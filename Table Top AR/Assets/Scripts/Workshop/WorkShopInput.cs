using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TableTopAR.Character;
using TableTopAR.Core;

namespace TableTopAR.WorkShop
{
    public class WorkShopInput : GenericInput
    {
#if UNITY_EDITOR
        private Camera _mainCamera;

        protected override void Awake()
        {
            base.Awake();
            _mainCamera = Camera.main;    
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                ProcessRaycast(ray);
            }
        }
#endif
    }
}
