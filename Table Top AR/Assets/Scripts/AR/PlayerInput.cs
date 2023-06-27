using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TableTopAR.Character;
using TableTopAR.Core;
using UnityEngine.UI;
using Unity.XR.CoreUtils;

namespace TableTopAR.AR
{
    public class PlayerInput : GenericInput
    {
        private Camera _arCamera = null;
        private RawImage _rayCastPointImage = null;
        private Transform _rayCastPointTransform = null;

        public Camera ARCamera { get => _arCamera; }
        public Transform RayCastPointTransform { get => _rayCastPointTransform; }

        protected override void Awake()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            base.Awake();
            var arComponents = FindObjectOfType<XROrigin>().GetComponent<ARComponentsPassThrough>();
            _rayCastPointImage = arComponents.RayCastPoint;
            _rayCastPointTransform = _rayCastPointImage.transform;
            _arCamera = arComponents.ARCamera;
#endif
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        void Update()
        {
            //if (!ARPlaneDetectionManager.IsPlaying) return;
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
