using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TableTopAR.Character;
using TableTopAR.Core;
using UnityEngine.UI;
using System.Linq;

namespace TableTopAR.WorkShop
{
    public class WorkShopInput : GenericInput
    {
        private Camera _mainCamera;
        private List<Button> _abilitybuttons = new List<Button>();

        protected override void Awake()
        {
#if UNITY_EDITOR
            base.Awake();
            _mainCamera = Camera.main;
            _abilitybuttons = _abilityButtonBar.GetComponentsInChildren<Button>().ToList();
#endif
        }

#if UNITY_EDITOR
        private void Update()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            ProcessRaycast(ray);
            for (int i = 0; i < _abilitybuttons.Count; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    _abilitybuttons[i].onClick.Invoke();
                }
            }
        }
#endif
    }
}
