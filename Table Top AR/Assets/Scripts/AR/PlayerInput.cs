using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TableTopAR.Character;
using TableTopAR.Core;
using UnityEngine.UI;

namespace TableTopAR.AR
{
    public class PlayerInput : GenericInput
    {
        private Camera _arCamera = null;
        private RawImage _rayCastPointImage = null;
        private Transform _rayCastPointTransform = null;

#if UNITY_ANDROID && !UNITY_EDITOR
        protected override void Awake()
        {
            base.Awake();
            var placmentManager = FindObjectOfType<ARPlacementManager>();
            _rayCastPointImage = placmentManager.RayCastPointImage;
            _rayCastPointTransform = _rayCastPointImage.transform;
            _arCamera = placmentManager.ARCamera;
        }

        void Update()
        {
            Ray ray = _arCamera.ScreenPointToRay(_rayCastPointTransform.position);
            ProcessRaycast(ray);
        }
#endif

        protected override void SetCursorImp(CursorMapping cursorMapping)
        {
            _rayCastPointImage.texture = cursorMapping.Texture;
        }
    }
}
